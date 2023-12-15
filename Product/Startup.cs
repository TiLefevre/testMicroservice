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

        services.AddScoped<IProductContext, ProductContext>();
        services.AddScoped<IProductRepository, ProductRepository>();

        services.AddMassTransit(config =>
        {
            config.UsingRabbitMq((ctx, cfg) =>
            {
                cfg.Host("amqp://guest:guest@localhost:5672");
            });
        });
        services.AddMassTransitHostedService();
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        
        app.UseDeveloperExceptionPage();
        app.UseSwagger();
        app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Product.API v1"));
        

        app.UseRouting();
        app.UseAuthorization();

        app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        
        // var bus = Bus.Factory.CreateUsingRabbitMq(config =>
        // {
        //     config.Host("amqp://guest:guest@localhost:5672");
        //     
        //     config.ReceiveEndpoint("temp-queue", c =>
        //     {
        //         c.Handler<Entities.Product>(context =>
        //         {
        //             return Console.Out.WriteLineAsync($"Received: {context.Message.Name}");
        //         });
        //     });
        // });
        //
        // bus.Start();
        // bus.Publish(new Entities.Product { Name = "Test" });
    }
}