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

    public class RosterStats
    {
        protected bool Equals(RosterStats other)
        {
            return Wins == other.Wins && Ties == other.Ties && Losses == other.Losses && PointsFor == other.PointsFor &&
                   PointsForDecimal == other.PointsForDecimal && PointsAgainst == other.PointsAgainst &&
                   PointsAgainstDecimal == other.PointsAgainstDecimal;
        }

        public override bool Equals(object? obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((RosterStats)obj);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Wins, Ties, Losses, PointsFor, PointsForDecimal, PointsAgainst, PointsAgainstDecimal);
        }

        [JsonPropertyName("wins")]
        public int Wins { get; set; }
        [JsonPropertyName("ties")]
        public int Ties { get; set; }
        [JsonPropertyName("losses")]
        public int Losses { get; set; }
        [JsonPropertyName("fpts")]
        public int PointsFor { get; set; }
        [JsonPropertyName("fpts_decimal")]
        public int PointsForDecimal { get; set; }
        [JsonPropertyName("fpts_against")]
        public int PointsAgainst { get; set; }
        [JsonPropertyName("fpts_against_decimal")]
        public int PointsAgainstDecimal { get; set; }
    }

    public class RosterMetadata
    {
        [JsonPropertyName("streak")]
        public string Streak { get; set; }
        [JsonPropertyName("record")]
        public string Record { get; set; } 
    }
    

    public class RosterModel
    {
        [JsonPropertyName("starters")]
        public List<string> StartingPlayerIds { get; set; } 
        [JsonPropertyName("settings")]
        public RosterStats RosterStats { get; set; }
        [JsonPropertyName("players")]
        public List<string> RosterPlayerIds { get; set; }
        [JsonPropertyName("owner_id")]
        public string OwnerId { get; set; }
        [JsonPropertyName("metadata")]
        public RosterMetadata RosterMetadata { get; set; }
        [JsonPropertyName("league_id")]
        public string LeagueId { get; set; }
    }

    public class DraftPick
    {
        protected bool Equals(DraftPick other)
        {
            return Season == other.Season && Round == other.Round && OwnerId == other.OwnerId && PreviousOwnerId == other.PreviousOwnerId;
        }

        public override bool Equals(object? obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((DraftPick)obj);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Season, Round, OwnerId, PreviousOwnerId);
        }

        [JsonPropertyName("season")] 
        public string Season { get; set; }
        [JsonPropertyName("round")]
        public int Round { get; set; }
        [JsonPropertyName("owner_id")]
        public int OwnerId { get; set; }
        [JsonPropertyName("previous_owner_id")]
        public int PreviousOwnerId { get; set; }
    }

    public class WaiverSettings
    {
        [JsonPropertyName("waiver_bid")]
        public int WaiverBid { get; set; }
        [JsonPropertyName("sequence")]
        public int WaiverSequence { get; set; }
    }

    public class TransactionsModel
    {
        [JsonPropertyName("type")]
        public string TransactionType { get; set; }
        [JsonPropertyName("settings")]
        public WaiverSettings WaiverSettings { get; set; }
        [JsonPropertyName("transaction_id")]
        public string TransactionId { get; set; }
        [JsonPropertyName("status")]
        public string TransactionStatus { get; set; }
        [JsonPropertyName("roster_ids")]
        public List<int> RosterIds { get; set; }
        [JsonPropertyName("draft_picks")]
        public List<DraftPick> DraftPicks { get; set; }
        [JsonPropertyName("adds")]
        public Dictionary<string, int> AddedPlayers { get; set; }
        [JsonPropertyName("drops")]
        public Dictionary<string, int> DroppedPlayers { get; set; }
    }
    
    public static class SleeperParser
    {
        public static IList<LeagueModel> ParseLeaguesFromFile(string aPath)
        {
            return ParseLeagues(File.ReadAllText(aPath));
        }

        public static IList<RosterModel> ParseRostersFromFile(string aPath)
        {
            return ParseRosters(File.ReadAllText(aPath));
        }

        public static IList<TransactionsModel> ParseTransactionsFromFile(string aPath)
        {
            return ParseTransactions(File.ReadAllText(aPath));
        }

        public static IList<LeagueModel> ParseLeagues(string json)
        {
            return JsonSerializer.Deserialize<IList<LeagueModel>>(json) ?? 
                   throw new InvalidOperationException("Failed to deserialize " + json);
        }
        
        public static IList<RosterModel> ParseRosters(string json)
        {
            return JsonSerializer.Deserialize<IList<RosterModel>>(json) ?? 
                   throw new InvalidOperationException("Failed to deserialize " + json);
        }

        public static IList<TransactionsModel> ParseTransactions(string json)
        {
            return JsonSerializer.Deserialize<IList<TransactionsModel>>(json) ?? 
                   throw new InvalidOperationException("Failed to deserialize " + json);
        }
    }
}

