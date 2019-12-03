using BooksBorrow.AppConfiguration;
using BooksBorrow.Common.Logger;
using BooksBorrow.Common.Utils;
using BooksBorrow.Domain.Core;
using BooksBorrow.Domain.Core.Commands;
using BooksBorrow.Domain.Core.QueryService;
using BooksBorrow.InjectFramework;
using System;
using System.Collections.Generic;
using System.Text;

namespace BooksBorrow.RuntimeConfiguration
{
    public abstract class RuntimeSettingBase
    {
        public void Inject()
        {
            BeforeInject();
            InjectLibraries();
            InjectCommonLibraries();
            AfterInject();
        }

        protected virtual void AfterInject()
        {
        }

        protected virtual void InjectCommonLibraries()
        {
            InjectContainer.RegisterType<IQueryService, QueryService>();
            InjectContainer.RegisterInstance<ICommandBus>(new CommandBus());
        }

        protected abstract void InjectLibraries();

        protected virtual void BeforeInject()
        {
            InjectContainer.RegisterType<IConfigurationReader, ConfigurationReader>();
            InjectContainer.RegisterInstance<ILogHelper>(new Log4netHelper());
            #if DEBUG
            InjectContainer.RegisterInstance<IStringEncrypter>(new PlainTextEncrypter());
            #else
            InjectContainer.RegisterInstance<IStringEncrypter>(new StringEncrypter());
            #endif
        }
    }
}
