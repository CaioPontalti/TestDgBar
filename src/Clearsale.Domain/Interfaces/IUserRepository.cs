using Clearsale.Domain.Commands.User;
using Clearsale.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Clearsale.Domain.Interfaces
{
    public interface IUserRepository
    {
        Task Save(User user);
        Task<IEnumerable<User>> GetAll();
        Task<User> GetUser(User user);
    }
}
