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
      var loggerFactory = LoggerFactory.Create(builder => builder.AddDebug());
      var httpClient = new HttpClient();
      var sleeperConfig = new SleeperConfigurationModel();
      sleeperConfig.Season = 2023;
      sleeperConfig.Sport = "nfl";
      sleeperConfig.Version = "v1";
      sleeperConfig.RostersEndpoint = "rosters";
      sleeperConfig.LeaguesEndpoint = "league";
      sleeperConfig.TransactionsEndpoint = "transactions";
      sleeperConfig.UserEndpoint = "user";
      sleeperConfig.BaseUrl = "https://api.sleeper.app";
      sleeperConfig.UserId = "871988413874761728";

      var trawlerConfig = new TrawlConfigurationModel();
      trawlerConfig.MaxTrades = 1000;
      trawlerConfig.NumWeeks = 17;
      trawlerConfig.MaxTrawlDepth = 10;
      trawlerConfig.TrawlRateLimit = -1;
      var trawler = new LeagueTrawler(sleeperConfig, trawlerConfig, httpClient, new LoggingReporter(loggerFactory));
      await trawler.Trawl();
   }
}