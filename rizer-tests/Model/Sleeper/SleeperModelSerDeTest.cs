using rizer.Model.Sleeper;

namespace rizer_tests.Model.Sleeper;

public class SleeperModelSerDeTest
{
    [Test]
    public void CanDeserializeLeagueModel()
    {
        var myLeagues = SleeperParser.ParseFromFile(@"TestData/zsanchez-leagues.json");
        var myExpectedRosters = new List<string>
            { "QB", "RB", "RB", "WR", "WR", "TE", "FLEX", "FLEX", "BN", "BN", "BN", "BN", "BN", "BN" };
        Assert.Multiple(() =>
        {
            Assert.That(myLeagues[0].NumRosters, Is.EqualTo(10));
            Assert.That(myLeagues[0].ScoringSettings.Reception, Is.EqualTo(1.0));
            Assert.That(myLeagues[0].RosterPositions, Is.EqualTo(myExpectedRosters));
        });
    }
}