namespace rizer.Model;

public class SleeperConfigurationModel
{
    public string BaseUrl { get; set; }
    public string Version { get; set; }
    public string UserEndpoint { get; set; }
    public string LeaguesEndpoint { get; set; }
    
    public string RostersEndpoint { get; set; }
    
    public string TransactionsEndpoint { get; set; }
    
    public string Sport { get; set; }
    public string UserId { get; set; }
    
    public int Season { get; set; }

    public bool IsValid()
    {
        return string.IsNullOrEmpty(BaseUrl)
               || string.IsNullOrEmpty(Version)
               || string.IsNullOrEmpty(UserEndpoint)
               || string.IsNullOrEmpty(LeaguesEndpoint)
               || string.IsNullOrEmpty(UserId);
    }

    public string UserUri(string userId)
    {
        return $"{BaseUrl}/{Version}/{UserEndpoint}/{userId}";
    }

    public string UserLeaguesUri(string userId)
    {
        return $"{UserUri(userId)}/{LeaguesEndpoint}s/{Sport}/{Season}";
    }

    public string LeaguesUri()
    {
        return $"{BaseUrl}/{Version}/{LeaguesEndpoint}";
    }

    public string RostersUri(string leagueId)
    {
        return $"{LeaguesUri()}/{leagueId}/{RostersEndpoint}";
    }

    public string TransactionsUri(string leagueId, int week)
    {
        return $"{LeaguesUri()}/{leagueId}/{TransactionsEndpoint}/{week}";
    }
}