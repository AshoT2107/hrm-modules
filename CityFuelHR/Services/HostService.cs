using CityFuelHR.Handler;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Logging;
using System.Net;
using System.Net.NetworkInformation;
using Telegram.Bot;
using Telegram.Bot.Polling;
using Telegram.Bot.Types.Enums;

namespace CityFuelHR.Services
{
    public class HostService : BackgroundService
    {
        private const string token = "your_token";
        private readonly ITelegramBotClient _botClient;
        private readonly UpdateHandler _handler;
        public HostService()
        {
            HttpClient httpClient = new HttpClient();

            var lan = IPGlobalProperties.GetIPGlobalProperties();
            if (lan.DomainName == "your_domain")
            {
                httpClient = new HttpClient(new HttpClientHandler()
                {
                    Proxy = new WebProxy("your_proxy_address", false)
                    {
                        Credentials = new NetworkCredential("login", "password")
                    },
                    UseProxy = false
                });
            }
            _botClient = new TelegramBotClient(token, httpClient);

            _handler = new UpdateHandler();
        }
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            await Task.Factory.StartNew(() =>
            {
                _botClient.StartReceiving(
                    updateHandler: _handler.HandleUpdateAsync,
                    pollingErrorHandler: _handler.HandlePollingErrorAsync,
                    receiverOptions: new ReceiverOptions()
                    {
                        AllowedUpdates = []
                    },
                    cancellationToken: stoppingToken);
            }, stoppingToken);
        }
    }
}
