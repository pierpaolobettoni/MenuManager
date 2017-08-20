using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using clean_aspnet_mvc.Data;
using Microsoft.AspNetCore.Authorization;

namespace clean_aspnet_mvc.Controllers
{

    public class EventController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public EventController(ApplicationDbContext context)
        : base(context)
        {

            _context = base.DBContext;
        }

        // GET: Event
        public async Task<IActionResult> Index()
        {
            return View(await GetLoggedInUser().GetEvents());
        }

        // GET: Event/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var @event = await _context.Events
                .SingleOrDefaultAsync(m => m.Id == id);
            if (@event == null)
            {
                return NotFound();
            }

            return View(@event);
        }

        // GET: Event/Create
        public IActionResult Create()
        {
            ViewBag.EventTypes = GetLoggedInUser().GetEventTypes();
            return View();
        }

        // POST: Event/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,EventName,StartDate,EndDate, EventTypeId, EventDescription, NumberOfPeopleAttending")] Event @event)
        {
            ViewBag.EventTypes = GetLoggedInUser().GetEventTypes();

            @event.Location = base.GetLoggedInUser().GetCurrentLocation();
            ModelState.Remove("Location");
            if (ModelState.IsValid)
            {
                _context.Add(@event);
                await base.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(@event);
        }

        // GET: Event/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            ViewBag.EventTypes = GetLoggedInUser().GetEventTypes();
            if (id == null)
            {
                return NotFound();
            }

            var @event = await _context.Events.Include("Meals").Include("Meals.Menu").SingleOrDefaultAsync(m => m.Id == id);
            if (@event == null)
            {
                return NotFound();
            }
            ViewBag.Slots = GetLoggedInUser().GetEventMealSlotTypes();
            ViewBag.Menus = GetLoggedInUser().GetMenus().ToList();
            return View(@event);
        }

        // POST: Event/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,EventName,StartDate,EndDate,EventTypeId, EventDescription, NumberOfPeopleAttending")] Event @event)
        {
            ViewBag.EventTypes = GetLoggedInUser().GetEventTypes();
            @event.Location = base.GetLoggedInUser().GetCurrentLocation();
            ModelState.Remove("Location");

            if (id != @event.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(@event);
                    await base.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EventExists(@event.Id))
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
            ViewBag.Slots = GetLoggedInUser().GetEventMealSlotTypes();
            ViewBag.Menus = GetLoggedInUser().GetMenus();
            return View(@event);
        }

        // GET: Event/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var @event = await _context.Events
                .SingleOrDefaultAsync(m => m.Id == id);
            if (@event == null)
            {
                return NotFound();
            }

            return View(@event);
        }

        // POST: Event/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var @event = await _context.Events.SingleOrDefaultAsync(m => m.Id == id);
            _context.Events.Remove(@event);
            await base.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool EventExists(int id)
        {
            return _context.Events.Any(e => e.Id == id);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddEventMeal([Bind("EventId, EventMealSlotId, MenuId, NumberOfPeopleAttending, MealDate")] EventMeal eventMeal)
        {

            var thisEvent =  await base.DBContext.Events.Where(x => x.Id == eventMeal.EventId).FirstOrDefaultAsync();
            if (thisEvent == null)
                throw new Exception("Bad event");
            if (eventMeal.MealDate >= thisEvent.StartDate && eventMeal.MealDate <= thisEvent.EndDate)
            {
                _context.Add(eventMeal);
                await base.SaveChangesAsync();
            }
            return RedirectToAction("Edit", new { id = eventMeal.EventId });
        }



        public async Task<IActionResult> DeleteEventMeal(int id, int eventId)
        {
            var eventMeal = await (from x in DBContext.EventMeal where x.Location == GetLoggedInUser().GetCurrentLocation() && x.Id == id select x).FirstAsync();
            if (eventMeal != null)
            {
                _context.Remove(eventMeal);
                await base.SaveChangesAsync();
            }

            return RedirectToAction("Edit", new { id = eventId });
        }
    }
}
