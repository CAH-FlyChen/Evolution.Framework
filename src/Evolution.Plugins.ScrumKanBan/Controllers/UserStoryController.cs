using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using AutoMapper;
using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;
using Evolution.Plugins.ScrumKanBan.Models;
using Evolution.Domain.Entity.SystemManage;
using Evolution.Web;

namespace Evolution.Plugins.ScrumKanBan.Controllers
{
    [Area("KanBan")]
    public class UserStoryController : EvolutionControllerBase
    {
        private string mystr;
        public UserStoryListViewModel models = new UserStoryListViewModel();
        private KanBanDbContext _context = null;
        private IMapper mapper = null;
        /// <summary>
        /// 注入
        /// </summary>
        /// <param name="context"></param>
        /// <param name="opt"></param>
        public UserStoryController(KanBanDbContext context, IMapper mapper)
        {
            this._context = context;
            this.mapper = mapper;
        }
        public override ActionResult Index()
        {
            return Error("必须带请求参数！");
        }
        // GET: UserStoryViewModels
        public async Task<IActionResult> Index(string projID)
        {
            var project = await _context.Projects.SingleOrDefaultAsync(x => x.Id == projID);
            List<UserStoryEntity> userStories = await _context.UserStories.Where(t=>t.Project.Id== projID).OrderBy(t=>t.SortCode).ToListAsync();
            foreach (var us in userStories)
            {
                UserStoryViewModel m = mapper.Map<UserStoryViewModel>(us);
                m.ButtonDisplayName = StoryStatusList.GetStatusButtonDisplay(m.StatusCode, _context).ButtonDisplayName;
                if (m.ListID == "Backlog")
                {
                    models.BacklogItemCount += 1;
                    models.BacklogItems.Add(m);
                } 
                else if (m.ListID == "Current")
                {
                    models.CurrentItemCount += 1;
                    models.CurrentItems.Add(m);
                }
                else if (m.ListID == "ICEBox")
                {
                    models.ICEItemCount += 1;
                    models.ICEItems.Add(m);
                }
                else if (m.ListID == "Done")
                {
                    models.DoneItemsCount += 1;
                    models.DoneItems.Add(m);
                }
            }
            ViewBag.ProjectName = project.Name;
                
            return View(models);
        }

        public IActionResult Create(string listId)
        {
            UserStoryViewModel userStoryViewModel = new UserStoryViewModel();
            userStoryViewModel.Content = "";
            userStoryViewModel.ID = Guid.NewGuid().ToString("N");
            userStoryViewModel.Point = 0;
            userStoryViewModel.ListID = listId;
            var assigntoSelectList = _context.Set<UserEntity>().ToList().Select(a => new SelectListItem
            {
                Text = a.NickName,
                Value = a.Id
            });

            userStoryViewModel.AssignToList = assigntoSelectList;

            var selectList = StoryStatusList.GetStatusList(_context).OrderBy(t => t.SortCode).Select(a => new SelectListItem
            {
                Text = a.Text,
                Value = a.Code
            });

            userStoryViewModel.StatusList = selectList;
            return PartialView(userStoryViewModel);
        }

