namespace rizer.Model;

public class SleeperConfigurationModel
{
    public string BaseUrl { get; set; }
    public string Version { get; set; }
    public string UserEndpoint { get; set; }
    public string LeaguesEndpoint { get; set; }
    
    public string RostersEndpoint { get; set; }
    
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

    public string UserUri()
    {
        return $"{BaseUrl}/{Version}/{UserEndpoint}/{UserId}";
    }

    public string UserLeaguesUri()
    {
        return $"{UserUri()}/{LeaguesEndpoint}s/{Sport}/{Season}";
    }

    public string LeaguesUri()
    {
        return $"{BaseUrl}/{Version}/{LeaguesEndpoint}";
    }

    public string RostersUri(string leagueId)
    {
        return $"{LeaguesUri()}/{leagueId}/{RostersEndpoint}";
    }
}