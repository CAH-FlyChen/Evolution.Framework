using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Evolution.Plugins.ScrumKanBan.Models;
using Evolution.Domain.Entity.SystemManage;
using Evolution.Web;
using Evolution.Framework;

namespace Evolution.Plugins.ScrumKanBan.Controllers
{
    [Area("KanBan")]
    public class ProjectController : EvolutionControllerBase
    {
        private readonly KanBanDbContext _context;

        public ProjectController(KanBanDbContext context)
        {
            _context = context;
        }

        // GET: Projects
        public new async Task<IActionResult> Index()
        {
            return View(await _context.Projects.ToListAsync());
        }

        public async Task<IActionResult> GetGridJson()
        {
            var data = await _context.Projects.ToListAsync();
            return Content(data.ToJson());
        }

        // GET: Projects/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return NotFound();
            }

            var project = await _context.Projects.SingleOrDefaultAsync(m => m.Id == id);
            if (project == null)
            {
                return NotFound();
            }

            return View(project);
        }

        // GET: Projects/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Projects/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,Order")] ProjectEntity project)
        {
            if (ModelState.IsValid)
            {
                project.CreateTime = DateTime.Now;
                project.LastModifyTime = DateTime.Now;
                UserEntity usere = _context.Set<UserEntity>().SingleOrDefault(t => t.Account == HttpContext.User.Identity.Name);
                project.CreatorUserId = usere.Id;
                project.LastModifyUserId = project.CreatorUserId;
                project.Id = Guid.NewGuid().ToString("N");
                _context.Add(project);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(project);
        }

        // GET: Projects/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var project = await _context.Projects.SingleOrDefaultAsync(m => m.Id == id);
            if (project == null)
            {
                return NotFound();
            }
            return View(project);
        }

        // POST: Projects/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("ID,CreateTime,Name,Order,UpdateTime")] ProjectEntity project)
        {
            if (id != project.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(project);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProjectExists(project.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Index");
            }
            return View(project);
        }

        // GET: Projects/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var project = await _context.Projects.SingleOrDefaultAsync(m => m.Id == id);
            if (project == null)
            {
                return NotFound();
            }

            return View(project);
        }

        // POST: Projects/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var project = await _context.Projects.SingleOrDefaultAsync(m => m.Id == id);
            _context.Projects.Remove(project);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }


        public IActionResult ViewStories(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            return RedirectToRoute(new { controller = "UserStory", action = "Index", projid=id }); ;
        }


        private bool ProjectExists(string id)
        {
            return _context.Projects.Any(e => e.Id == id);
        }


    }
}
