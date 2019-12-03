using BooksBorrow.Domain.Core.Commands;
using System;
using System.Collections.Generic;
using System.Text;

namespace BooksBorrow.Domain.Core
{
    public interface ICommandBus
    {
        void Send<T>(T command) where T : ICommand;
    }
}
