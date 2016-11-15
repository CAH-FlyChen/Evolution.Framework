using Evolution.Data;
using Evolution.Data.Entity;
using Evolution.Domain.Entity.SystemManage;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Evolution.Plugins.ScrumKanBan.Models
{
    public class ProjectEntity:EntityBase
    {
        public string Name { get; set; }
        public ICollection<UserStoryEntity> Stories { get; set; }
    }
}
