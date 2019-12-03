using System;
using System.Collections.Generic;
using System.Text;

namespace BooksBorrow.Common.Logger
{
    public interface ILogHelper
    {
        void WriteError(Type type, string message);

        void WriteError(object instance, string message);

        void WriteDebug(Type type, string message);

        void WriteDebug(object instance, string message);

        void WriteInfo(Type type, string message);

        void WriteInfo(object instance, string message);
    }
}
