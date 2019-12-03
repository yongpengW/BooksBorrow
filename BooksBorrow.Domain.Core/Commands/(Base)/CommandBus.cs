using BooksBorrow.Domain.Core.CommandHandlers;
using BooksBorrow.Domain.Core.DomainEvent.EventHandlers;
using BooksBorrow.InjectFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace BooksBorrow.Domain.Core.Commands
{
    public class CommandBus: ICommandBus
    {
        static CommandBus()
        {
            // 订阅域事件处理程序

            Type domainEventHandlerBaseType = typeof(DomainEventHandler<>);

            var domainEventHandlers = domainEventHandlerBaseType.Assembly.GetExportedTypes()
                .Where(t => t.BaseType != null && t.BaseType.IsGenericType && t.BaseType.GetGenericTypeDefinition() == domainEventHandlerBaseType)
                .ToList();

            foreach (var h in domainEventHandlers)
            {
                Common.Logger.Logger.LogDebug(typeof(CommandBus), String.Format("Register Event Handler: {0}", h.FullName));

                object instance = h.Assembly.CreateInstance(h.FullName);

                MethodInfo subscribeMethod = h.GetMethod("Subscribe");

                subscribeMethod.Invoke(instance, null);
            }
        }

        public CommandBus()
        {
        }

        public void Send<T>(T command) where T : ICommand
        {
            var handler = InjectContainer.GetInstance<ICommandHandler<T>>();

            if (handler != null)
            {
                try
                {
                    handler.Execute(command);
                }
                catch (Exception ex)
                {
                    throw (ex);
                }
                finally
                {
                    handler.Dispose();
                }
            }
            else
            {
                //Cannot find command handler for type
                throw new InvalidOperationException(string.Format("无法找到类型的命令处理程序 '{0}'", typeof(T).FullName));
            }
        }
    }
}
