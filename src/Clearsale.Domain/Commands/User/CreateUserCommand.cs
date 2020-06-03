using Clearsale.Domain.Contracts;
using FluentValidator;
using FluentValidator.Validation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Clearsale.Domain.Commands.User
{
    public class CreateUserCommand : Notifiable, ICommand
    {
        
        public string Email { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }

        public CreateUserCommand(string email, string password, string confirmPassword)
        {
            Email = email;
            Password = password;
            ConfirmPassword = confirmPassword;
        }

        public void Validate()
        {
            if (Password != ConfirmPassword)
                AddNotification(new Notification("Register", "Os campos de usuário e senha não confemrem"));

            AddNotifications(
              new ValidationContract()
                   .Requires()
                   .HasMinLen(Password, 3, "Password", " A senha deve ter no mínimo 3 caracteres!")
                   .IsNotNullOrEmpty(Email, Email, "Email obrigatório")
           ); ;
        }
    }
}
