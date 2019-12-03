using BooksBorrow.Common.Logger;
using BooksBorrow.Database.POCO.DataAccess.Domain;
using BooksBorrow.Database.POCO.DataAccess.Query;
using BooksBorrow.Domain.Core.CommandHandlers;
using BooksBorrow.Domain.Core.Commands;
using BooksBorrow.Domain.Core.Interface;
using BooksBorrow.Domain.Core.Interface.IQueryDataAccessor;
using BooksBorrow.InjectFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BooksBorrow.RuntimeConfiguration
{
    public class ProductionRuntimeSetting: RuntimeSettingBase
    {
        protected override void InjectLibraries()
        {
            InjectContainer.RegisterType<IDomainDataAccessor, DomainDbDataAccessor>();
            InjectContainer.RegisterType<IQueryDataAccessor, QueryDbDataAccessor>();
            RegisterCommandHandlers();
        }

        private void RegisterCommandHandlers()
        {
            Func<Type, bool> isCommandHandler = i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(ICommandHandler<>);

            var commandHandlers = typeof(ICommandHandler<>).Assembly.GetExportedTypes()
                .Where(t => t.GetInterfaces().Any(isCommandHandler))
                .ToList();

            var registerSource = commandHandlers.Select(h =>
            {
                return new { FromType = h.GetInterfaces().First(isCommandHandler), ToType = h };
            }).ToList();

            foreach (var r in registerSource)
            {
                Logger.LogDebug(typeof(CommandBus), String.Format("Register Command Handler: {0}", r.ToType.FullName));
                InjectContainer.RegisterType(r.FromType, r.ToType);
            }
        }
    }
}
