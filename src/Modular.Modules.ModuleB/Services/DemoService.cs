using Evolution.Data;
using Modular.Modules.ModuleB.Module;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Modular.Modules.ModuleB.Services
{
    public interface IDemoService
    {
        string GetStr();
    }
    public class DemoService :IDemoService
    {
        ModuleBContext dr = null;
        public DemoService(ModuleBContext dr)
        {
            this.dr = dr;
        }
        public string GetStr()
        {
            var x = dr.DemoModels.ToList();
            return "abc";
        }


    }
}
