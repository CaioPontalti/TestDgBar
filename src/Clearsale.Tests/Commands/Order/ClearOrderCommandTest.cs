using Clearsale.Domain.Commands.Order;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace Clearsale.Tests.Commands.Order
{
    [TestClass]
    public class ClearOrderCommandTest
    {
        [TestMethod]
        public void Dado_um_commamd_clear_order_invalido()
        {
            var command = new ClearOrderCommand();
            command.Validate();

            Assert.AreEqual(command.Valid, false);
        }

        [TestMethod]
        public void Dado_um_commamd_clear_order_valido()
        {
            var command = new ClearOrderCommand();
            command.OrderNumber = 1;
            command.Validate();

            Assert.AreEqual(command.Valid, true);
        }

        [TestMethod]
        public void Dado_um_commamd_clear_order_zero()
        {
            var command = new ClearOrderCommand();
            command.OrderNumber = 0;
            command.Validate();

            Assert.AreEqual(command.Valid, false);
        }
    }
}
