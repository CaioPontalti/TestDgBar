using Clearsale.Domain.Handler.Order;
using Clearsale.Domain.Handles.Order;
using Clearsale.Domain.Handles.User;
using Clearsale.Domain.Interfaces;
using Clearsale.Infra.Context;
using Clearsale.Infra.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Clearsale.Api.Config.Ioc
{
    public static class IocConfig
    {
        public static IServiceCollection AddIocConfig(this IServiceCollection services)
        {
            services.AddScoped<DataContext>();

            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IOrderRepository, OrderRepository>();

            services.AddScoped<UserHandler>();
            services.AddScoped<CreateOrderHandler>();
            services.AddScoped<ClearOrderHandler>();
            services.AddScoped<PayOrderHandler>();

            return services;
        }
    }
}
