using System.Linq;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Mvc.Rendering;
using Evolution.Plugins.ScrumKanBan.Models;
using Evolution.Web;

namespace Evolution.Plugins.ScrumKanBan.Controllers
{
    [Area("KanBan")]
    public class StoryStatusController : EvolutionControllerBase
    {
        private string mystr;
        private KanBanDbContext _context;
        /// <summary>
        /// 注入
        /// </summary>
        /// <param name="context"></param>
        /// <param name="opt"></param>
        public StoryStatusController(KanBanDbContext context)
        {
            _context = context;
        }

        public IEnumerable<SelectListItem> GetStatusSelectList()
        {
            var selectList = StoryStatusList.GetStatusList(_context).OrderBy(t => t.SortCode).Select(a => new SelectListItem
            {
                Text = a.Text,
                Value = a.Code
            });
            return selectList;
        }



    }
}
