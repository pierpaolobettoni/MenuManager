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
    public class MealItemController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public MealItemController(ApplicationDbContext context)
        : base(context)
        {
            _context = context;
        }

        // GET: MealItem
        public async Task<IActionResult> Index()
        {
            return View(await _context.MealItems.ToListAsync());
        }

        // GET: MealItem/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var mealItem = await _context.MealItems
                .SingleOrDefaultAsync(m => m.Id == id);
            if (mealItem == null)
            {
                return NotFound();
            }

            return View(mealItem);
        }

        // GET: MealItem/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: MealItem/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,MealItemName,MealItemDescription,Quantity,MeasureType")] MealItem mealItem)
        {
            if (ModelState.IsValid)
            {
                _context.Add(mealItem);
                //await base.SaveChangesAsync();
                await base.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(mealItem);
        }

        // GET: MealItem/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var mealItem = await _context.MealItems.SingleOrDefaultAsync(m => m.Id == id);
            if (mealItem == null)
            {
                return NotFound();
            }
            return View(mealItem);
        }

        // POST: MealItem/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,MealItemName,MealItemDescription,Quantity,MeasureType")] MealItem mealItem)
        {
            if (id != mealItem.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(mealItem);
                    await base.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MealItemExists(mealItem.Id))
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
            return View(mealItem);
        }

        // GET: MealItem/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var mealItem = await _context.MealItems
                .SingleOrDefaultAsync(m => m.Id == id);
            if (mealItem == null)
            {
                return NotFound();
            }

            return View(mealItem);
        }

        // POST: MealItem/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var mealItem = await _context.MealItems.SingleOrDefaultAsync(m => m.Id == id);
            _context.MealItems.Remove(mealItem);
            await base.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool MealItemExists(int id)
        {
            return _context.MealItems.Any(e => e.Id == id);
        }
    }
}
