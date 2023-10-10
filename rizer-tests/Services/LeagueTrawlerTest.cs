using Microsoft.Extensions.Logging;
using rizer.Model;
using rizer.Services;
using rizer.Services.LeagueTrawlerReporter;

namespace rizer_tests.Services;

public class LeagueTrawlerTest
{
   [Test]
   public async Task testTrawler()
   {
      var loggerFactory = LoggerFactory.Create(builder => builder.AddConsole());
      var httpClient = new HttpClient();
      var config = new SleeperConfigurationModel();
      config.Season = 2023;
      config.Sport = "nfl";
      config.Version = "v1";
      config.RostersEndpoint = "rosters";
      config.LeaguesEndpoint = "league";
      config.UserEndpoint = "user";
      config.BaseUrl = "https://api.sleeper.app";
      config.UserId = "871988413874761728";
      var trawler = new LeagueTrawler(config, httpClient, new LoggingReporter(loggerFactory));
      await trawler.Trawl();
   }
}