using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Evolution.Plugins.ScrumKanBan.Models
{
    public class UserStoryListViewModel
    {
        public int BacklogItemCount { get; set; }
        public List<UserStoryViewModel> BacklogItems { get; set; }
        public int CurrentItemCount { get; set; }
        public List<UserStoryViewModel> CurrentItems { get; set; }
        public List<UserStoryViewModel> ICEItems { get; set; }
        public int DoneItemsCount { get; set; }
        public List<UserStoryViewModel> DoneItems { get; set; }
        public int ICEItemCount { get; set; }

        public UserStoryListViewModel()
        {
            BacklogItems = new List<UserStoryViewModel>();
            CurrentItems = new List<UserStoryViewModel>();
            ICEItems = new List<UserStoryViewModel>();
            DoneItems = new List<UserStoryViewModel>();
        }
    }
}
