using BooksBorrow.Domain.Core.Commands;
using System;
using System.Collections.Generic;
using System.Text;

namespace BooksBorrow.Domain.Core.CommandHandlers
{
    public interface ICommandHandler<TCommand> : IDisposable where TCommand : ICommand
    {
        void Execute(TCommand command);
    }
}
