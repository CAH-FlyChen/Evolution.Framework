/*******************************************************************************
 * Copyright © 2016 Evolution.Framework 版权所有
 * Author: Evolution
 * Description: Evolution快速开发平台
 * Website：
*********************************************************************************/
using Evolution.Framework;
using Microsoft.AspNetCore.Http;
using Evolution.Domain.Entity.SystemSecurity;
using System;
using System.Collections.Generic;
using System.Linq;
using Evolution.Data;
using Evolution.Domain.IRepository.SystemSecurity;

namespace Evolution.Application.SystemSecurity
{
    public class LogApp
    {
        private ILogRepository service = null;
        public LogApp(ILogRepository service)
        {
            this.service = service;
        }
        public List<LogEntity> GetList(Pagination pagination, string queryJson)
        {
            var expression = ExtLinq.True<LogEntity>();
            var queryParam = queryJson.ToJObject();
            if (!queryParam["keyword"].IsEmpty())
            {
                string keyword = queryParam["keyword"].ToString();
                expression = expression.And(t => t.Account.Contains(keyword));
            }
            if (!queryParam["timeType"].IsEmpty())
            {
                string timeType = queryParam["timeType"].ToString();
                DateTime startTime = DateTime.Now.ToString("yyyy-MM-dd").ToDate();
                DateTime endTime = DateTime.Now.ToString("yyyy-MM-dd").ToDate().AddDays(1);
                switch (timeType)
                {
                    case "1":
                        break;
                    case "2":
                        startTime = DateTime.Now.AddDays(-7);
                        break;
                    case "3":
                        startTime = DateTime.Now.AddMonths(-1);
                        break;
                    case "4":
                        startTime = DateTime.Now.AddMonths(-3);
                        break;
                    default:
                        break;
                }
                expression = expression.And(t => t.Date >= startTime && t.Date <= endTime);
            }
            return service.FindList(expression, pagination);
        }
        public void RemoveLog(string keepTime)
        {
            DateTime operateTime = DateTime.Now;
            if (keepTime == "7")            //保留近一周
            {
                operateTime = DateTime.Now.AddDays(-7);
            }
            else if (keepTime == "1")       //保留近一个月
            {
                operateTime = DateTime.Now.AddMonths(-1);
            }
            else if (keepTime == "3")       //保留近三个月
            {
                operateTime = DateTime.Now.AddMonths(-3);
            }
            var expression = ExtLinq.True<LogEntity>();
            expression = expression.And(t => t.Date <= operateTime);
            service.Delete(expression);
        }
        public void WriteDbLog(bool result, string resultLog,HttpContext context)
        {
            var userCode = context.User.Claims.First(t => t.Type == OperatorModelClaimNames.UserCode).Value;
            var userName = context.User.Claims.First(t => t.Type == OperatorModelClaimNames.UserName).Value;

            LogEntity logEntity = new LogEntity();
            logEntity.Id = Common.GuId();
            logEntity.Date = DateTime.Now;
            logEntity.Account = userCode;
            logEntity.NickName = userName;
            //logEntityIPAddress = Net.Ip;
            logEntity.IPAddress = Net.GetIp(context);
            logEntity.IPAddressName = Net.GetLocation(logEntity.IPAddress);
            logEntity.Result = result;
            logEntity.Description = resultLog;
            logEntity.Create(context);
            service.Insert(logEntity);
        }
        public void WriteDbLog(LogEntity logEntity,HttpContext context)
        {
            logEntity.Id = Common.GuId();
            logEntity.Date = DateTime.Now;
            logEntity.IPAddress = "117.81.192.182";
            logEntity.IPAddressName = Net.GetLocation(logEntity.IPAddress);
            logEntity.Create(context);
            service.Insert(logEntity);
        }
    }
}
