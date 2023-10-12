using Microsoft.AspNetCore.Mvc;
using rizer.Model;
using rizer.Model.Sleeper;

namespace rizer.Controller;

[ApiController]
[Route("api/[controller]")]
public class LeaguesController : ControllerBase
{
    private readonly ILogger<LeaguesController> _logger;
    private readonly HttpClient _httpClient;
    private readonly SleeperConfigurationModel _sleeperConfig;

    public LeaguesController(ILoggerFactory loggerFactory, HttpClient httpClient, SleeperConfigurationModel sleeperConfig)
    {
        _logger = loggerFactory.CreateLogger<LeaguesController>();
        _httpClient = httpClient;
        _sleeperConfig = sleeperConfig;
    }

    [HttpGet]
    public async Task<IList<LeagueModel>> Get()
    {
        _logger.LogInformation("{}", _sleeperConfig.UserLeaguesUri(_sleeperConfig.UserId));
        var response = await _httpClient.GetAsync(_sleeperConfig.UserLeaguesUri(_sleeperConfig.UserId));
        response.EnsureSuccessStatusCode();
        var json = await response.Content.ReadAsStringAsync();
        return SleeperParser.ParseLeagues(json);
    }
}