using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BubiSanat.Data;
using BubiSanat.Models;
using Microsoft.AspNetCore.Authorization;

namespace BubiSanat.Controllers
{
    [Authorize]
    public class TopCategoriesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public TopCategoriesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: TopCategories
        public async Task<IActionResult> Index()
        {
              return View(await _context.TopCategories.ToListAsync());
        }

        // GET: TopCategories/Details/5
        public async Task<IActionResult> Details(short? id)
        {
            if (id == null || _context.TopCategories == null)
            {
                return NotFound();
            }

            var topCategory = await _context.TopCategories
                .FirstOrDefaultAsync(m => m.Id == id);
            if (topCategory == null)
            {
                return NotFound();
            }

            return View(topCategory);
        }

        // GET: TopCategories/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: TopCategories/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name")] TopCategory topCategory)
        {
            if (ModelState.IsValid)
            {
                _context.Add(topCategory);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(topCategory);
        }

        // GET: TopCategories/Edit/5
        public async Task<IActionResult> Edit(short? id)
        {
            if (id == null || _context.TopCategories == null)
            {
                return NotFound();
            }

            var topCategory = await _context.TopCategories.FindAsync(id);
            if (topCategory == null)
            {
                return NotFound();
            }
            return View(topCategory);
        }

        // POST: TopCategories/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(short id, [Bind("Id,Name")] TopCategory topCategory)
        {
            if (id != topCategory.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(topCategory);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TopCategoryExists(topCategory.Id))
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
            return View(topCategory);
        }

        // GET: TopCategories/Delete/5
        public async Task<IActionResult> Delete(short? id)
        {
            if (id == null || _context.TopCategories == null)
            {
                return NotFound();
            }

            var topCategory = await _context.TopCategories
                .FirstOrDefaultAsync(m => m.Id == id);
            if (topCategory == null)
            {
                return NotFound();
            }

            return View(topCategory);
        }

        // POST: TopCategories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(short id)
        {
            if (_context.TopCategories == null)
            {
                return Problem("Entity set 'ApplicationDbContext.TopCategories'  is null.");
            }
            var topCategory = await _context.TopCategories.FindAsync(id);
            if (topCategory != null)
            {
                _context.TopCategories.Remove(topCategory);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TopCategoryExists(short id)
        {
          return _context.TopCategories.Any(e => e.Id == id);
        }
    }
}
