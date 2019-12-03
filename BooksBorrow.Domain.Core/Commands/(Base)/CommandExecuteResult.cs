using BooksBorrow.Domain.Core.DomainModels.Identities;
using BooksBorrow.Domain.Core.Errors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BooksBorrow.Domain.Core.Commands
{
    public class CommandExecuteResult
    {
        private List<BusinessError> executeErrors;

        public bool Success { get; set; }

        public IdentityBase DomainModelId { get; private set; }

        public IEnumerable<BusinessError> Errors
        {
            get
            {
                return executeErrors.AsEnumerable();
            }
        }

        public CommandExecuteResult()
        {
            this.executeErrors = new List<BusinessError>();
        }

        public void ExecuteSuccess()
        {
            this.Success = true;
        }
        public void ExecuteSuccess(IdentityBase id)
        {
            ExecuteSuccess();

            this.DomainModelId = id;
        }
        public void ExecuteFail(BusinessError error)
        {
            this.Success = false;

            AddError(error);
        }

        public void ExecuteFail(IEnumerable<BusinessError> errors)
        {
            this.Success = false;

            AddErrors(errors);
        }

        public void AddError(BusinessError error)
        {
            this.executeErrors.Add(error);
        }

        public void RemoveError(BusinessError error)
        {
            this.executeErrors.Remove(error);
        }

        public void AddErrors(IEnumerable<BusinessError> errors)
        {
            this.executeErrors.AddRange(errors);
        }
    }
}
