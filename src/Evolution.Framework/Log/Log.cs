// * Copyright © 2016 Evolution.Framework 版权所有
// * Author: Evolution
// * Description: Evolution快速开发平台
// * Website：
//*********************************************************************************/
using System;
using Microsoft.Extensions.Logging;
///*******************************************************************************
namespace Evolution.Framework
{
    public class Logger: ILogger
    {
        public Logger(ILogger log)
        {
            
        }
        public void Debug(object message)
        {
            //this.logger.(message);
        }
        public void Error(object message)
        {
            //this.logger.Error(message);
        }
        public void Info(object message)
        {
            //this.logger.Info(message);
        }
        public void Warn(object message)
        {
            //this.logger.Warn(message);
        }

        IDisposable ILogger.BeginScope<TState>(TState state)
        {
            throw new NotImplementedException();
        }

        bool ILogger.IsEnabled(LogLevel logLevel)
        {
            throw new NotImplementedException();
        }

        void ILogger.Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
        {
            throw new NotImplementedException();
        }
    }
}
