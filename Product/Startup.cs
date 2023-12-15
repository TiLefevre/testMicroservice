using Product.Data;
using Product.Repositories;
using Product.Repositories.Interfaces;
using Microsoft.OpenApi.Models;
using Product.Data.Interfaces;
using MassTransit;
using Microsoft.AspNetCore.Builder;

namespace Product;

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
        services.AddSwaggerGen(c => { c.SwaggerDoc("v1", new OpenApiInfo { Title = "Product.API", Version = "v1" }); });
        
        services.AddMassTransit(config =>
        {
            config.UsingRabbitMq((ctx, cfg) =>
            {
                cfg.Host("rabbitmq://localhost:15672");
            });
        });
        services.AddMassTransitHostedService();

        services.AddScoped<IProductContext, ProductContext>();
        services.AddScoped<IProductRepository, ProductRepository>();
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        
        app.UseDeveloperExceptionPage();
        app.UseSwagger();
        app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Product.API v1"));
        

        app.UseRouting();
        Console.WriteLine("test");
        app.UseAuthorization();

        app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
    }
}