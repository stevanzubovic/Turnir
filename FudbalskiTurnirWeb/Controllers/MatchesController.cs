using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FudbalskiTurnirWeb.Data;
using FudbalskiTurnirWeb.Models;
using System.Security.Cryptography.X509Certificates;
using FudbalskiTurnirWeb.Common;
using FudbalskiTurnirWeb.Interfaces;
using Microsoft.AspNetCore.Authorization;

namespace FudbalskiTurnirWeb.Controllers
{
    public class MatchesController : Controller
    {
        private readonly FudbalskiTurnirWebContext _context;
        private readonly IGeneratePairingForMatches _pair;

        public MatchesController(FudbalskiTurnirWebContext context, IGeneratePairingForMatches pair)
        {
            _context = context;
            _pair = pair;
        }

        // GET: Matches
        public async Task<IActionResult> Index()
        {
            var fudbalskiTurnirWebContext = _context.Match.Include(m => m.AwayTeam).Include(m => m.HomeTeam);
            return View(await fudbalskiTurnirWebContext.ToListAsync());
        }

        // GET: Matches/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Match == null)
            {
                return NotFound();
            }

            var match = await _context.Match
                .Include(m => m.AwayTeam)
                .Include(m => m.HomeTeam)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (match == null)
            {
                return NotFound();
            }

            return View(match);
        }

        // GET: Matches/Create
        [Authorize]
        public IActionResult Create()
        {
            ViewData["AwayTeamId"] = new SelectList(_context.Team, "Id", "Name");
            ViewData["HomeTeamId"] = new SelectList(_context.Team, "Id", "Name");
            return View();
        }

        // POST: Matches/Create
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,HomeTeamId,AwayTeamId,HomeTeamGoals,AwayTeamGoals,Date")] Match match)
        {
            if (ModelState.IsValid)
            {
                match.HomeTeam = _context.Team.Find(match.HomeTeamId);
                match.AwayTeam = _context.Team.Find(match.AwayTeamId);
                _context.Add(match);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AwayTeamId"] = new SelectList(_context.Team, "Id", "Name", match.AwayTeamId);
            ViewData["HomeTeamId"] = new SelectList(_context.Team, "Id", "Name", match.HomeTeamId);
            return View(match);
        }

        // GET: Matches/Edit/5
        [Authorize]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Match == null)
            {
                return NotFound();
            }

            var match = await _context.Match.FindAsync(id);
            if (match == null)
            {
                return NotFound();
            }
            ViewData["AwayTeamId"] = new SelectList(_context.Team, "Id", "Name", match.AwayTeamId);
            ViewData["HomeTeamId"] = new SelectList(_context.Team, "Id", "Name", match.HomeTeamId);
            return View(match);
        }

        // POST: Matches/Edit/5

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,HomeTeamId,AwayTeamId,HomeTeamGoals,AwayTeamGoals,Date")] Match match)
        {
            if (id != match.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(match);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MatchExists(match.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["AwayTeamId"] = new SelectList(_context.Team, "Id", "Id", match.AwayTeamId);
            ViewData["HomeTeamId"] = new SelectList(_context.Team, "Id", "Id", match.HomeTeamId);
            return View(match);
        }

        // GET: Matches/Delete/5
        [Authorize]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Match == null)
            {
                return NotFound();
            }

            var match = await _context.Match
                .Include(m => m.AwayTeam)
                .Include(m => m.HomeTeam)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (match == null)
            {
                return NotFound();
            }

            return View(match);
        }

        // POST: Matches/Delete/5
        [Authorize]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Match == null)
            {
                return Problem("Entity set 'FudbalskiTurnirWebContext.Match'  is null.");
            }
            var match = await _context.Match.FindAsync(id);
            if (match != null)
            {
                _context.Match.Remove(match);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MatchExists(int id)
        {
          return (_context.Match?.Any(e => e.Id == id)).GetValueOrDefault();
        }

        [Authorize]
        public async Task<IActionResult> GeneratePairs()
        {
            var teams = _context.Team.Select(x => x).ToList();
            List<Match> matches = new List<Match>();

            matches = _pair.Generate(teams);
            _context.RemoveRange(_context.Match.Select(x => x));
            _context.AddRange(matches);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
    }
}   
