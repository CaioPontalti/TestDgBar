using Clearsale.Domain.Core;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Clearsale.Domain.Contracts
{
    public interface ICommandHandler<T> where T : ICommand
    {
        Task<ResponseCommand> Handle(T command);
    }
}
