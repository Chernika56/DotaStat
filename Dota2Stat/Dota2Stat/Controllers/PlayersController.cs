using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Dota2Stat.Models.DB;

namespace Dota2Stat.Controllers
{
    public class PlayersController : Controller
    {
        private readonly Dota2statContext _context;

        public PlayersController(Dota2statContext context)
        {
            _context = context;
        }

        // GET: Players
        public async Task<IActionResult> Index()
        {
            var dota2statContext = _context.Players.Include(p => p.PlCountryNavigation).OrderBy(p => p.PlRang);
            return View(await dota2statContext.ToListAsync());
        }

        // GET: Players/Details/5
        public async Task<IActionResult> Details(uint? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var player = await _context.Players
                .Include(p => p.PlCountryNavigation)
                .FirstOrDefaultAsync(m => m.PlId == id);
            if (player == null)
            {
                return NotFound();
            }

            return View(player);
        }

        // GET: Players/Create
        public IActionResult Create()
        {
            ViewData["PlCountry"] = new SelectList(_context.Countries, "CnId", "CnName");
            return View();
        }

        // POST: Players/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PlId,PlFirstName,PlLastName,PlNickname,PlCountry,PlMmr,PlRang")] Player player)
        {
            if (ModelState.IsValid)
            {
                _context.Add(player);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["PlCountry"] = new SelectList(_context.Countries, "CnId", "CnId", player.PlCountry);
            return View(player);
        }

        // GET: Players/Edit/5
        public async Task<IActionResult> Edit(uint? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var player = await _context.Players.FindAsync(id);
            if (player == null)
            {
                return NotFound();
            }
            ViewData["PlCountry"] = new SelectList(_context.Countries, "CnId", "CnName", player.PlCountry);
            return View(player);
        }

        // POST: Players/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(uint id, [Bind("PlId,PlFirstName,PlLastName,PlNickname,PlCountry,PlMmr,PlRang")] Player player)
        {
            if (id != player.PlId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(player);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PlayerExists(player.PlId))
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
            ViewData["PlCountry"] = new SelectList(_context.Countries, "CnId", "CnId", player.PlCountry);
            return View(player);
        }

        // GET: Players/Delete/5
        public async Task<IActionResult> Delete(uint? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var player = await _context.Players
                .Include(p => p.PlCountryNavigation)
                .FirstOrDefaultAsync(m => m.PlId == id);
            if (player == null)
            {
                return NotFound();
            }

            return View(player);
        }

        // POST: Players/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(uint id)
        {
            var player = await _context.Players.FindAsync(id);
            if (player != null)
            {
                _context.Players.Remove(player);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PlayerExists(uint id)
        {
            return _context.Players.Any(e => e.PlId == id);
        }
    }
}
