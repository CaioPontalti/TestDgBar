using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Clearsale.Domain.Interfaces;
using Clearsale.Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Clearsale.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductRepository _productRepository;
        public ProductsController(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        [HttpGet]
        [Route("return-products")]
        [Authorize]
        public async Task<IEnumerable<Product>> GetAll()
        {
            try
            {
                var result = await _productRepository.GetAll();

                return result;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

        }

    }
}