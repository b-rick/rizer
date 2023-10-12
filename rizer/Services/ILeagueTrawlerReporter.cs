using rizer.Model.Sleeper;

namespace rizer.Services.LeagueTrawlerReporter
{
    public interface ILeagueTrawlerReporter
    {
        void OnNewLeague(LeagueModel leagueModel);

        void OnNewRoster(RosterModel rosterModel);

        void OnNewTransaction(TransactionsModel transactionsModel);
    }

    public class LoggingReporter : ILeagueTrawlerReporter
    {
        private readonly ILogger _logger;
        public LoggingReporter(ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger<LoggingReporter>();
            _logger.LogInformation("Hello world!");
        }
        
        public void OnNewLeague(LeagueModel leagueModel)
        {
            _logger.LogInformation("Found league - {}:{} ", leagueModel.Name, leagueModel.LeagueId); 
        }

        public void OnNewRoster(RosterModel rosterModel)
        {
            _logger.LogInformation("Found roster - {}:{} ", rosterModel.OwnerId, rosterModel.RosterMetadata); 
        }

        public void OnNewTransaction(TransactionsModel transactionsModel)
        {
            if (transactionsModel.TransactionType == "trade")
            {
                _logger.LogInformation("Found trade - {}", transactionsModel.TransactionId);
            }
            else
            {
                _logger.LogInformation("Found other_transaction - {}", transactionsModel.TransactionId);
            }
        }
    }

    public class NoopReporter : ILeagueTrawlerReporter
    {
        public void OnNewLeague(LeagueModel leagueModel)
        {
        }

        public void OnNewRoster(RosterModel rosterModel)
        {
        }

        public void OnNewTransaction(TransactionsModel transactionsModel)
        {
        }
    }
}