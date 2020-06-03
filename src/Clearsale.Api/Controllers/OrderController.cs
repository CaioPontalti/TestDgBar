using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Clearsale.Domain.Commands.Order;
using Clearsale.Domain.Core;
using Clearsale.Domain.Handler.Order;
using Clearsale.Domain.Handles.Order;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Clearsale.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly CreateOrderHandler _handlerCreate;
        private readonly ClearOrderHandler _handlerClear;
        private readonly PayOrderHandler _handlerPay;

        public OrderController(CreateOrderHandler handlerCreate, ClearOrderHandler handlerClear, PayOrderHandler handlerPay)
        {
            _handlerCreate = handlerCreate;
            _handlerClear = handlerClear;
            _handlerPay = handlerPay;
        }

        [HttpPost]
        [Route("include-product")]
        [Authorize]
        public async Task<ResponseCommand> Create([FromBody] CreateOrderCommand command)
        {
            try
            {
                var result = await _handlerCreate.Handle(command);

                return result;
            }
            catch (Exception e)
            {
                throw e;
            }

        }

        [HttpPost]
        [Route("clear-order")]
        [Authorize]
        public async Task<ResponseCommand> Clear([FromBody] ClearOrderCommand command)
        {
            try
            {
                var result = await _handlerClear.Handle(command);

                return result;
            }
            catch (Exception e)
            {
                throw e;
            }

        }

        [HttpPost]
        [Route("pay-order")]
        [Authorize]
        public async Task<ResponseCommand> Pay([FromBody] PayOrderCommand command)
        {
            try
            {
                var result = await _handlerPay.Handle(command);

                return result;
            }
            catch (Exception e)
            {
                throw e;
            }

        }
    }
}