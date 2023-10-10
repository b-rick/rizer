using rizer.Model;
using rizer.Model.Sleeper;
using rizer.Services.LeagueTrawlerReporter;

namespace rizer.Services;

public class LeagueTrawler
{
    private readonly ISet<string> league_ids_;
    private readonly SleeperConfigurationModel config_;
    private readonly HttpClient http_client_;
    private readonly ILeagueTrawlerReporter reporter_;

    public LeagueTrawler(SleeperConfigurationModel config, HttpClient httpClient, ILeagueTrawlerReporter reporter)
    {
        league_ids_ = new HashSet<string>();
        config_ = config;
        http_client_ = httpClient;
        reporter_ = reporter;
    }


    public async Task Trawl()
    {
        var response = await http_client_.GetAsync(config_.UserLeaguesUri());
        response.EnsureSuccessStatusCode();
        var json = await response.Content.ReadAsStringAsync();
        var leagues = SleeperParser.Parse(json);
        foreach (var model in leagues)
        {
            await Trawl(model);
        }
    }

    private async Task Trawl(LeagueModel leagueModel)
    {
        reporter_.OnNewLeague(leagueModel);
        if (!league_ids_.Add(leagueModel.LeagueId))
        {
            return;
        }

        var response = await http_client_.GetAsync(config_.RostersUri(leagueModel.LeagueId));
        response.EnsureSuccessStatusCode();
        var json = await response.Content.ReadAsStringAsync();
        Console.Write(json);
    }
    
}