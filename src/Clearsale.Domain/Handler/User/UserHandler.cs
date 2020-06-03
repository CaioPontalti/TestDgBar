using Clearsale.Domain.Commands.User;
using Clearsale.Domain.Contracts;
using Clearsale.Domain.Core;
using Clearsale.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Clearsale.Domain.Handles.User
{
    public class UserHandler : ICommandHandler<CreateUserCommand>
    {
        private readonly IUserRepository _userRepository;

        public UserHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<ResponseCommand> Handle(CreateUserCommand command)
        {
            command.Validate();
            if (command.Invalid)
                return await Task.FromResult(new ResponseCommand(false, "Dados inválidos", command.Notifications));

            var user = new Models.User(command.Email, command.Password);

            await _userRepository.Save(user);

            return await Task.FromResult( 
                new ResponseCommand(true, "Usuário Cadastrado", new { user.Id, user.Email })
            );
        }

    }
}
