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
    public interface IRepositoryBase : IDisposable
    {
        IRepositoryBase BeginTrans();
        Task<int> CommitAsync();
        Task<int> InsertAsync<TEntity>(TEntity entity) where TEntity : class;
        Task<int> InsertAsync<TEntity>(List<TEntity> entitys) where TEntity : class;
        Task<int> UpdateAsync<TEntity>(TEntity entity) where TEntity : class;
        Task<int> DeleteAsync<TEntity>(TEntity entity) where TEntity : class;
        Task<int> DeleteAsync<TEntity>(Expression<Func<TEntity, bool>> predicate) where TEntity : class;
        Task<TEntity> FindEntityAsync<TEntity>(object keyValue) where TEntity : class;
        Task<TEntity> FindEntityASync<TEntity>(Expression<Func<TEntity, bool>> predicate) where TEntity : class;
        IQueryable<TEntity> IQueryable<TEntity>() where TEntity : class;
        IQueryable<TEntity> IQueryable<TEntity>(Expression<Func<TEntity, bool>> predicate) where TEntity : class;
        Task<List<TEntity>> FindListAsync<TEntity>(string strSql) where TEntity : class;
        Task<List<TEntity>> FindListAsync<TEntity>(string strSql, DbParameter[] dbParameter) where TEntity : class;
        Task<List<TEntity>> FindListAsync<TEntity>(Pagination pagination) where TEntity : class,new();
        Task<List<TEntity>> FindListAsync<TEntity>(Expression<Func<TEntity, bool>> predicate, Pagination pagination) where TEntity : class,new();
    }
}
