using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using RenaStore.Domain.StoreContext.Handlers;
using RenaStore.Domain.StoreContext.Repositories;
using RenaStore.Domain.StoreContext.Services;
using RenaStore.Infra.StoreContext.DataContexts;
using RenaStore.Infra.StoreContext.Repositories;
using RenaStore.Infra.StoreContext.Services;
using Elmah.Io.AspNetCore;
using System;

namespace RenaStore.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            // services.AddResponseCompression();

            services.AddScoped<RenaDataContext, RenaDataContext>();
            services.AddTransient<ICustomerRepository, CustomerRepository>();
            services.AddTransient<IEmailService, EmailService>();
            services.AddTransient<CustomerHandler, CustomerHandler>();

            services.AddSwaggerGen(x =>
            {
                x.SwaggerDoc("v1", new OpenApiInfo { Title = "Rena Store", Version = "v1" });
            });

            services.AddElmahIo(o =>
            {
                o.ApiKey = "82e178c3d5834b2fa170432463727d8e";
                o.LogId = new Guid("aab27950-75a1-4068-9d8f-e146232da669");
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseAuthorization();

            // app.UseResponseCompression();

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Rena Store - V1");
            });

            app.UseElmahIo();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

        }
    }
}
