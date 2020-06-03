using Clearsale.Domain.Contracts;
using FluentValidator;
using FluentValidator.Validation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Clearsale.Domain.Commands.Order
{
    public class ClearOrderCommand : Notifiable, ICommand
    {
        public int OrderNumber { get; set; }
        public void Validate()
        {
            AddNotifications(
             new ValidationContract()
                  .Requires()
                  .IsGreaterThan(OrderNumber, 0, "OrderNumber", "Número da comanda é obrigatório")
          );
        }
    }
}
