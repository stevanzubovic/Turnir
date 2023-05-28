using FudbalskiTurnirWeb.Models;
using Microsoft.VisualBasic;

namespace FudbalskiTurnirWeb.Interfaces
{
    public interface IMakeLeaderboard
    {
        public List<Leaderboard> Leaderboard(List<Team> teams, List<Match> matches);
    }

    public class Leaderboard
    {
        public string Name { get; set; }

        public int Id { get; set; }

        public int Points { get; set; }

        public int Placement { get; set; }
    }
}
