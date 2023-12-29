using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using EnergyHR.Services;

public class Program
{
    public static async Task Main(string[] args)
    {
        await new HostBuilder()
            .ConfigureServices((hostContext, services) =>
            {
                services.AddHostedService<HostService>();
            })
            .RunConsoleAsync();

        Console.ReadLine();
    }
}