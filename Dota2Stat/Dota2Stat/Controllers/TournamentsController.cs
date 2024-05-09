using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Dota2Stat.Models.DB;
using Dota2Stat.Models;

namespace Dota2Stat.Controllers
{
    public class TournamentsController : Controller
    {
        private readonly Dota2statContext _context;

        public TournamentsController(Dota2statContext context)
        {
            _context = context;
        }

        // GET: Tournaments
        public async Task<IActionResult> Index()
        {
            var dota2statContext = _context.Tournaments.Include(t => t.TrOrganizerNavigation);
            return View(await dota2statContext.ToListAsync());
        }

        // GET: Tournaments/Details/5
        public async Task<IActionResult> Details(uint? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tournament = await _context.Tournaments
                .Include(t => t.TrOrganizerNavigation)
                .Include(trtm => trtm.M2mTournamentTeams)
                .ThenInclude(trtm => trtm.TrtmTeamNavigation)
                .FirstOrDefaultAsync(m => m.TrId == id);

            if (tournament == null)
            {
                return NotFound();
            }

            tournament.M2mTournamentTeams = tournament.M2mTournamentTeams.OrderBy(tt => tt.TrtmResult).ToList();

            return View(tournament);
        }

        // GET: Tournaments/Create
        public IActionResult Create()
        {
            ViewData["TrOrganizer"] = new SelectList(_context.Organizers, "OpId", "OpName");
            return View();
        }

        // POST: Tournaments/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TrId,TrName,TrTier,TrParticipants,TrOrganizer,TrPrize,TrPlace,TrStartDate,TrEndDate")] Tournament tournament)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tournament);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["TrOrganizer"] = new SelectList(_context.Organizers, "OpId", "OpName", tournament.TrOrganizer);
            return View(tournament);
        }

        // GET: Tournaments/Edit/5
        public async Task<IActionResult> Edit(uint? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tournament = await _context.Tournaments
                .Include(trtm => trtm.M2mTournamentTeams)
                .FirstOrDefaultAsync(m => m.TrId == id);

            var viewModel = new TournamentTeam
            {
                Tournament = tournament
            };

            if (viewModel == null)
            {
                return NotFound();
            }

            ViewData["TrOrganizer"] = new SelectList(_context.Organizers, "OpId", "OpName", tournament.TrOrganizer);
            ViewData["Teams"] = new SelectList(_context.Teams, "TmId", "TmName");

            return View(viewModel);
        }

        // POST: Tournaments/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(uint id, [Bind("Tournament,TmId,TmResult")] TournamentTeam viewModel)
        {
            if (id != viewModel.Tournament.TrId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var tournament = new Tournament
                    {
                        TrId = viewModel.Tournament.TrId,
                        TrName = viewModel.Tournament.TrName,
                        TrTier = viewModel.Tournament.TrTier,
                        TrParticipants = viewModel.Tournament.TrParticipants,
                        TrOrganizer = viewModel.Tournament.TrOrganizer,
                        TrPrize = viewModel.Tournament.TrPrize,
                        TrPlace = viewModel.Tournament.TrPlace,
                        TrStartDate = viewModel.Tournament.TrStartDate,
                        TrEndDate = viewModel.Tournament.TrEndDate
                    };

                    _context.Update(tournament);
                    await _context.SaveChangesAsync();

                    if (viewModel.TmId != null)
                    {
                        var existingTournamentTeam = await _context.M2mTournamentTeams
                            .FirstOrDefaultAsync(tt => tt.TrtmTeam == viewModel.TmId && tt.TrtmTournament == viewModel.Tournament.TrId);

                        if (existingTournamentTeam != null)
                        {
                            existingTournamentTeam.TrtmResult = viewModel.TmResult;
                            _context.Update(existingTournamentTeam);
                        }
                        else
                        {
                            var tournamentTeam = new M2mTournamentTeam
                            {
                                TrtmTeam = (uint)viewModel.TmId,
                                TrtmTournament = viewModel.Tournament.TrId,
                                TrtmResult = viewModel.TmResult
                            };
                            _context.Add(tournamentTeam);
                        }
                    }

                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TournamentExists(id))
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

            ViewData["TrOrganizer"] = new SelectList(_context.Organizers, "OpId", "OpName", viewModel.Tournament.TrOrganizer);
            ViewData["Teams"] = new SelectList(_context.Teams, "TmId", "TmName");

            return View(viewModel);
        }

        // GET: Tournaments/Delete/5
        public async Task<IActionResult> Delete(uint? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tournament = await _context.Tournaments
                .Include(t => t.TrOrganizerNavigation)
                .FirstOrDefaultAsync(m => m.TrId == id);
            if (tournament == null)
            {
                return NotFound();
            }

            return View(tournament);
        }

        // POST: Tournaments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(uint id)
        {
            var tournament = await _context.Tournaments.FindAsync(id);
            if (tournament != null)
            {
                _context.Tournaments.Remove(tournament);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TournamentExists(uint id)
        {
            return _context.Tournaments.Any(e => e.TrId == id);
        }
    }
}
