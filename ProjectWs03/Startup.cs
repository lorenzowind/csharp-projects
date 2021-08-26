using System;
using System.Text;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.OpenApi.Models;
using Microsoft.EntityFrameworkCore;

using ProjectWs03.src.shared.database.contexts;
using ProjectWs03.src.shared.database.utils;
using ProjectWs03.src.modules.customers.repositories;
using ProjectWs03.src.modules.orders.repositories;
using ProjectWs03.src.modules.products.repositories;
using ProjectWs03.src.modules.sessions.repositories;

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

        // options.LogTo(Console.WriteLine).EnableSensitiveDataLogging();
      });

      services
        .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)  
        .AddJwtBearer(options =>  
        {  
          options.TokenValidationParameters = new TokenValidationParameters  
          {  
            ValidateIssuer = true,  
            ValidateAudience = true,  
            ValidateLifetime = true,  
            ValidateIssuerSigningKey = true,  
            ValidIssuer = "https://localhost:5001",  
            ValidAudience = "https://localhost:5001",  
            IssuerSigningKey = new SymmetricSecurityKey(
              Encoding.UTF8.GetBytes("22e59f9f9052eebb28b09e45e34105cb@1234")
            )  
          };  
        });  

      SqlServerService sqlServerService = new SqlServerService(services);

      services.AddSingleton(
        typeof(ICustomersRepository), 
        new CustomersRepository(sqlServerService)
      );

      services.AddSingleton(
        typeof(IOrdersRepository), 
        new OrdersRepository(sqlServerService)
      );

      services.AddSingleton(
        typeof(IProductsRepository), 
        new ProductsRepository(sqlServerService)
      );

      services.AddSingleton<ISessionsRepository, SessionsRepository>();

      services.AddControllers();
      services.AddSwaggerGen(c =>
      {
        c.SwaggerDoc("v1", new OpenApiInfo { Title = "ProjectWs03", Version = "v1" });
      });

      services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
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

      app.UseAuthentication();

      app.UseAuthorization();

      app.UseEndpoints(endpoints =>
      {
        endpoints.MapControllers();
      });
    }
  }
}
