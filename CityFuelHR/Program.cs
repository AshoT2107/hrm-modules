using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using CityFuelHR.Services;
using System.Net.NetworkInformation;
using System.Net;
using Microsoft.IdentityModel.Logging;

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