using Clearsale.Domain.Interfaces;
using Clearsale.Domain.Models;
using Clearsale.Domain.Queries;
using Clearsale.Infra.Context;
using Clearsale.Share.DTO;
using Dapper;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Clearsale.Infra.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly DataContext _context;

        public OrderRepository(DataContext context)
        {
            _context = context;
        }
        public async Task Save(Order order)
        {

            try
            {
                using (var sqlConn = _context.OpenConn())
                {
                    await sqlConn.ExecuteScalarAsync<User>(
                        @"INSERT INTO [order] VALUES (@Order_Number, @Product_Id, @Value, @Has_Discount)",
                        new
                        {
                            @Order_Number = order.OrderNumber,
                            @Product_Id = order.ProductId,
                            @Value = order.Value,
                            @Has_Discount = order.HasDiscount
                        }
                    );
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public async Task<int> GetProductAmount(int order, int productId)
        {
            try
            {
                using (var sqlConn = _context.OpenConn())
                {
                    var result = await sqlConn.ExecuteScalarAsync<int>(
                        @"SELECT 
	                        COUNT(*)
                        FROM [order]
                        WHERE Order_Number = @order
                        AND Product_Id = @productId", new { order, productId });

                    return result;
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public async Task<GroupOrderProduct> GetProductHasDiscount(int order, int productId)
        {
            try
            {
                using (var sqlConn = _context.OpenConn())
                {
                    var result = await sqlConn.QueryFirstOrDefaultAsync<GroupOrderProduct>(
                        @"SELECT 
                                TOP 1 Order_Number, 
                                      Product_Id
                          FROM [order] A 
                          WHERE Order_Number = @order 
                          AND Product_Id = @productId
                          AND Has_Discount = 0", new { order, productId });

                    return result;
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public async Task SaveDiscount(int? id, int product, double value)
        {
            try
            {
                using (var sqlConn = _context.OpenConn())
                {
                    await sqlConn.ExecuteScalarAsync(
                    @"INSERT INTO [order_discount] VALUES (@Id, @product, @value)",
                        new { @Id = id, @product = product, @value = value });

                    await sqlConn.ExecuteScalarAsync(
                      @"UPDATE [order]
	                    SET Has_Discount = 1
                      WHERE Id = @id",
                        new { @Id = id });

                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public async Task SaveDiscountProduct(int? id, int productId)
        {
            try
            {
                using (var sqlConn = _context.OpenConn())
                {
                    await sqlConn.ExecuteScalarAsync(
                    @"UPDATE [order]
	                SET Has_Discount = 1
                  WHERE Order_Number = @id
                  AND Product_Id = @productId",
                        new { @Id = id, @productId = productId });

                }
            }
            catch (Exception e)
            {
                throw e;
            }
            
        }

        public async Task ClearOrder(int order)
        {

            try
            {
                using (var sqlConn = _context.OpenConn())
                {
                    await sqlConn.ExecuteScalarAsync(
                        @"DELETE FROM [order]          WHERE Order_Number = @order
                          DELETE FROM [order_discount] WHERE [order] = @order
                          DELETE FROM [order_free]     WHERE [order] = @order",
                            new { @order = order });
                }
            }
            catch (Exception e)
            {
                throw e;
            }
            
        }

        public async Task<bool> ExistsOrder(int order)
        {
            try
            {
                using (var sqlConn = _context.OpenConn())
                {
                    var result = await sqlConn.QueryFirstOrDefaultAsync<bool>(
                        @"SELECT COUNT(*)
                        FROM [order]
                        WHERE Order_Number = @order",
                            new { @order = order });

                    return result;  
                }
            }
            catch (Exception e)
            {

                throw e;
            }

            
        }

        public async Task SaveFree(int order, int? product)
        {
            try
            {
                using (var sqlConn = _context.OpenConn())
                {
                    if (!await ExistsOrderFree(order, product))
                    {
                        await sqlConn.ExecuteScalarAsync(
                            @"INSERT INTO [order_free] VALUES (@order, @product, @amount)",
                            new { @order = order, @product = product, @amount = 1 });
                    }
                    else
                    {
                        await sqlConn.ExecuteScalarAsync(
                            @"UPDATE [order_free]
	                         SET [amount] = [amount] + 1
                          WHERE [order] = @order
                          AND product = @product",
                        new { @order = order, @product = product });
                    }
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public async Task SaveFreeDiscount(int? order, int product, double value)
        {
            try
            {
                using (var sqlConn = _context.OpenConn())
                {
                    await sqlConn.ExecuteScalarAsync(
                    @"INSERT INTO [order_discount] VALUES (@Id, @product, @value)",
                        new { @Id = order, @product = product, @value = value });
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public async Task<int> GetProductAmountFree(int order, int productId)
        {
            try
            {
                using (var sqlConn = _context.OpenConn())
                {
                    var result = await sqlConn.ExecuteScalarAsync<int>(
                        @"SELECT 
	                        amount
                        FROM [order_free]
                        WHERE [order] = @order
                        AND product = @productId", new { order, productId });

                    return result;
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        private async Task<bool> ExistsOrderFree(int order, int? product)
        {
            try
            {
                using (var sqlConn = _context.OpenConn())
                {
                   
                    var result = await sqlConn.QueryFirstOrDefaultAsync<bool>(
                        @"SELECT COUNT(*)
                        FROM [order_free]
                        WHERE [order] = @order
                        AND product = @product",
                            new { @order = order, @product = product });

                    return result;
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public async Task UpdateFreeAmount(int order)
        {
            try
            {
                using (var sqlConn = _context.OpenConn())
                {
                    await sqlConn.ExecuteScalarAsync(
                    @"UPDATE [order_free]
	                    SET [amount] = [amount] - 3 
                        WHERE [order] = @order
                        AND product = 2",
                        new { @order = order });

                    await sqlConn.ExecuteScalarAsync(
                    @"UPDATE [order_free]
	                    SET [amount] = [amount] - 2 
                        WHERE [order] = @order
                        AND product = 1",
                        new { @order = order });
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public async Task<orderResult> PayOrder(int order)
        {
            try
            {
                using (var sqlConn = _context.OpenConn())
                {
                    var result = new orderResult();
                   
                    result.Items = await sqlConn.QueryAsync<orderItems>(
                        @"  IF OBJECT_ID('tempdb..#temp') IS NOT NULL
	                        drop table #temp

                        IF OBJECT_ID('tempdb..#temp') IS NOT NULL
	                        drop table #Temp2

                        SELECT 
	                            O.Order_Number,
	                            O.Product_Id,
	                            P.Name Product,
	                            COUNT(*) AS Amount,
	                            SUM(P.VALUE) AS Total_Pay
                        INTO #Temp
                        FROM [order] O
                        JOIN [products] P
	                        ON O.Product_Id = P.Id
                        WHERE O.[Order_Number] = @order
                        GROUP BY O.Order_Number, O.Product_Id, P.Name


                        SELECT 
	                        [order] as [Order],
	                        [product] as [Product],
	                        sum(value) as Value
                        INTO #Temp2
                        FROM [order_discount]
                        WHERE [order] = @order
                        group by [order], product

                        SELECT 
                            A.Order_Number,
	                        A.product ,
	                        A.Amount,
	                        A.Total_Pay AS Total_Item,
	                        isnull(B.[value],0) AS Discount,
	                        A.Total_Pay - isnull(B.[value],0) AS Total_Pay
                        FROM #temp A
                        LEFT JOIN #temp2 B
	                        ON A.product_id = B.product
                            
                        DELETE Pay_Order WHERE Order_Number = @order

                        INSERT INTO Pay_Order 
                        SELECT A.Order_Number,
	                        SUM(Total_Pay) 
                        FROM #temp A
                        LEFT JOIN #temp2 B
	                        ON A.product_id = B.product
                        WHERE [Order_Number] = @order
                        group by A.Order_Number", new { @order = order }
                    ) as List<orderItems>;

                    result.Total_Order = await sqlConn.ExecuteScalarAsync<double>(
                        @"SELECT
	                        Value_Pay AS Total_Order
                        FROM Pay_Order
                        WHERE Order_Number = @order", new { @order = order }
                    );

                    return result;
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
