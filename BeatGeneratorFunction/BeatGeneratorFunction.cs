using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace BeatGeneratorFunction
{
    public class Function1
    {
        private readonly ILogger<Function1> _logger;
        private readonly string _baseApiUrl;
        private readonly HttpClient _httpClient;

        public Function1(ILogger<Function1> logger, IConfiguration config)
        {
            _logger = logger;
            _baseApiUrl = config["BaseApiUrl"] ?? "";
            _httpClient = new HttpClient();

        }

        // Part III i : Azure Function to call beatgenerator api
        [Function("BeatGeneratorFunction")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get",
            Route = "beatgenerator/{i:int}/{j:int}")] HttpRequest req, int i, int j)
        {
            _logger.LogInformation("BeatGeneratorFunction processed a request.");

            var apiUrl = $"{_baseApiUrl}/Beat/generator/{i}/{j}";
            HttpResponseMessage res;

            try
            {
                res = await _httpClient.GetAsync(apiUrl);
            }
            catch (HttpRequestException e)
            {
                _logger.LogError($"Request error: {e.Message}");
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }

            if (res.IsSuccessStatusCode)
            {
                var content = await res.Content.ReadAsStringAsync();
                return new OkObjectResult(content);
            }

            else
            {
                _logger.LogError($"API call failed with status code {res.StatusCode}");
                return new StatusCodeResult((int)res.StatusCode);
            }
        }
    }
}
