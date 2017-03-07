/*******************************************************************************
 * Copyright © 2016 Evolution.Framework 版权所有
 * Author: Evolution
 * Description: Evolution快速开发平台
 * Website：
*********************************************************************************/
using Evolution.IRepository;
using Microsoft.EntityFrameworkCore;
using Evolution.Data;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text.RegularExpressions;
using Evolution.Framework;
using Evolution.Data.Extensions;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Storage;

namespace Evolution.Repository
{
    /// <summary>
    /// 仓储实现
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public class RepositoryBase<TEntity> : IRepositoryBase<TEntity> where TEntity : class,new()
    {

        public DbContext dbcontext = null;

        public RepositoryBase(DbContext ctx)
        {
            dbcontext = ctx;
        }

        public Task<List<TEntity>> GetAllAsync()
        {
            return dbcontext.Set<TEntity>().ToListAsync();
        }

        public Task<int> InsertAsync(TEntity entity)
        {
            dbcontext.Entry<TEntity>(entity).State = EntityState.Added;
            return dbcontext.SaveChangesAsync();
        }
        public Task<int> InsertAsync(List<TEntity> entitys)
        {
            foreach (var entity in entitys)
            {
                dbcontext.Entry<TEntity>(entity).State = EntityState.Added;
            }
            return dbcontext.SaveChangesAsync();
        }
        public Task<int> UpdateAsync(TEntity entity)
        {
            dbcontext.Set<TEntity>().Attach(entity);
            PropertyInfo[] props = entity.GetType().GetProperties();
            foreach (PropertyInfo prop in props)
            {
                if (prop.Name == "Id") continue;
                if (prop.GetValue(entity, null) != null)
                {
                    if (prop.GetValue(entity, null).ToString() == "&nbsp;")
                        dbcontext.Entry(entity).Property(prop.Name).CurrentValue = null;
                    dbcontext.Entry(entity).Property(prop.Name).IsModified = true;
                }
            }
            return dbcontext.SaveChangesAsync();
        }
        public Task<int> DeleteAsync(TEntity entity)
        {
            dbcontext.Set<TEntity>().Attach(entity);
            dbcontext.Entry<TEntity>(entity).State = EntityState.Deleted;
            return dbcontext.SaveChangesAsync();
        }
        public Task<int> DeleteAsync(Expression<Func<TEntity, bool>> predicate)
        {
            var entitys = dbcontext.Set<TEntity>().Where(predicate).ToList();
            entitys.ForEach(m => dbcontext.Entry<TEntity>(m).State = EntityState.Deleted);
            return dbcontext.SaveChangesAsync();
        }
        public Task<TEntity> FindEntityAsync(object keyValue)
        {
            return dbcontext.Set<TEntity>().FindAsync(keyValue);
        }
        public Task<TEntity> FindEntityASync(Expression<Func<TEntity, bool>> predicate)
        {
            return dbcontext.Set<TEntity>().FirstOrDefaultAsync(predicate);
        }
        public IQueryable<TEntity> IQueryable()
        {
            return dbcontext.Set<TEntity>();
        }
        public IQueryable<TEntity> IQueryable(Expression<Func<TEntity, bool>> predicate)
        {
            return dbcontext.Set<TEntity>().Where(predicate);
        }
        public Task<List<TEntity>> FindListAsync(string strSql)
        {
            return dbcontext.Set<TEntity>().FromSql<TEntity>(strSql).ToListAsync<TEntity>();
        }
        public Task<List<TEntity>> FindListAsync(string strSql, DbParameter[] dbParameter)
        {
            return dbcontext.Set<TEntity>().FromSql<TEntity>(strSql,dbParameter).ToListAsync<TEntity>();
        }
        public Task<List<TEntity>> FindListAsync(Pagination pagination)
        {
            bool isAsc = pagination.sord.ToLower() == "asc" ? true : false;
            string[] _order = pagination.sidx.Split(',');
            MethodCallExpression resultExp = null;
            var tempData = dbcontext.Set<TEntity>().AsQueryable();
            foreach (string item in _order)
            {
                string _orderPart = item;
                _orderPart = Regex.Replace(_orderPart, @"\s+", " ");
                string[] _orderArry = _orderPart.Split(' ');
                string _orderField = _orderArry[0];
                bool sort = isAsc;
                if (_orderArry.Length == 2)
                {
                    isAsc = _orderArry[1].ToUpper() == "ASC" ? true : false;
                }
                var parameter = Expression.Parameter(typeof(TEntity), "t");
                var property = typeof(TEntity).GetProperty(_orderField);
                var propertyAccess = Expression.MakeMemberAccess(parameter, property);
                var orderByExp = Expression.Lambda(propertyAccess, parameter);
                resultExp = Expression.Call(typeof(Queryable), isAsc ? "OrderBy" : "OrderByDescending", new Type[] { typeof(TEntity), property.PropertyType }, tempData.Expression, Expression.Quote(orderByExp));
            }
            tempData = tempData.Provider.CreateQuery<TEntity>(resultExp);
            pagination.records = tempData.Count();
            tempData = tempData.Skip<TEntity>(pagination.rows * (pagination.page - 1)).Take<TEntity>(pagination.rows).AsQueryable();
            return tempData.ToListAsync();
        }
        public async Task<List<TEntity>> FindListAsync(Expression<Func<TEntity, bool>> predicate, Pagination pagination)
        {
            bool isAsc = pagination.sord.ToLower() == "asc" ? true : false;
            string[] _order = pagination.sidx==null? null: pagination.sidx.Split(',');

            MethodCallExpression resultExp = null;
            var tempData = dbcontext.Set<TEntity>().Where(predicate);
            if(_order!=null)
                foreach (string item in _order)
                {
                    string _orderPart = item;
                    _orderPart = Regex.Replace(_orderPart, @"\s+", " ");
                    string[] _orderArry = _orderPart.Split(' ');
                    string _orderField = _orderArry[0];
                    bool sort = isAsc;
                    if (_orderArry.Length == 2)
                    {
                        isAsc = _orderArry[1].ToUpper() == "ASC" ? true : false;
                    }
                    var parameter = Expression.Parameter(typeof(TEntity), "t");
                    var property = typeof(TEntity).GetProperty(_orderField);
                    var propertyAccess = Expression.MakeMemberAccess(parameter, property);
                    var orderByExp = Expression.Lambda(propertyAccess, parameter);
                    resultExp = Expression.Call(typeof(Queryable), isAsc ? "OrderBy" : "OrderByDescending", new Type[] { typeof(TEntity), property.PropertyType }, tempData.Expression, Expression.Quote(orderByExp));
                }
            tempData = tempData.Provider.CreateQuery<TEntity>(resultExp);
            pagination.records = await tempData.CountAsync();
            tempData = tempData.Skip<TEntity>(pagination.rows * (pagination.page - 1)).Take<TEntity>(pagination.rows).AsQueryable();
            return await tempData.ToListAsync();
        }

        public IDbContextTransaction BeginTrans()
        {
            return dbcontext.Database.BeginTransaction();
        }

        public void CommitTrans(IDbContextTransaction trans)
        {
            this.dbcontext.Database.CommitTransaction();
        }

        public void RollbackTrans()
        {
            this.dbcontext.Database.RollbackTransaction();
        }
    }
}
