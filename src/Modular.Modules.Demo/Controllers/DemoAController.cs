using Evolution.Framework;
using Microsoft.AspNetCore.Mvc;
using Evolution.Application.SystemManage;
using Evolution.Domain.Entity.SystemManage;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;

namespace Modular.Modules.Demo
{
	[Area("DemoArea")]
    public class DemoAController : MControllerBase
    {
	    //构造函数用于注入
		//public DemoAController()
        //{
        //    
        //}

        		[HttpGet]
		[HandlerAjaxOnly]
		[PermissionLevelDescrip("", "")]
		public async Task<ActionResult> GetTreeGridJson(string keyword)
		{
			var data = await organizeApp.GetList();
			if (!string.IsNullOrEmpty(keyword))
			{
				data = data.TreeWhere(t => t.FullName.Contains(keyword));
			}
			var treeList = new List<TreeGridModel>();
			foreach ([#EntityNAME#] item in data)
			{
				TreeGridModel treeModel = new TreeGridModel();
				bool hasChildren = data.Count(t => t.ParentId == item.Id) == 0 ? false : true;
				treeModel.id = item.Id;
				treeModel.isLeaf = hasChildren;
				treeModel.parentId = item.ParentId;
				treeModel.expanded = hasChildren;
				treeModel.entityJson = item.ToJson();
				treeList.Add(treeModel);
			}
			return Content(treeList.TreeGridJson());
		}


		[HttpPost]
        [HandlerAjaxOnly]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SubmitForm(RoleEntity roleEntity, string keyValue)
        {
            //await dutyApp.Save(roleEntity, keyValue,HttpContext);
            return Success("操作成功。");
        }

        [HttpGet]
        [HandlerAjaxOnly]
        public async Task<IActionResult> GetFormJson(string keyValue)
        {
            var data = await dutyApp.GetForm(keyValue);
            return Content(data.ToJson());
        }

        [HttpPost]
        [HandlerAjaxOnly]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteForm(string keyValue)
        {
            //await myApp.Delete(keyValue);
            return Success("删除成功。");
        }

    }
}
