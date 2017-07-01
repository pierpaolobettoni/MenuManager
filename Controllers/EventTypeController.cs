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
    public class EventTypeController : ControllerBase
    {

        public EventTypeController(ApplicationDbContext context)
        : base(context)
        {

        }

        // GET: EventType
        public async Task<IActionResult> Index()
        {
            return View(await DBContext.EventTypes.Where(x => x.Location == GetLoggedInUser().GetCurrentLocation()).OrderBy(x => x.EventTypeName).ToListAsync());
        }

        // GET: EventType/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var eventType = await DBContext.EventTypes
                .SingleOrDefaultAsync(m => m.Id == id);
            if (eventType == null)
            {
                return NotFound();
            }

            return View(eventType);
        }

        // GET: EventType/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: EventType/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,EventTypeName")] EventType eventType)
        {
            eventType.Location = base.GetLoggedInUser().GetCurrentLocation();
            ModelState.Remove("Location");
            if (ModelState.IsValid)
            {
                DBContext.Add(eventType);
                await DBContext.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(eventType);
        }

        // GET: EventType/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var eventType = await DBContext.EventTypes.SingleOrDefaultAsync(m => m.Id == id);
            if (eventType == null)
            {
                return NotFound();
            }
            return View(eventType);
        }

        // POST: EventType/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,EventTypeName")] EventType eventType)
        {
            if (id != eventType.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    DBContext.Update(eventType);
                    await DBContext.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EventTypeExists(eventType.Id))
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
            return View(eventType);
        }

        // GET: EventType/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var eventType = await DBContext.EventTypes
                .SingleOrDefaultAsync(m => m.Id == id);
            if (eventType == null)
            {
                return NotFound();
            }

            return View(eventType);
        }

        // POST: EventType/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var eventType = await DBContext.EventTypes.SingleOrDefaultAsync(m => m.Id == id);
            DBContext.EventTypes.Remove(eventType);
            await DBContext.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool EventTypeExists(int id)
        {
            return DBContext.EventTypes.Any(e => e.Id == id);
        }
    }
}
