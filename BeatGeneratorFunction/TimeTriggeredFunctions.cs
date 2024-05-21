using System.Text;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace BeatGeneratorFunction
{
    public class TimeTriggeredFunctions
    {
        private readonly ILogger _logger;
        private readonly string _baseApiUrl;
        private readonly HttpClient _httpClient;

        public TimeTriggeredFunctions(ILogger<TimeTriggeredFunctions> logger, IConfiguration config)
        {
            _logger = logger;
            _baseApiUrl = config["BaseApiUrl"] ?? "";
            _httpClient = new HttpClient();
        }

        // Part III i : Time-triggered Azure Function
        [Function("MorningTrigger")]
        public async Task MorningTrigger([TimerTrigger("0 0 8 * * *")] TimerInfo myTimer)
        {
            _logger.LogInformation($"MorningTrigger function executed at: {DateTime.Now}");
            await UpdateConfiguration(1, "Bass Drum");
            await UpdateConfiguration(3, "kick");
            await UpdateConfiguration(4, "snare");
            await UpdateConfiguration(12, "cymbal");
        }

        [Function("LunchTrigger")]
        public async Task LunchTrigger([TimerTrigger("0 30 12 * * *")] TimerInfo myTimer)
        {
            _logger.LogInformation($"LunchTrigger function executed at: {DateTime.Now}");
            await UpdateConfiguration(1, "Low-Mid Tom");
            await UpdateConfiguration(3, "snare");
            await UpdateConfiguration(4, "Hi-Hat");
            await UpdateConfiguration(12, "cymbal");
        }

        [Function("EveningTrigger")]
        public async Task EveningTrigger([TimerTrigger("0 0 17 * * *")] TimerInfo myTimer)
        {
            _logger.LogInformation($"EveningTrigger function executed at: {DateTime.Now}");
            await ResetConfiguration();
        }

        private async Task UpdateConfiguration(int i, string text)
        {
            var url = $"{_baseApiUrl}/Beat/configure/{i}/{text}";
            var content = new StringContent("", Encoding.UTF8, "application/json");

            HttpResponseMessage response;
            try
            {
                response = await _httpClient.PostAsync(url, content);
            }
            catch (HttpRequestException e)
            {
                _logger.LogError($"Request error: {e.Message}");
                return;
            }

            if (!response.IsSuccessStatusCode)
            {
                _logger.LogError($"API call failed with status code {response.StatusCode}");
            }
        }

        private async Task ResetConfiguration()
        {
            var url = $"{_baseApiUrl}/Beat/reset";
            HttpResponseMessage response;
            try
            {
                response = await _httpClient.PostAsync(url, null);
            }
            catch (HttpRequestException e)
            {
                _logger.LogError($"Request error: {e.Message}");
                return;
            }

            if (!response.IsSuccessStatusCode)
            {
                _logger.LogError($"API call failed with status code {response.StatusCode}");
            }
        }
    }
}