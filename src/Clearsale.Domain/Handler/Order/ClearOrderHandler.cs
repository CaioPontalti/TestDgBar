using Clearsale.Domain.Commands.Order;
using Clearsale.Domain.Contracts;
using Clearsale.Domain.Core;
using Clearsale.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Clearsale.Domain.Handles.Order
{
    public class ClearOrderHandler : ICommandHandler<ClearOrderCommand>
    {
        private readonly IOrderRepository _orderRepository;

        public ClearOrderHandler(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }


        public async Task<ResponseCommand> Handle(ClearOrderCommand command)
        {
            command.Validate();
            if (command.Invalid)
                return await Task.FromResult(new ResponseCommand(false, "Dados inválidos", command.Notifications));
            
            if(!await _orderRepository.ExistsOrder(command.OrderNumber))
                return await Task.FromResult(new ResponseCommand(false, "Número de comanda não existe", command.OrderNumber));


            await _orderRepository.ClearOrder(command.OrderNumber);

            return await Task.FromResult(new ResponseCommand(true, "Comanda resetada com sucesso!", command.Notifications));
        }
    }
}
