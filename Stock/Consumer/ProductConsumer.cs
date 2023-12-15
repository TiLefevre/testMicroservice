using MassTransit;
using Stock.Entities;

namespace Stock.Consumer;

internal class ProductConsumer : IConsumer<Product>
{
    private readonly ILogger<ProductConsumer> logger;
    
    public ProductConsumer(ILogger<ProductConsumer> logger)
    {
        this.logger = logger;
    }
    
    public async Task Consume(ConsumeContext<Product> context)
    {
        await Console.Out.WriteLineAsync($"Product received: {context.Message.Name}");
        logger.LogInformation($"Got new message {context.Message.Name}");
    }
}