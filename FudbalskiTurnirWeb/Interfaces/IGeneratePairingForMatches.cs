using FudbalskiTurnirWeb.Models;

namespace FudbalskiTurnirWeb.Interfaces
{
    public interface IGeneratePairingForMatches
    {
         List<Match> Generate(List<Team> teams);
    }
}
