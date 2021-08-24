using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Microsoft.EntityFrameworkCore;

using ProjectWs03.src.shared.database.contexts;
using ProjectWs03.src.shared.database.utils;
using ProjectWs03.src.modules.customers.repositories;
using ProjectWs03.src.modules.customers.services;
using ProjectWs03.src.modules.products.repositories;
using ProjectWs03.src.modules.products.services;
using ProjectWs03.src.modules.orders.repositories;
using ProjectWs03.src.modules.orders.services;

namespace ProjectWs03
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
      services.AddDbContext<SqlServerContext>(options => {
        options.UseSqlServer(
          Configuration.GetConnectionString("SqlServerConnection")
        );

        options.LogTo(Console.WriteLine).EnableSensitiveDataLogging();
      });

      SqlServerService sqlServerService = new SqlServerService(services);

      services.AddSingleton(
        typeof(ICustomersRepository), 
        new CustomersService(sqlServerService)
      );

      services.AddSingleton(
        typeof(IProductsRepository), 
        new ProductsService(sqlServerService)
      );

      services.AddSingleton(
        typeof(IOrdersRepository), 
        new OrdersService(sqlServerService)
      );

      services.AddControllers();
      services.AddSwaggerGen(c =>
      {
        c.SwaggerDoc("v1", new OpenApiInfo { Title = "ProjectWs03", Version = "v1" });
      });
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
      if (env.IsDevelopment())
      {
        app.UseDeveloperExceptionPage();
        app.UseSwagger();
        app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "ProjectWs03 v1"));
      }

      app.UseHttpsRedirection();

      app.UseRouting();

      app.UseAuthorization();

      app.UseEndpoints(endpoints =>
      {
        endpoints.MapControllers();
      });
    }
  }
}
