namespace rizer.Model;

public class TrawlConfigurationModel
{
    // TODO: Season data information should be fetched from somewhere else 
    public int NumWeeks { get; set; }
    
    public int MaxTrades { get; set; }
    
    public int MaxTrawlDepth { get; set; }
    
    //TODO: add rate limiting
    public int  TrawlRateLimit { get; set; }
}