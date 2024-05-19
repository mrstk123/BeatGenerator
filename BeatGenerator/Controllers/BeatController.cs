using Microsoft.AspNetCore.Mvc;

namespace BeatGenerator.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BeatController : ControllerBase
    {
        // Part I: API
        private static readonly Dictionary<int, string> DefaultMappings = new()
        {
            { 1, "Low Floor Tom" },
            { 3, "kick" },
            { 4, "snare" },
            { 12, "Hi-Hat" }
        };

        private static Dictionary<int, string> Mappings = new(DefaultMappings);

        // Part I.i: Beat Generator Endpoint
        [HttpGet("generator/{i}/{j}")]
        public IActionResult GenerateBeat(int i, int j)
        {
            if (i <= j)
                return BadRequest("i must be greater than j.");

            var result = new List<string>();
            for (int k = i; k >= j; k--)
            {
                if (k % 12 == 0)
                    result.Add(Mappings[12]); // Hi-Hat
                else if (k % 4 == 0)
                    result.Add(Mappings[4]); // snare
                else if (k % 3 == 0)
                    result.Add(Mappings[3]); // kick
                else
                    result.Add(Mappings[1]); // Low Floor Tom
            }

            return Ok(result);
        }

        // Part I.ii: Health Check Endpoints
        [HttpGet("livez")]
        public IActionResult Livez()
        {
            return Ok();
        }

        [HttpGet("readyz")]
        public IActionResult Readyz()
        {
            // Logic to check if the server is ready
            bool isReady = true;

            if (isReady)
            {
                return Ok();
            }
            else
            {
                return StatusCode(503);
            }
        }

        // Part I.iii: Configuration Endpoint
        [HttpPost("configure/{i}/{text}")]
        public IActionResult Configure(int i, string text)
        {
            var validKeys = new List<int> { 1, 3, 4, 12 };
            var validTexts = new List<string> { "snare", "kick", "Hi-Hat", "Low Floor Tom", "cymbal", "Low-Mid Tom", "Bass Drum" };

            if (!validKeys.Contains(i) || !validTexts.Contains(text))
                return BadRequest("Invalid configuration");

            Mappings[i] = text;
            return Ok();
        }

        // Part I.iv: Reset Endpoint
        [HttpPost("reset")]
        public IActionResult Reset()
        {
            Mappings = new(DefaultMappings);
            return Ok();
        }
    }
}
