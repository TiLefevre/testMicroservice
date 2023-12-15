using Microsoft.OpenApi.Models;
using MassTransit;
using Stock.Consumer;
using Stock.Data;
using Stock.Data.Interfaces;
using Stock.Repositories;
using Stock.Repositories.Interfaces;

namespace Stock;

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
        services.AddSwaggerGen(c => { c.SwaggerDoc("v1", new OpenApiInfo { Title = "Stock.API", Version = "v1" }); });
        
        services.AddMassTransit(config =>
        {
            config.AddConsumer<ProductConsumer>();
            
            config.UsingRabbitMq((ctx, cfg) =>
            {
                cfg.Host("amqp://guest:guest@rabbitmq:5672");
                
                cfg.ReceiveEndpoint("product-queue", c =>
                {
                    c.ConfigureConsumer<ProductConsumer>(ctx);
                });
            });
        });
        services.AddMassTransitHostedService();

        services.AddScoped<IStockContext, StockContext>();
        services.AddScoped<IStockRepository, StockRepository>();
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        
        app.UseDeveloperExceptionPage();
        app.UseSwagger();
        app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Stock.API v1"));
        

        app.UseRouting();
        app.UseAuthorization();

        app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
    }
}