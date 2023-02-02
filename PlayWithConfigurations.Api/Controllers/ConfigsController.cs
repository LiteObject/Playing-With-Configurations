using Microsoft.AspNetCore.Mvc;
using System.Text;

namespace PlayWithConfigurations.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ConfigsController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        private readonly ILogger<ConfigsController> _logger;

        public ConfigsController(ILogger<ConfigsController> logger, IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;

            _logger.LogInfo($"Instantiated ${nameof(ConfigsController)}");
        }

        [HttpGet]
        public IActionResult Get()
        {
            using (_logger.CreateScope("--- Serving the GET request ---"))
            {
                IConfigurationRoot configRoot = (IConfigurationRoot)_configuration;

                _logger.LogInfo("Environment.GetEnvironmentVariable(\"MY_ENV_VARIABLE\"): " + Environment.GetEnvironmentVariable("MY_ENV_VARIABLE"));
                _logger.LogInfo("configRoot[\"MY_ENV_VARIABLE\"]: " + configRoot["MY_ENV_VARIABLE"]);

                string debugView = configRoot.GetDebugView();
                return Ok(debugView);
            }
        }

        [HttpGet("/providers")]
        public IActionResult GetProviders()
        {
            IConfigurationRoot root = (IConfigurationRoot)_configuration;

            StringBuilder str = new();

            foreach (IConfigurationProvider? provider in root.Providers.ToList())
            {
                _ = str.AppendLine(provider.ToString());
            }

            return Ok(str.ToString());
        }
    }
}