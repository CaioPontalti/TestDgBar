using Clearsale.Domain.Interfaces;
using Clearsale.Domain.Models;
using Clearsale.Infra.Context;
using Dapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Clearsale.Infra.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly DataContext _context;

        public ProductRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Product>> GetAll()
        {
            try
            {
                using (var sqlConn = _context.OpenConn())
                {
                    var result = await sqlConn.QueryAsync<Product>(
                        @"SELECT 
	                    Id,
	                    Name,
	                    Value,
                        Value_Discount AS ValueDiscount
                    FROM [products]");

                    return result;
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public async Task<Product> GetById(int id)
        {
            try
            {
                using (var sqlConn = _context.OpenConn())
                {
                    var result = await sqlConn.QueryFirstOrDefaultAsync<Product>(
                        @"SELECT 
	                    Id,
	                    Name,
	                    Value,
                        Value_Discount AS ValueDiscount
                    FROM [products]
                    WHERE Id = @id", new { id});

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
