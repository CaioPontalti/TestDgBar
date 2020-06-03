using Clearsale.Domain.Commands.User;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace Clearsale.Tests.Commands.User
{
    [TestClass]
    public class CreateUserCommandTest
    {
        [TestMethod]
        public void Dado_um_commamd_user_invalido()
        {
            var command = new CreateUserCommand("user@user.com", "1234", "12345");
            command.Validate();

            Assert.AreEqual(command.Valid, false);
        }

        [TestMethod]
        public void Dado_um_commamd_user_valido()
        {
            var command = new CreateUserCommand("user@user.com", "1234", "1234");
            command.Validate();

            Assert.AreEqual(command.Valid, true);
        }

        [TestMethod]
        public void Dado_um_commamd_user_sem_email()
        {
            var command = new CreateUserCommand("", "1234", "1234");
            command.Validate();

            Assert.AreEqual(command.Valid, false);
        }
    }
}
