using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Evolution.Plugins.ScrumKanBan.Models
{
    public class StoryMoveViewModel
    {
        public string ItemId { get; set; }
        public string TargetStatus { get; set; }
    }
}
