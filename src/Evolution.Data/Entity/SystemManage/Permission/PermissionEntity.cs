using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Evolution.Data.Entity.SystemManage
{
    public class PermissionEntity
    {
        public string DescriptionName { get; set; }
        public string FullNamespace { get; set; }
        public string Url { get; set; }
        public string MoudleName { get; set; }
        public string Name { get; set; }
        public int Type { get; set; }
        public string Description { get; set; }
    }
}
