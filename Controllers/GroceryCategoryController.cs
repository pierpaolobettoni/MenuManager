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
    public class GroceryCategoryController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public GroceryCategoryController(ApplicationDbContext context)
        : base(context)
        {
            _context = context;
        }

        // GET: GroceryCategory
        public async Task<IActionResult> Index()
        {
            return View(await _context.GroceryCategory.ToListAsync());
        }

        // GET: GroceryCategory/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var groceryCategory = await _context.GroceryCategory
                .SingleOrDefaultAsync(m => m.Id == id);
            if (groceryCategory == null)
            {
                return NotFound();
            }

            return View(groceryCategory);
        }

        // GET: GroceryCategory/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: GroceryCategory/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,GroceryCategoryName,GroceryCategoryDescription")] GroceryCategory groceryCategory)
        {
            if (ModelState.IsValid)
            {
                _context.Add(groceryCategory);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(groceryCategory);
        }

        // GET: GroceryCategory/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var groceryCategory = await _context.GroceryCategory.SingleOrDefaultAsync(m => m.Id == id);
            if (groceryCategory == null)
            {
                return NotFound();
            }
            return View(groceryCategory);
        }

        // POST: GroceryCategory/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,GroceryCategoryName,GroceryCategoryDescription")] GroceryCategory groceryCategory)
        {
            if (id != groceryCategory.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(groceryCategory);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!GroceryCategoryExists(groceryCategory.Id))
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
            return View(groceryCategory);
        }

        // GET: GroceryCategory/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var groceryCategory = await _context.GroceryCategory
                .SingleOrDefaultAsync(m => m.Id == id);
            if (groceryCategory == null)
            {
                return NotFound();
            }

            return View(groceryCategory);
        }

        // POST: GroceryCategory/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var groceryCategory = await _context.GroceryCategory.SingleOrDefaultAsync(m => m.Id == id);
            _context.GroceryCategory.Remove(groceryCategory);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool GroceryCategoryExists(int id)
        {
            return _context.GroceryCategory.Any(e => e.Id == id);
        }
    }
}
