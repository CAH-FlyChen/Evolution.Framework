/*******************************************************************************
 * Copyright © 2016 Evolution.Framework 版权所有
 * Author: Evolution
 * Description: Evolution快速开发平台
 * Website：
*********************************************************************************/
using Evolution.Repository;
using Evolution.Data;
using Evolution.Domain.Entity.SystemManage;
using Evolution.Domain.IRepository.SystemManage;
using Evolution.Repository.SystemManage;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using System.Text;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace Evolution.Repository.SystemManage
{
    public class ItemsDetailRepository : RepositoryBase<ItemsDetailEntity>, IItemsDetailRepository
    {
        private EvolutionDBContext ctx = null;
        public ItemsDetailRepository(EvolutionDBContext ctx) : base(ctx)
        {
            this.ctx = ctx;
        }
        public Task<List<ItemsDetailEntity>> GetItemList(string enCode)
        {
            var x = from a in ctx.ItemsDetails
                     join b in ctx.Items on a.ItemId equals b.Id
                     where b.EnCode == enCode && a.EnabledMark == true && a.DeleteMark == false
                     orderby a.SortCode
                     select a;
            return x.ToListAsync();
            
            //StringBuilder strSql = new StringBuilder();
            //strSql.Append(@"SELECT  d.*
            //                FROM    Sys_ItemsDetail d
            //                        INNER  JOIN Sys_Items i ON i.F_Id = d.F_ItemId
            //                WHERE   1 = 1
            //                        AND i.F_EnCode = @enCode
            //                        AND d.F_EnabledMark = 1
            //                        AND d.F_DeleteMark = 0
            //                ORDER BY d.F_SortCode ASC");
            //DbParameter[] parameter = 
            //{
            //     new SqlParameter("@enCode",enCode)
            //};
            //return this.FindList(strSql.ToString(), parameter);
        }
    }
}
