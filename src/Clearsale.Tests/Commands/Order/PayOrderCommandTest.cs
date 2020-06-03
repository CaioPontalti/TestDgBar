using Clearsale.Domain.Commands.Order;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace Clearsale.Tests.Commands.Order
{
    [TestClass]
    public class PayOrderCommandTest
    {
        [TestMethod]
        public void Dado_um_commamd_pay_order_invalido()
        {
            var command = new PayOrderCommand(0);
            command.Validate();

            Assert.AreEqual(command.Valid, false);
        }

        [TestMethod]
        public void Dado_um_commamd_pay_order_valido()
        {
            var command = new PayOrderCommand(1);
            command.Validate();

            Assert.AreEqual(command.Valid, true);
        }
    }
}
