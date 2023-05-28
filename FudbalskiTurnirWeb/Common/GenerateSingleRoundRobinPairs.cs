using FudbalskiTurnirWeb.Data;
using FudbalskiTurnirWeb.Interfaces;
using FudbalskiTurnirWeb.Models;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;

namespace FudbalskiTurnirWeb.Common
{
    public class GenerateSingleRoundRobinPairs : IGeneratePairingForMatches
    {

        private readonly FudbalskiTurnirWebContext _context;

        public GenerateSingleRoundRobinPairs(FudbalskiTurnirWebContext context)
        {
            _context = context;
        }

        public List<Match> Generate(List<Team> teams)
        {
            List<Match> matches = new List<Match>();

            for (int i= 0, d = 0; i < teams.Count; i++)
            {
                for (var y = i; y < teams.Count; y++)
                {
                    if (teams[i] != teams[y])
                    {
                        matches.Add(new Match
                        {
                            AwayTeamId = teams[i].Id,
                            HomeTeamId = teams[y].Id,
                            AwayTeamGoals = null,
                            HomeTeamGoals = null,
                            Date = (DateTime.Now).AddDays(++d)
                        });

                    }
                }
            }
       
            return matches;
        }
        
    }

   
}
