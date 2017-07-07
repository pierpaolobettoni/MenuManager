using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using clean_aspnet_mvc.Data;

namespace clean_aspnet_mvc.Controllers
{
    public class EventMealController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public EventMealController(ApplicationDbContext context)
        : base(context)
        {
            _context = context;
        }

        // GET: EventMeal
        public async Task<IActionResult> Index()
        {
            return View(await _context.EventMeal.ToListAsync());
        }

        // GET: EventMeal/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var eventMeal = await _context.EventMeal
                .SingleOrDefaultAsync(m => m.Id == id);
            if (eventMeal == null)
            {
                return NotFound();
            }

            return View(eventMeal);
        }

        // GET: EventMeal/Create
        public IActionResult Create()
        {
            return View();
        }

        public IActionResult CreateForEvent(int eventId)
        {
            return View();
        }

        // POST: EventMeal/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id")] EventMeal eventMeal)
        {
            if (ModelState.IsValid)
            {
                _context.Add(eventMeal);
                await base.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(eventMeal);
        }

        // GET: EventMeal/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var eventMeal = await _context.EventMeal.SingleOrDefaultAsync(m => m.Id == id);
            if (eventMeal == null)
            {
                return NotFound();
            }
            return View(eventMeal);
        }

        // POST: EventMeal/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id")] EventMeal eventMeal)
        {
            if (id != eventMeal.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(eventMeal);
                    await base.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EventMealExists(eventMeal.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Index");
            }
            return View(eventMeal);
        }

        // GET: EventMeal/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var eventMeal = await _context.EventMeal
                .SingleOrDefaultAsync(m => m.Id == id);
            if (eventMeal == null)
            {
                return NotFound();
            }

            return View(eventMeal);
        }

        // POST: EventMeal/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var eventMeal = await _context.EventMeal.SingleOrDefaultAsync(m => m.Id == id);
            _context.EventMeal.Remove(eventMeal);
            await base.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool EventMealExists(int id)
        {
            return _context.EventMeal.Any(e => e.Id == id);
        }
    }
}
