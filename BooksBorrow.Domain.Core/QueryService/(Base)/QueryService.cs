using System;
using System.Collections.Generic;
using BooksBorrow.Domain.Core.Interface.IQueryDataAccessor;
using System.Text;
using BooksBorrow.AppConfiguration;
using BooksBorrow.InjectFramework;

namespace BooksBorrow.Domain.Core.QueryService
{
    public partial class QueryService : IQueryService
    {
        protected IQueryDataAccessor DataAccessor { get; private set; }

        protected IConfigurationReader Reader { get; set; }
        public QueryService()
        {
            Reader = InjectContainer.GetInstance<IConfigurationReader>();
            DataAccessor = InjectContainer.GetInstance<IQueryDataAccessor>(new Dictionary<string, object>()
            {
                {"connectionString", Reader.ConnectionString}
            });
        }
        public void Dispose()
        {
            DataAccessor.Dispose();
        }
    }
}
