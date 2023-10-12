using rizer.Model;
using rizer.Model.Sleeper;
using rizer.Services.LeagueTrawlerReporter;

namespace rizer.Services;

public class LeagueTrawler
{
    private readonly ISet<string> _leagueIds = new HashSet<string>();
    private readonly ISet<string> _ownerIds = new HashSet<string>();
    
    private readonly SleeperConfigurationModel _sleeperConfig;
    private readonly TrawlConfigurationModel _trawlerConfig;
    private readonly HttpClient _httpClient;
    private readonly ILeagueTrawlerReporter _reporter;
    private int _depth;

    public LeagueTrawler(SleeperConfigurationModel sleeperConfig, TrawlConfigurationModel trawlerConfig,
        HttpClient httpClient, ILeagueTrawlerReporter reporter)
    {
        _depth = 0;
        
        _sleeperConfig = sleeperConfig;
        _trawlerConfig = trawlerConfig;
        _httpClient = httpClient;
        _reporter = reporter;
    }

    public async Task Trawl()
    {
        await Trawl(_sleeperConfig.UserId, 0);
    }

    private async Task Trawl(string userId, int depth)
    {
        if (depth > _trawlerConfig.MaxTrawlDepth)
        {
            return;
        }
        if (!_ownerIds.Add(userId))
        {
            return;
        }
        var response = await _httpClient.GetAsync(_sleeperConfig.UserLeaguesUri(userId));
        response.EnsureSuccessStatusCode();
        var json = await response.Content.ReadAsStringAsync();
        var leagues = SleeperParser.ParseLeagues(json);
        Task.WaitAll(leagues.Select(i => Trawl(i, depth)).ToArray());
    }
    

    private async Task Trawl(LeagueModel leagueModel, int depth)
    {
        _reporter.OnNewLeague(leagueModel);
        if (!_leagueIds.Add(leagueModel.LeagueId))
        {
            return;
        }

        await FindTrades(leagueModel);
        // Recurse into each roster 
        var response = await _httpClient.GetAsync(_sleeperConfig.RostersUri(leagueModel.LeagueId));
        response.EnsureSuccessStatusCode();
        var json = await response.Content.ReadAsStringAsync();
        var rosters = SleeperParser.ParseRosters(json);
        Task.WaitAll(rosters.Select(i => Trawl(i, depth)).ToArray());
    }

    private async Task Trawl(RosterModel rosterModel, int depth)
    {
        _reporter.OnNewRoster(rosterModel);
        await Trawl(rosterModel.OwnerId, depth + 1);
    }

    private async Task FindTrades(LeagueModel leagueModel)
    {
        var myEnumerable = Enumerable.Range(0, _trawlerConfig.NumWeeks);
        Task.WaitAll(myEnumerable.Select(week => FindTrade(leagueModel, week)).ToArray());
    }

    private async Task FindTrade(LeagueModel leagueModel, int week)
    {
        var response = await _httpClient.GetAsync(_sleeperConfig.TransactionsUri(leagueModel.LeagueId, week));
        response.EnsureSuccessStatusCode();
        var json = await response.Content.ReadAsStringAsync();
        var transactions = SleeperParser.ParseTransactions(json);
        foreach (var transactionsModel in transactions)
        {
            _reporter.OnNewTransaction(transactionsModel); 
        }
    }
}