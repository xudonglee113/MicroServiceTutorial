using Microsoft.Extensions.Configuration;
using PlatformService.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace PlatformService.SyncDataService.Http
{
    public class CommandDataClient : ICommandDataClient
    {
        private readonly HttpClient _httpclient;
        private readonly IConfiguration _configuration;

        public CommandDataClient(HttpClient httpClient,IConfiguration configuration)
        {
            _httpclient = httpClient;
            _configuration = configuration;
        }
        public async Task SendPlatformToCommand(PlatformReadDto plat)
        {
            var content = new StringContent(
                JsonSerializer.Serialize(plat), Encoding.UTF8, "application/json"
                );

            try
            {
                Console.WriteLine($"{_configuration.GetSection("CommandService")}");
                var results = await _httpclient.PostAsync($"{_configuration["CommandService"]}", content);
                if (results.IsSuccessStatusCode)
                {
                    Console.WriteLine("Post to command success");
                }
                else
                {
                    Console.WriteLine("Post to command failed");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

        }
    }
}
