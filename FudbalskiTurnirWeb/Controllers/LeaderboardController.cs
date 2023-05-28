using FudbalskiTurnirWeb.Data;
using FudbalskiTurnirWeb.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FudbalskiTurnirWeb.Controllers
{
    public class LeaderboardController : Controller
    {

        private readonly IMakeLeaderboard _leaderboard;
        private readonly FudbalskiTurnirWebContext _context;

        public LeaderboardController(IMakeLeaderboard leaderboard, FudbalskiTurnirWebContext context)
        {
            _leaderboard = leaderboard;
            _context = context;
        }

        // GET: LeaderboardController
        public ActionResult Index()
        {
           var Leaderboard = _leaderboard.Leaderboard(_context.Team.Select(x => x).ToList(), _context.Match.Select(x => x).ToList());

            return View(Leaderboard);
        }

      

       
     


       
     
     
    }
}
