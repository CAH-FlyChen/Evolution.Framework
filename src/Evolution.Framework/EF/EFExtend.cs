using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Evolution.Framework.EF
{
    public static class EFExtend
    {
        public static void Delete<TEntity>(this DbContext ctx, Expression<Func<TEntity, bool>> predicate)
    where TEntity : class
        {
            var result = ctx.Set<TEntity>().Where(predicate).ToList();
            ctx.RemoveRange(result);
            ctx.SaveChanges();
        }
    }
}
