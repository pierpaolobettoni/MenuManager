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
        public ActionResult Index()
        {
            return View(GetLoggedInUser().GetMealItems());
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
            ViewBag.MenuItemTypes = GetLoggedInUser().GetMenuItemTypes();
            return View();
        }

        // POST: MealItem/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,MealItemName,MealItemDescription,TypeOfServing, MenuItemTypeId, NumberOfServings")] MealItem mealItem)
        {
            if (ModelState.IsValid)
            {
                _context.Add(mealItem);
                //await base.SaveChangesAsync();
                await base.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.MenuItemTypes = GetLoggedInUser().GetMenuItemTypes();
            return View(mealItem);
        }

        // GET: MealItem/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var mealItem = await _context.MealItems.Include("Ingredients").Include("Ingredients.GroceryItem").SingleOrDefaultAsync(m => m.Id == id);

            if (mealItem == null)
            {
                return NotFound();
            }
            ViewBag.MenuItemTypes = GetLoggedInUser().GetMenuItemTypes();
            return View(mealItem);
        }

        // POST: MealItem/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,MealItemName,MealItemDescription, TypeOfServing, MenuItemTypeId, NumberOfServings")] MealItem mealItem)
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
            ViewBag.MenuItemTypes = GetLoggedInUser().GetMenuItemTypes();
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

        // GET: MealItemIngredient/Create
        public IActionResult MealItemIngredientAdd(int id)
        {
            ViewData["GroceryItemId"] = new SelectList(GetLoggedInUser().GetAllGroceryItems().Result.OrderBy(x => x.GroceryItemName), "Id", "NameWithCategoryAtTheEnd");
            ViewData["MealItemId"] = id;
            return View();
        }

         public IActionResult MealItemIngredientEdit(int id)
        {
            ViewData["GroceryItemId"] = new SelectList(GetLoggedInUser().GetAllGroceryItems().Result.OrderBy(x => x.GroceryItemName), "Id", "NameWithCategoryAtTheEnd");
            ViewData["MealItemId"] = id;
            var model = (from ingredient in base.DBContext.MealItemIngredients where ingredient.Id == id && ingredient.Location == GetLoggedInUser().GetCurrentLocation() select ingredient).FirstOrDefault();
            return View(model);
        }

        // POST: MealItemIngredient/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> MealItemIngredientAdd([Bind("GroceryItemId,Quantity,MeasureType, MealItemId")] MealItemIngredient mealItemIngredient)
        {
            if (ModelState.IsValid)
            {
                _context.Add(mealItemIngredient);
                await base.SaveChangesAsync();
                return RedirectToAction("Edit", new { id = mealItemIngredient.MealItemId });
            }
            ViewData["GroceryItemId"] = new SelectList(GetLoggedInUser().GetAllGroceryItems().Result.OrderBy(x => x.GroceryItemName), "Id", "NameWithCategoryAtTheEnd", mealItemIngredient.GroceryItemId);
            return View(mealItemIngredient);
        }

        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> MealItemIngredientEdit([Bind("Id, GroceryItemId,Quantity,MeasureType")] MealItemIngredient mealItemIngredient)
        {
            if (ModelState.IsValid)
            {
                var existingItem = (from i in DBContext.MealItemIngredients where i.Id == mealItemIngredient.Id && i.Location == GetLoggedInUser().GetCurrentLocation() select i).First();
                existingItem.GroceryItemId = mealItemIngredient.GroceryItemId;
                existingItem.MeasureType = mealItemIngredient.MeasureType;
                existingItem.Quantity = mealItemIngredient.Quantity;
                await base.SaveChangesAsync();
                return RedirectToAction("Edit", new { id = existingItem.MealItemId });
            }
            ViewData["GroceryItemId"] = new SelectList(GetLoggedInUser().GetAllGroceryItems().Result.OrderBy(x => x.GroceryItemName), "Id", "NameWithCategoryAtTheEnd", mealItemIngredient.GroceryItemId);
            return View();
        }
    }
}
