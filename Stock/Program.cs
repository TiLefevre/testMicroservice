using System.Net;

namespace Stock;

public class Program
{
    public static void Main(string[] args)
    {
        CreateHostBuilder(args).Build().Run();
    }

    public static IHostBuilder CreateHostBuilder(string[] args)
    {
        // return Host.CreateDefaultBuilder(args)
        //     .ConfigureWebHostDefaults(webBuilder => { webBuilder.UseStartup<Startup>(); });
        return Host.CreateDefaultBuilder(args)
            .ConfigureWebHostDefaults(webBuilder =>
            {
                webBuilder.ConfigureKestrel(serverOptions =>
                {
                    serverOptions.Listen(IPAddress.Any, 5001);
                });
                webBuilder.UseStartup<Startup>();
            });
    }
}