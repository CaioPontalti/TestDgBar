using Clearsale.Domain.Commands.Order;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace Clearsale.Tests.Commands.Order
{
    [TestClass]
    public class CreateORderCommandTest
    {
        [TestMethod]
        public void Dado_um_commamd_create_order_invalido()
        {
            var command = new CreateOrderCommand();
            command.Validate();

            Assert.AreEqual(command.Valid, false);
        }

        [TestMethod]
        public void Dado_um_commamd_create_order_produto_maior_que_permitido()
        {
            var command = new CreateOrderCommand();
            command.OrderNumber = 1;
            command.ProductId = 5;
            command.Validate();

            Assert.AreEqual(command.Valid, false);
        }

        [TestMethod]
        public void Dado_um_commamd_create_order_sem_comanda()
        {
            var command = new CreateOrderCommand();
            command.ProductId = 1;
            command.Validate();

            Assert.AreEqual(command.Valid, false);
        }

        [TestMethod]
        public void Dado_um_commamd_create_order_ok()
        {
            var command = new CreateOrderCommand();
            command.OrderNumber = 1;
            command.ProductId = 1;
            command.Validate();

            Assert.AreEqual(command.Valid, true);
        }
    }
}
