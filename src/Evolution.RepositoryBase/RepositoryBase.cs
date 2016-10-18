/*******************************************************************************
 * Copyright © 2016 Evolution.Framework 版权所有
 * Author: Evolution
 * Description: Evolution快速开发平台
 * Website：
*********************************************************************************/
using Evolution.IRepository;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text.RegularExpressions;
using Microsoft.EntityFrameworkCore;
using Evolution.Framework;
using Evolution.Data;
using Evolution.Data.Extensions;
using Microsoft.EntityFrameworkCore.Storage;
using System.Threading.Tasks;

namespace Evolution.Repository
{
    /// <summary>
    /// 仓储实现
    /// </summary>
    public class RepositoryBase : IRepositoryBase, IDisposable
    {
        private DbContext dbcontext = null;

        public RepositoryBase(DbContext ctx)
        {
            dbcontext = ctx;
        }

        private IDbContextTransaction dbTransaction { get; set; }
        public IRepositoryBase BeginTrans()
        {
            DbConnection dbConnection = dbcontext.Database.GetDbConnection();
            if (dbConnection.State == ConnectionState.Closed)
            {
                dbConnection.Open();
            }
            dbTransaction = dbcontext.Database.BeginTransaction();
            return this;
        }
        public Task<int> CommitAsync()
        {
            try
            {
                //此处保存不能异步SaveChangesAsync。因为Commit是同步的，这样就会导致save没有执行就执行了commit方法。
                var returnValue = dbcontext.SaveChanges();
                if (dbTransaction != null)
                {
                    dbTransaction.Commit();
                }
                return Task.FromResult(0);
            }
            catch (Exception ex)
            {
                if (dbTransaction != null)
                {
                    this.dbTransaction.Rollback();
                }
                throw;
            }
            finally
            {
                this.Dispose();
            }
        }
        public void Dispose()
        {
            if (dbTransaction != null)
            {
                this.dbTransaction.Dispose();
            }
            this.dbcontext.Dispose();
        }
        public Task<int> InsertAsync<TEntity>(TEntity entity) where TEntity : class
        {
            dbcontext.Entry<TEntity>(entity).State = EntityState.Added;

            return dbTransaction == null ? this.CommitAsync() : Task.FromResult(0);
        }
        public Task<int> InsertAsync<TEntity>(List<TEntity> entitys) where TEntity : class
        {
            foreach (var entity in entitys)
            {
                dbcontext.Entry<TEntity>(entity).State = EntityState.Added;
            }
            return dbTransaction == null ? this.CommitAsync() : Task.FromResult(0);
        }
        public Task<int> UpdateAsync<TEntity>(TEntity entity) where TEntity : class
        {
            dbcontext.Set<TEntity>().Attach(entity);
            PropertyInfo[] props = entity.GetType().GetProperties();
            foreach (PropertyInfo prop in props)
            {
                if (prop.GetValue(entity, null) != null && prop.Name!="Id")
                {
                    if (prop.GetValue(entity, null).ToString() == "&nbsp;")
                        dbcontext.Entry(entity).Property(prop.Name).CurrentValue = null;
                    dbcontext.Entry(entity).Property(prop.Name).IsModified = true;
                }
            }
            return dbTransaction == null ? this.CommitAsync() : Task.FromResult(0);
        }
        public Task<int> DeleteAsync<TEntity>(TEntity entity) where TEntity : class
        {
            dbcontext.Set<TEntity>().Attach(entity);
            dbcontext.Entry<TEntity>(entity).State = EntityState.Deleted;
            return dbTransaction == null ? this.CommitAsync() : Task.FromResult(0);
        }
        public Task<int> DeleteAsync<TEntity>(Expression<Func<TEntity, bool>> predicate) where TEntity : class
        {
            var entitys = dbcontext.Set<TEntity>().Where(predicate).ToList();
            entitys.ForEach(m => dbcontext.Entry<TEntity>(m).State = EntityState.Deleted);
            return dbTransaction == null ? this.CommitAsync() : Task.FromResult(0);
        }
        public Task<TEntity> FindEntityAsync<TEntity>(object keyValue) where TEntity : class
        {
            return dbcontext.Set<TEntity>().FindAsync(keyValue);
        }
        public Task<TEntity> FindEntityASync<TEntity>(Expression<Func<TEntity, bool>> predicate) where TEntity : class
        {
            return dbcontext.Set<TEntity>().FirstOrDefaultAsync(predicate);
        }
        public IQueryable<TEntity> IQueryable<TEntity>() where TEntity : class
        {
            return dbcontext.Set<TEntity>();
        }
        public IQueryable<TEntity> IQueryable<TEntity>(Expression<Func<TEntity, bool>> predicate) where TEntity : class
        {
            return dbcontext.Set<TEntity>().Where(predicate);
        }
        public Task<List<TEntity>> FindListAsync<TEntity>(string strSql) where TEntity : class
        {
            return dbcontext.Set<TEntity>().FromSql(strSql).ToListAsync<TEntity>();
        }
        public Task<List<TEntity>> FindListAsync<TEntity>(string strSql, DbParameter[] dbParameter) where TEntity : class
        {
            return dbcontext.Set<TEntity>().FromSql<TEntity>(strSql, dbParameter).ToListAsync<TEntity>();
        }
        public Task<List<TEntity>> FindListAsync<TEntity>(Pagination pagination) where TEntity : class,new()
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
        public Task<List<TEntity>> FindListAsync<TEntity>(Expression<Func<TEntity, bool>> predicate, Pagination pagination) where TEntity : class,new()
        {
            bool isAsc = pagination.sord.ToLower() == "asc" ? true : false;
            string[] _order = pagination.sidx.Split(',');
            MethodCallExpression resultExp = null;
            var tempData = dbcontext.Set<TEntity>().Where(predicate);
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
    }
}
