using Evolution.Data;
using Evolution.Plugins.Demo.Repos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Evolution.Plugins.Demo.Services
{
    public class DemoService :IDemoService
    {
        IDemoRepositroy repo = null;
        public DemoService(IDemoRepositroy demoRepo)
        {
            this.repo = demoRepo;
        }
        public string GetStr()
        {
            var x = this.repo.GetDemoInfo();
            return x;
        }
    }
}
