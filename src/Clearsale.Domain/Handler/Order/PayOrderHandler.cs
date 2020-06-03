using Clearsale.Domain.Commands.Order;
using Clearsale.Domain.Contracts;
using Clearsale.Domain.Core;
using Clearsale.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading.Tasks;

namespace Clearsale.Domain.Handler.Order
{
    public class PayOrderHandler : ICommandHandler<PayOrderCommand>
    {
        private readonly IOrderRepository _orderRepository;

        public PayOrderHandler(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public async Task<ResponseCommand> Handle(PayOrderCommand command)
        {
            command.Validate();
            if (command.Invalid)
                return await Task.FromResult(new ResponseCommand(false, "Dados inválidos", command.Notifications));

            if (!await _orderRepository.ExistsOrder(command.OrderNumber))
                return await Task.FromResult(new ResponseCommand(false, "Número de comanda não encontrado", command.Notifications));

            var result = await _orderRepository.PayOrder(command.OrderNumber);

            return new ResponseCommand(true, "Informações da comanda", result);
        }
    }
}
