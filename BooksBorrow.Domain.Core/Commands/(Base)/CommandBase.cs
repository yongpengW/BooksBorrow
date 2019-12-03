using BooksBorrow.Domain.Core.DomainModels.Identities;
using BooksBorrow.Domain.Core.Errors;
using System;
using System.Collections.Generic;
using System.Text;

namespace BooksBorrow.Domain.Core.Commands
{
    public class CommandBase<T>: ICommand where T : CommandExecuteResult, new()
    {
        public T ExecuteResult { get; protected set; }

        public CommandBase()
        {
            this.ExecuteResult = new T();
        }

        public void ExecuteSuccess()
        {
            this.ExecuteResult.ExecuteSuccess();
        }
        public void ExecuteSuccess(IdentityBase id)
        {
            this.ExecuteResult.ExecuteSuccess(id);
        }
        public void ExecuteFail(BusinessError error)
        {
            this.ExecuteResult.ExecuteFail(error);
        }

        public void ExecuteFail(IEnumerable<BusinessError> errors)
        {
            this.ExecuteResult.ExecuteFail(errors);
        }
    }
}
