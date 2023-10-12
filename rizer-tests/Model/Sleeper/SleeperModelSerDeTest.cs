using rizer.Model.Sleeper;

namespace rizer_tests.Model.Sleeper;

public class SleeperModelSerDeTest
{
    [Test]
    public void CanDeserializeLeagueModel()
    {
        var myLeagues = SleeperParser.ParseLeaguesFromFile(@"TestData/zsanchez-leagues.json");
        var myExpectedPositions = new List<string>
            { "QB", "RB", "RB", "WR", "WR", "TE", "FLEX", "FLEX", "BN", "BN", "BN", "BN", "BN", "BN" };
        Assert.Multiple(() =>
        {
            Assert.That(myLeagues[0].NumRosters, Is.EqualTo(10));
            Assert.That(myLeagues[0].ScoringSettings.Reception, Is.EqualTo(1.0));
            Assert.That(myLeagues[0].RosterPositions, Is.EqualTo(myExpectedPositions));
        });
    }

    [Test]
    public void canDeserializeRosterModel()
    {
        var myRosters = SleeperParser.ParseRostersFromFile(@"TestData/rosters.json");
        Assert.That(myRosters, Is.Not.Empty);

        var myRoster = myRosters[0];
        Assert.That(myRoster.StartingPlayerIds, Is.EqualTo(new List<string>{"4881",
            "9509",
            "4035",
            "6786",
            "8126",
            "4983",
            "2505",
            "4137",
            "7591",
            "SF"}));
        Assert.That(myRoster.RosterStats,
            Is.EqualTo(new RosterStats
            {
                Wins = 2, Ties = 0, Losses = 2, PointsFor = 526, PointsForDecimal = 34, PointsAgainst = 529,
                PointsAgainstDecimal = 84
            }));
        Assert.That(myRoster.RosterPlayerIds, Is.EqualTo(new List<string>{"2505",
            "4035",
            "4098",
            "4137",
            "4179",
            "4881",
            "4981",
            "4983",
            "5987",
            "6786",
            "7591",
            "8126",
            "8138",
            "9508",
            "9509",
            "SF"}));
        Assert.That(myRoster.RosterMetadata.Streak, Is.EqualTo("1W"));
        Assert.That(myRoster.RosterMetadata.Record, Is.EqualTo("LWLW"));
    }

    [Test]
    public void canDeserializeTradesModel()
    {
        var myTrades = SleeperParser.ParseTransactionsFromFile(@"TestData/trades.json");
        Assert.That(myTrades, Is.Not.Empty);
        var myWaiver = myTrades[0];
        Assert.That(myWaiver.TransactionType, Is.EqualTo("waiver"));
        Assert.That(myWaiver.WaiverSettings.WaiverBid, Is.EqualTo(3));
        Assert.That(myWaiver.RosterIds, Is.EqualTo(new List<int>{6}));
        Assert.That(myWaiver.AddedPlayers, Is.EqualTo(new Dictionary<string, int>{{"2549", 6}}));

        var myTrade = myTrades[1];
        Assert.That(myTrade.TransactionType, Is.EqualTo("trade"));
        Assert.That(myTrade.RosterIds, Is.EqualTo(new List<int>{12, 6}));
        Assert.That(myTrade.DraftPicks[0], Is.EqualTo(new DraftPick{Season = "2024", Round = 2, PreviousOwnerId = 12, OwnerId = 6}));
        Assert.That(myTrade.DroppedPlayers,
            Is.EqualTo(new Dictionary<string, int>
                { { "9226", 12 }, { "8151", 6 }, { "8112", 12 }, { "6813", 6 }, { "5001", 6 }, { "10236", 12 } }));
        Assert.That(myTrade.AddedPlayers,
            Is.EqualTo(new Dictionary<string, int>
                { { "9226", 6 }, { "8151", 12 }, { "8112", 6 }, { "6813", 12 }, { "5001", 12 }, { "10236", 6 } }));
    }
}