        //// POST: UserStoryViewModels/Create
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(UserStoryViewModel userStoryViewModel)
        {
            if (ModelState.IsValid)
            {
                //mapping   
                UserStoryEntity usNew = mapper.Map<UserStoryEntity>(userStoryViewModel);
                usNew.Id = Guid.NewGuid().ToString("N");
                usNew.StatusCode = "Unstarted";
                UserEntity currentUser = _context.Set<UserEntity>().SingleOrDefault(t => t.Account == User.Identity.Name);
                usNew.CreatorUserId = currentUser.Id;
                usNew.CreateTime = DateTime.Now;
                usNew.SortCode = _context.UserStories.Max(t => t.SortCode)+1;
                _context.UserStories.Add(usNew);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View("OK");
        }


        // GET: UserStoryViewModels/Edit/5
        [HttpPost]
        public async Task<IActionResult> Move([FromBody] StoryMoveViewModel p)
        {
            if (p.ItemId == null)
            {
                return HttpNotFound();
            }

            UserStoryEntity us = await _context.UserStories.SingleAsync(m => m.Id == p.ItemId);
            if (us == null)
            {
                return HttpNotFound();
            }

            us.StatusCode = p.TargetStatus;
            await _context.SaveChangesAsync();
            return Json(true);
            //return View();
        }

        private IActionResult HttpNotFound()
        {
            Response.StatusCode = 404;
            return null;
        }

        [HttpPost]
        public async Task<IActionResult> ChangeOrder([FromBody] ChangeOrderViewModel p)
        {
            UserStoryEntity story = await _context.UserStories.SingleAsync(m => m.Id == p.ItemID);
            if (story == null) return HttpNotFound();

            //跨列表
            if (p.NewListID!=p.OldListID && p.NewListID!="" && p.OldListID!="")
            {
                story.SortCode = p.NewIndex;
                story.ListID = p.NewListID;
                //分割位置下面的向上移动
                //处理新列表。下移动新列表的分割条目后面的项目
                var downItems = _context.UserStories.Where(t => t.SortCode >= p.NewIndex && t.ListID == p.NewListID).OrderBy(t => t.SortCode).ToList();
                for (int i = 0; i < downItems.Count; i++)
                {
                    var item = downItems[i];
                    item.SortCode = i + 1 + p.NewIndex;
                }
                //处理源列表移出位置上移
                var oldListItems = _context.UserStories.Where(t => t.SortCode > p.OldIndex && t.ListID == p.OldListID).OrderBy(t => t.SortCode).ToList();
                for(int i=0;i<oldListItems.Count;i++)
                {
                    var item = oldListItems[i];
                    item.SortCode = p.OldIndex + i;
                }
                
            }
            else
            {
                //同列表
                if (p.NewIndex > p.OldIndex)
                {
                    //向下移动
                    //找到在旧列表中 分割的位置上边的内容
                    var upItems = _context.UserStories.Where(t => t.SortCode <= p.NewIndex && t.SortCode > p.OldIndex && t.ListID == p.OldListID && t.SortCode != p.OldIndex).OrderBy(t => t.SortCode).ToList();

                    story.SortCode = p.NewIndex;
                    for (int i = 0; i < upItems.Count; i++)
                    {
                        var item = upItems[i];
                        item.SortCode = i + p.OldIndex;
                    }
                }
                else
                {
                    //向上移动
                    //找到在旧列表中 分割的位置下边的内容
                    var downItems = _context.UserStories.Where(t => t.SortCode >= p.NewIndex && t.SortCode < p.OldIndex && t.ListID == p.OldListID && t.SortCode != p.OldIndex).OrderBy(t => t.SortCode).ToList();

                    story.SortCode = p.NewIndex;
                    for (int i = 0; i < downItems.Count; i++)
                    {
                        var item = downItems[i];
                        item.SortCode = i + 1 + p.NewIndex;
                    }
                }
            }

            _context.SaveChanges();
            return RedirectToAction("Index");
        }


        public async Task<IActionResult> Edit(string listID,string itemID)
        {
            if (string.IsNullOrEmpty(itemID))
            {
                return HttpNotFound();
            }
            UserStoryEntity story = await _context.UserStories.SingleAsync(m => m.Id == itemID);
            if (story == null)
            {
                return HttpNotFound();
            }
            UserStoryViewModel usvm = mapper.Map<UserStoryViewModel>(story);

            var assigntoSelectList = _context.Set<UserEntity>().ToList().Select(a => new SelectListItem
            {
                Text = a.NickName,
                Value = a.Id
            });

            usvm.AssignToList = assigntoSelectList;

            var selectList = StoryStatusList.GetStatusList(_context).OrderBy(t => t.SortCode).Select(a => new SelectListItem
            {
                Text = a.Text,
                Value = a.Code
            });

            usvm.StatusList = selectList;
            return PartialView(usvm);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(UserStoryViewModel vm)
        {
            if (vm.ID == null)
            {
                return HttpNotFound();
            }

            UserStoryEntity story = await _context.UserStories.SingleAsync(m => m.Id == vm.ID);
            if (story == null)
            {
                return HttpNotFound();
            }
            story.Content = vm.Content;
            story.ItemTypeCode = vm.ItemTypeCode;
            story.Point = vm.Point;
            story.StatusCode = vm.StatusCode;
            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> ChangeStatus([FromBody]ChangeStoryStatusViewModel p)
        {
            if (ModelState.IsValid)
            {
                var item = _context.UserStories.Single(t => t.Id == p.ItemID);
                var s = StoryStatusList.GetNextStatusButtonDisplay(item.StatusCode, _context);
                if (string.IsNullOrEmpty(p.ApprovalResult))
                    item.StatusCode = s.Code;
                else if (p.ApprovalResult == "Y")
                    item.StatusCode = "Accepted";
                else
                    item.StatusCode = "Rejected";
                await _context.SaveChangesAsync();
                
                UserStoryViewModel model = mapper.Map<UserStoryViewModel>(item);
                model.ButtonDisplayName = s.ButtonDisplayName;
                return ViewComponent("StatusButton", new { model = model });
            }

            return null;
        }

    }
}
