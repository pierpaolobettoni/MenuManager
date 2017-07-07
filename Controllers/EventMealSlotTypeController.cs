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
    public class EventMealSlotTypeController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public EventMealSlotTypeController(ApplicationDbContext context)
        : base(context)
        {
            _context = context;
        }

        // GET: EventMealSlotType
        public  IActionResult Index()
        {
            return View( GetLoggedInUser().GetEventMealSlotTypes().ToList());
        }

        // GET: EventMealSlotType/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var eventMealSlotType = await _context.EventMealSlotTypes
                .SingleOrDefaultAsync(m => m.Id == id);
            if (eventMealSlotType == null)
            {
                return NotFound();
            }

            return View(eventMealSlotType);
        }

        // GET: EventMealSlotType/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: EventMealSlotType/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name")] EventMealSlotType eventMealSlotType)
        {
            if (ModelState.IsValid)
            {
                _context.Add(eventMealSlotType);
                await base.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(eventMealSlotType);
        }

        // GET: EventMealSlotType/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var eventMealSlotType = await _context.EventMealSlotTypes.SingleOrDefaultAsync(m => m.Id == id);
            if (eventMealSlotType == null)
            {
                return NotFound();
            }
            return View(eventMealSlotType);
        }

        // POST: EventMealSlotType/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name")] EventMealSlotType eventMealSlotType)
        {
            if (id != eventMealSlotType.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(eventMealSlotType);
                    await base.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EventMealSlotTypeExists(eventMealSlotType.Id))
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
            return View(eventMealSlotType);
        }

        // GET: EventMealSlotType/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var eventMealSlotType = await _context.EventMealSlotTypes
                .SingleOrDefaultAsync(m => m.Id == id);
            if (eventMealSlotType == null)
            {
                return NotFound();
            }

            return View(eventMealSlotType);
        }

        // POST: EventMealSlotType/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var eventMealSlotType = await _context.EventMealSlotTypes.SingleOrDefaultAsync(m => m.Id == id);
            _context.EventMealSlotTypes.Remove(eventMealSlotType);
            await base.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool EventMealSlotTypeExists(int id)
        {
            return _context.EventMealSlotTypes.Any(e => e.Id == id);
        }
    }
}
