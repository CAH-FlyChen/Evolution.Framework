using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Evolution.Web.Attributes
{
    public class PermissionLevelDescripAttribute : Attribute
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public PermissionLevelDescripAttribute(string name = "", string description = "")
        {
            this.Name = name;
            this.Description = description;
        }
    }
}
