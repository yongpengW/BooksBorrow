using BooksBorrow.AppConfiguration;
using BooksBorrow.Common.Utils;
using BooksBorrow.Domain.Core.Interface;
using BooksBorrow.InjectFramework;
using System;
using System.Collections.Generic;
using System.Text;

namespace BooksBorrow.Domain.Core.CommandHandlers
{
    public abstract class CommandHandlerBase : IDisposable
    {
        protected IDomainDataAccessor DataAccessor { get; set; }

        protected IConfigurationReader Reader { get; set; }

        protected DateTime ExecuteDateTime { get; set; }

        public CommandHandlerBase()
        {
            this.Reader = InjectContainer.GetInstance<IConfigurationReader>();
            this.DataAccessor = InjectContainer.GetInstance<IDomainDataAccessor>(new Dictionary<string, object>()
            {
                {"connectionString", Reader.ConnectionString}
            });

            this.ExecuteDateTime = Utility.GetCurrentDateTime();
        }

        public virtual void Dispose()
        {
            this.DataAccessor.Dispose();
            this.DataAccessor = null;
        }
    }
}
