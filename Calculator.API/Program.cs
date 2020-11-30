using Microsoft.AspNetCore.Hosting;
using Serilog;

public class Program
{
    public static void Main()
    {
        var host = new WebHostBuilder()
            .UseKestrel()
            .UseIISIntegration()
            .UseStartup<Startup>()
            .UseSerilog()
            .Build();

        host.Run();
    }
}