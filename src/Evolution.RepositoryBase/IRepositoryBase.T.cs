/*******************************************************************************
 * Copyright © 2016 Evolution.Framework 版权所有
 * Author: Evolution
 * Description: Evolution快速开发平台
 * Website：
*********************************************************************************/
using Evolution.Framework;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Evolution.IRepository
{
    /// <summary>
    /// 仓储接口
    /// </summary>
    /// <typeparam name="TEntity">实体类型</typeparam>
    public interface IRepositoryBase<TEntity> where TEntity : class,new()
    {
        Task<List<TEntity>> GetAllAsync();
        Task<int> InsertAsync(TEntity entity);
        Task<int> InsertAsync(List<TEntity> entitys);
        Task<int> UpdateAsync(TEntity entity);
        Task<int> DeleteAsync(TEntity entity);
        Task<int> DeleteAsync(Expression<Func<TEntity, bool>> predicate);
        Task<TEntity> FindEntityAsync(object keyValue);
        Task<TEntity> FindEntityASync(Expression<Func<TEntity, bool>> predicate);
        IQueryable<TEntity> IQueryable();
        IQueryable<TEntity> IQueryable(Expression<Func<TEntity, bool>> predicate);
        Task<List<TEntity>> FindListAsync(string strSql);
        Task<List<TEntity>> FindListAsync(string strSql, DbParameter[] dbParameter);
        Task<List<TEntity>> FindListAsync(Pagination pagination);
        Task<List<TEntity>> FindListAsync(Expression<Func<TEntity, bool>> predicate, Pagination pagination);
        //void BeginTrans();
        //void CommitTrans();
        //void RollbackTrans();
    }
}
