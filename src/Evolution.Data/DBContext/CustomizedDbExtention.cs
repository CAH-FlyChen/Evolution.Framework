using Microsoft.EntityFrameworkCore.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;

namespace Evolution.Data.DBContext
{
    public class CustomizedDbExtention : IDbContextOptionsExtension
    {
        public string MyData { get; set; }
        public void ApplyServices(IServiceCollection services)
        {
            
        }
    }
}
