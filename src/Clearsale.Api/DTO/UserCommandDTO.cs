using Clearsale.Domain.Contracts;
using FluentValidator;
using FluentValidator.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;

namespace Clearsale.Api.DTO
{
    public class UserCommandDTO : Notifiable, ICommand
    {
        public string Email { get; set; }
        public string Password { get; set; }

        public void Validate()
        {
            AddNotifications(
              new ValidationContract()
                   .Requires()
                   .IsNullOrEmpty(Email, Email, " Campo e-mail é obrigatório")
                   .IsNullOrEmpty(Password, Password, " Campo senha é obrigatório")
           );
        }
    }
}
