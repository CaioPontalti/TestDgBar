using Clearsale.Domain.Models;
using Clearsale.Domain.Queries;
using Clearsale.Share.DTO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Clearsale.Domain.Interfaces
{
    public interface IOrderRepository
    {
        Task Save(Order order);
        Task<int> GetProductAmount(int order, int productId);
        Task<GroupOrderProduct> GetProductHasDiscount(int order, int productId);
        Task SaveDiscount(int? id, int product, double value);
        Task SaveDiscountProduct(int? id, int productId );
        Task ClearOrder(int order);
        Task<bool> ExistsOrder(int order);
        Task SaveFree(int order, int? product);
        Task SaveFreeDiscount(int? order, int product, double value);
        Task<int> GetProductAmountFree(int order, int productId);
        Task UpdateFreeAmount(int order);
        Task<orderResult> PayOrder(int order);

    }
}
