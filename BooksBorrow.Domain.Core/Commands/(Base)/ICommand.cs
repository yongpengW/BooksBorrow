using BooksBorrow.Domain.Core.DomainModels.Identities;
using BooksBorrow.Domain.Core.Errors;
using System.Collections.Generic;
using System.Text;

namespace BooksBorrow.Domain.Core.Commands
{
    public interface ICommand
    {
        void ExecuteSuccess();

        void ExecuteSuccess(IdentityBase id);

        void ExecuteFail(BusinessError error);

        void ExecuteFail(IEnumerable<BusinessError> errors);
    }
}
