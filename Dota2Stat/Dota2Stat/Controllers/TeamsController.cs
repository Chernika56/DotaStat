using Dota2Stat.Models;
using Dota2Stat.Models.DB;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Dota2Stat.Controllers
{
    public class TeamsController : Controller
    {
        private readonly Dota2statContext _context;

        public TeamsController(Dota2statContext context)
        {
            _context = context;
        }

        // GET: Teams
        public async Task<IActionResult> Index()
        {
            var teams = await _context.Teams.OrderByDescending(t => t.TmRating).ToListAsync();
            return View(teams);
        }

        // GET: Teams/Details/5
        public async Task<IActionResult> Details(uint? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            //var team = await _context.Teams
            //    .FirstOrDefaultAsync(m => m.TmId == id);

            var team = await _context.Teams
                .Include(t => t.M2mPlayerTeams)
                .ThenInclude(pt => pt.PltmPlayerNavigation)
                .FirstOrDefaultAsync(m => m.TmId == id);

            if (team == null)
            {
                return NotFound();
            }

            return View(team);
        }

        // GET: Teams/Create
        public IActionResult Create()
        {
            ViewData["Players"] = new SelectList(_context.Players, "PlId", "PlNickname");

            var viewModel = new PlayerTeam
            {
                SelectedPlayerIds = new List<uint>(new uint[6])
            };

            return View(viewModel);
        }

        // POST: Teams/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TmId,TmName,TmRating,SelectedPlayerIds")] PlayerTeam viewModel)
        {
            if (ModelState.IsValid)
            {
                var team = new Team
                {
                    TmName = viewModel.TmName,
                    TmRating = viewModel.TmRating
                };
                _context.Add(team);
                await _context.SaveChangesAsync();

                for (var i = 0; i < 6; i++)
                {
                    if (viewModel.SelectedPlayerIds[i] != 0)
                    {
                        var playerTeam = new M2mPlayerTeam
                        {
                            PltmPlayer = viewModel.SelectedPlayerIds[i],
                            PltmTeam = team.TmId,
                            PltmRole = (byte)(i + 1)
                        };
                        _context.Add(playerTeam);
                    }
                }
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }

            ViewData["Players"] = new SelectList(await _context.Players.ToListAsync(), "PlId", "PlNickname");
            return View(viewModel);
        }

        // GET: Teams/Edit/5
        public async Task<IActionResult> Edit(uint? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var team = await _context.Teams.FindAsync(id);
            if (team == null)
            {
                return NotFound();
            }

            var players = await _context.Players
                                            .Select(p => new PlayerInfo { PlId = p.PlId, NickName = p.PlNickname })
                                            .ToListAsync();

            var viewModel = new PlayerTeam
            {
                Team = team,
                Players = players,
                SelectedPlayerIds = new List<uint>()
            };

            for (var i = 0; i < 6; i++)
            {
                viewModel.SelectedPlayerIds.Add(0);
            }

            ViewData["Players"] = new SelectList(_context.Players, "PlId", "PlNickname");
            return View(viewModel);
        }

        // POST: Teams/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(uint id, [Bind("Team,SelectedPlayerIds")] PlayerTeam viewModel)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    for (var i = 0; i < 6; i++)
                    {
                        var playerTeam = new M2mPlayerTeam
                        {
                            PltmPlayer = viewModel.SelectedPlayerIds[i],
                            PltmTeam = id,
                            PltmRole = (byte)(i + 1)
                        };

                        _context.Add(playerTeam);
                    }

                    var team = new Team
                    {
                        TmId = id,
                        TmName = viewModel.Team.TmName,
                        TmRating = viewModel.Team.TmRating,
                    };

                    _context.Update(team);

                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TeamExists(id))
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

            ViewData["Players"] = new SelectList(_context.Players, "PlId", "PlNickname");
            return View(viewModel);
        }


        // GET: Teams/Delete/5
        public async Task<IActionResult> Delete(uint? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var team = await _context.Teams
                .FirstOrDefaultAsync(m => m.TmId == id);
            if (team == null)
            {
                return NotFound();
            }

            return View(team);
        }

        // POST: Teams/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(uint id)
        {
            var team = await _context.Teams.FindAsync(id);
            if (team != null)
            {
                _context.Teams.Remove(team);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TeamExists(uint id)
        {
            return _context.Teams.Any(e => e.TmId == id);
        }
    }
}
