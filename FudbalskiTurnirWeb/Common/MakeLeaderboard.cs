using FudbalskiTurnirWeb.Interfaces;
using FudbalskiTurnirWeb.Models;

namespace FudbalskiTurnirWeb.Common
{
    public class MakeLeaderboard : IMakeLeaderboard
    {

        const int pointsForWin = 3;
        const int pointsforDraw = 1;
        const int pointsForLoss = 0;
        public List<Leaderboard> Leaderboard(List<Team> teams, List<Match> matches)
        {
            List<Leaderboard> Leaderboard = new();

            foreach (var team in teams)
            {
                int points = 0;
                foreach(Match match in matches)
                {
                    if (match.AwayTeamGoals == null || match.HomeTeamGoals == null) continue;
                    if ((team.Id == match.AwayTeamId && match.AwayTeamGoals > match.HomeTeamGoals) || (team.Id == match.HomeTeamId && match.HomeTeamGoals > match.AwayTeamGoals))
                    {
                        points += pointsForWin;
                        continue;
                    }
                    if ((team.Id == match.AwayTeamId || team.Id == match.HomeTeamId) && match.HomeTeamGoals == match.AwayTeamGoals)
                    {
                        points += pointsforDraw;
                        continue;
                    }
                    if ((team.Id == match.AwayTeamId && match.AwayTeamGoals < match.HomeTeamGoals) || (team.Id == match.HomeTeamId && match.HomeTeamGoals < match.AwayTeamGoals)) 
                    {
                        points += pointsForLoss;
                    }

                   
                }
                Leaderboard.Add(new Leaderboard { Id = team.Id, Name = team.Name, Points = points });
            }
            Leaderboard = Leaderboard.OrderByDescending(x => x.Points).ToList();
            for(int i = 0, y = 1; i < Leaderboard.Count; i++)
            {
                if (i == 0) 
                {
                    Leaderboard[i].Placement = y;
                    continue;
                } 
                if (Leaderboard[i].Points == Leaderboard[i - 1].Points) Leaderboard[i].Placement = y;
                if (Leaderboard[i].Points < Leaderboard[i - 1].Points) Leaderboard[i].Placement = ++y;

            }
                
            return Leaderboard;
        }
    }
}
