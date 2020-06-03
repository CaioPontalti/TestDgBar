using Clearsale.Domain.Contracts;
using FluentValidator;
using FluentValidator.Validation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Clearsale.Domain.Commands.Order
{
    public class CreateOrderCommand : Notifiable, ICommand
    {

        public int OrderNumber { get; set; }
        public int ProductId { get; set; }

        public void Validate()
        {
            if (ProductId > 4)
                AddNotification(new Notification("ProductId", "O id do produto deve ser entre 1 e 4. Consulte os produtos disponives."));

            AddNotifications(
              new ValidationContract()
                   .Requires()
                   .IsGreaterThan(OrderNumber, 0, "OrderNumber", "Número da comanda é obrigatório")
           );
        }
    }
}
