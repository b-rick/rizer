using rizer.Model.Sleeper;

namespace rizer.Services.LeagueTrawlerReporter
{
    public interface ILeagueTrawlerReporter
    {
        void OnNewLeague(LeagueModel leagueModel);

        void OnNewRoster(int rosterId);
    }

    public class LoggingReporter : ILeagueTrawlerReporter
    {
        private readonly ILogger _logger;
        public LoggingReporter(ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger<LoggingReporter>();
        }
        
        public void OnNewLeague(LeagueModel leagueModel)
        {
            _logger.LogInformation("Found league - {}:{} ", leagueModel.Name, leagueModel.LeagueId); 
        }

        public void OnNewRoster(int rosterId)
        {
            throw new NotImplementedException();
        }
    }

    public class NoopReporter : ILeagueTrawlerReporter
    {
        public void OnNewLeague(LeagueModel leagueModel)
        {
        }

        public void OnNewRoster(int rosterId)
        {
        }
    }
}