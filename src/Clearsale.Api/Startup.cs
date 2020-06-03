
using Clearsale.Api.Config.Ioc;
using Clearsale.Api.Config.Jwt;
using Clearsale.Api.Config.Swagger;
using Clearsale.Domain.Handler.Order;
using Clearsale.Domain.Handles.Order;
using Clearsale.Domain.Handles.User;
using Clearsale.Domain.Interfaces;
using Clearsale.Infra.Context;
using Clearsale.Infra.Repositories;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Clearsale.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            services.AddIocConfig(); //Ioc

            services.AddJwt(Configuration); //JWT
            
            services.AddSwaggerConfig();//Swagger

        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                
                app.UseHsts();
            }

            app.UseHttpsRedirection();

            app.UseSwagger();
            app.UseSwaggerUI(s =>
            {
                s.SwaggerEndpoint("/swagger/v1/swagger.json", "Bar do DG API v1");
            });

            app.UseCors(x => x
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader());

            app.UseAuthentication();

            app.UseMvc();

        }
    }
}
