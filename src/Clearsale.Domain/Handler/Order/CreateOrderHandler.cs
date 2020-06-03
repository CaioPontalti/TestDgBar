using Clearsale.Domain.Commands.Order;
using Clearsale.Domain.Contracts;
using Clearsale.Domain.Core;
using Clearsale.Domain.Interfaces;
using Clearsale.Domain.Models;
using Microsoft.AspNetCore.Http.Features;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Clearsale.Domain.Handles.Order
{
    public class CreateOrderHandler : ICommandHandler<CreateOrderCommand>
    {
        private readonly IProductRepository _productRepository;
        private readonly IOrderRepository _orderRepository;

        public CreateOrderHandler(IProductRepository productRepository, IOrderRepository orderRepository)
        {
            _productRepository = productRepository;
            _orderRepository = orderRepository;
        }

        public async Task<ResponseCommand> Handle(CreateOrderCommand command)
        {
            bool hasDiscount = false;
            bool hasUpdateFree = false;
            command.Validate();
            if (command.Invalid)
                return await Task.FromResult(new ResponseCommand(false, "Dados inválidos", command.Notifications));

            if(command.ProductId == 3)
            {
                if(await _orderRepository.GetProductAmount(command.OrderNumber, command.ProductId) >= 3)
                    return await Task.FromResult(new ResponseCommand(false, "Não foi possível inserir o item. Máximo de 3 sucos por comanda", new { }));

                dynamic orderId = await _orderRepository.GetProductHasDiscount(command.OrderNumber, 1);
                if (orderId != null)
                {
                    await _orderRepository.SaveDiscount(orderId.Order_Number, orderId.Product_Id,  3.00);
                    await _orderRepository.SaveDiscountProduct(orderId.Order_Number, orderId.Product_Id);
                    hasDiscount = true;
                }
            }
            else if (command.ProductId == 1)
            {
                dynamic orderNumber = await _orderRepository.GetProductHasDiscount(command.OrderNumber, 3);
                if (orderNumber != null)
                {
                    await _orderRepository.SaveDiscount(orderNumber.Order_Number, command.ProductId, 3.00);
                    hasDiscount = true;
                }

                await _orderRepository.SaveFree(command.OrderNumber, command.ProductId);

                if (await _orderRepository.GetProductAmountFree(command.OrderNumber, 2) >= 3)
                {
                    if (await _orderRepository.GetProductAmountFree(command.OrderNumber, 1) >= 2)
                    {
                        await _orderRepository.SaveFreeDiscount(command.OrderNumber, 4, 70.00);
                        hasUpdateFree = true;
                    }
                }
                
                if (hasUpdateFree)
                    await _orderRepository.UpdateFreeAmount(command.OrderNumber);

            }
            else if (command.ProductId == 2) 
            {
                if (await _orderRepository.GetProductAmountFree(command.OrderNumber, 2) >= 2)
                {
                    if (await _orderRepository.GetProductAmountFree(command.OrderNumber, 1) > 1)
                    {
                        await _orderRepository.SaveFreeDiscount(command.OrderNumber, 4, 70.00);
                        hasUpdateFree = true;
                    }
                }

                await _orderRepository.SaveFree(command.OrderNumber, command.ProductId);
                if (hasUpdateFree)
                    await _orderRepository.UpdateFreeAmount(command.OrderNumber);
            }

            var product = await _productRepository.GetById(command.ProductId);

            var order = new Models.Order(command.OrderNumber, command.ProductId, product.Value, hasDiscount);

            await _orderRepository.Save(order);

            return await Task.FromResult(new ResponseCommand(true, "Item incluído", new { }));
        }
    }
}
