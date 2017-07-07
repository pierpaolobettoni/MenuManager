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
    public class MealItemIngredientController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public MealItemIngredientController(ApplicationDbContext context)
        : base(context)
        {
            _context = context;
        }

        // GET: MealItemIngredient
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.MealItemIngredients.Include(m => m.GroceryItem);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: MealItemIngredient/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var mealItemIngredient = await _context.MealItemIngredients
                .Include(m => m.GroceryItem)
                .SingleOrDefaultAsync(m => m.Id == id);
            if (mealItemIngredient == null)
            {
                return NotFound();
            }

            return View(mealItemIngredient);
        }

        // GET: MealItemIngredient/Create
        public IActionResult Create()
        {
            ViewData["GroceryItemId"] = new SelectList(_context.GroceryItems, "Id", "GroceryItemName");
            return View();
        }

        // POST: MealItemIngredient/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,GroceryItemId,Quantity,MeasureType")] MealItemIngredient mealItemIngredient)
        {
            if (ModelState.IsValid)
            {
                _context.Add(mealItemIngredient);
                await base.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewData["GroceryItemId"] = new SelectList(_context.GroceryItems, "Id", "GroceryItemName", mealItemIngredient.GroceryItemId);
            return View(mealItemIngredient);
        }

        // GET: MealItemIngredient/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var mealItemIngredient = await _context.MealItemIngredients.SingleOrDefaultAsync(m => m.Id == id);
            if (mealItemIngredient == null)
            {
                return NotFound();
            }
            ViewData["GroceryItemId"] = new SelectList(_context.GroceryItems, "Id", "GroceryItemName", mealItemIngredient.GroceryItemId);
            return View(mealItemIngredient);
        }

        // POST: MealItemIngredient/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,GroceryItemId,Quantity,MeasureType")] MealItemIngredient mealItemIngredient)
        {
            if (id != mealItemIngredient.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(mealItemIngredient);
                    await base.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MealItemIngredientExists(mealItemIngredient.Id))
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
            ViewData["GroceryItemId"] = new SelectList(_context.GroceryItems, "Id", "GroceryItemName", mealItemIngredient.GroceryItemId);
            return View(mealItemIngredient);
        }

        // GET: MealItemIngredient/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var mealItemIngredient = await _context.MealItemIngredients
                .Include(m => m.GroceryItem)
                .SingleOrDefaultAsync(m => m.Id == id);
            if (mealItemIngredient == null)
            {
                return NotFound();
            }

            return View(mealItemIngredient);
        }

        // POST: MealItemIngredient/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var mealItemIngredient = await _context.MealItemIngredients.SingleOrDefaultAsync(m => m.Id == id);
            _context.MealItemIngredients.Remove(mealItemIngredient);
            await base.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool MealItemIngredientExists(int id)
        {
            return _context.MealItemIngredients.Any(e => e.Id == id);
        }
    }
}
