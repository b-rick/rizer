using System.Text.Json;
using System.Text.Json.Serialization;

namespace rizer.Model.Sleeper
{
    public class ScoringSettingsModel
    {
        [JsonPropertyName("rec")]
        public double Reception { get; set; }
        [JsonPropertyName("rec_yd")]
        public double ReceivingYard { get; set; }
        [JsonPropertyName("rush_yd")]
        public double RushYard { get; set; }
        [JsonPropertyName("pass_yd")]
        public double PassYard { get; set; }
        [JsonPropertyName("rec_td")]
        public double ReceivingTouchdown { get; set; }
        [JsonPropertyName("rush_td")]
        public double RushTouchdown { get; set; }
        [JsonPropertyName("pass_td")]
        public double PassTouchdown { get; set; } 
        [JsonPropertyName("rec_2pt")]
        public double TwoPointConversion { get; set; }
        [JsonPropertyName("pass_td_50p")]
        public double FiftyYardPassTouchdown { get; set; }
        [JsonPropertyName("rush_td_50p")]
        public double FiftyYardRushTouchdown { get; set; }
        [JsonPropertyName("rec_td_50p")]
        public double FiftyYardReceivingTouchdown { get; set; }
        [JsonPropertyName("bonus_rec_te")]
        public double TightEndPremium { get; set; }
        [JsonPropertyName("rec_fd")]
        public double ReceivingFirstDown { get; set; }
        [JsonPropertyName("rush_fd")]
        public double RushFirstDown { get; set; }
        
    } 
    
    public class LeagueModel
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }
        [JsonPropertyName("league_id")]
        public string LeagueId { get; set; }
        [JsonPropertyName("draft_id")]
        public string DraftId { get; set; }
        [JsonPropertyName("roster_positions")]
        public ICollection<string> RosterPositions { get; set; } 
        [JsonPropertyName("total_rosters")]
        public int NumRosters { get; set; } 
        [JsonPropertyName("scoring_settings")]
        public ScoringSettingsModel ScoringSettings { get; set; } 
    }
    
    public static class SleeperParser
    {
        public static IList<LeagueModel> ParseFromFile(string aPath)
        {
            return Parse(File.ReadAllText(aPath));
        }
        
        public static IList<LeagueModel> Parse(string json)
        {
            return JsonSerializer.Deserialize<IList<LeagueModel>>(json) ?? 
                   throw new InvalidOperationException("Failed to deserialize " + json);
        }
    }
}

