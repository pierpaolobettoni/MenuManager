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
    public class GroceryItemController : Controller
    {
        private readonly ApplicationDbContext _context;

        public GroceryItemController(ApplicationDbContext context)
        {
            _context = context;    
        }

        // GET: GroceryItem
        public async Task<IActionResult> Index()
        {
            return View(await _context.GroceryItems.ToListAsync());
        }

        // GET: GroceryItem/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var groceryItem = await _context.GroceryItems
                .SingleOrDefaultAsync(m => m.Id == id);
            if (groceryItem == null)
            {
                return NotFound();
            }

            return View(groceryItem);
        }

        // GET: GroceryItem/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: GroceryItem/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,GroceryItemName")] GroceryItem groceryItem)
        {
            if (ModelState.IsValid)
            {
                _context.Add(groceryItem);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(groceryItem);
        }

        // GET: GroceryItem/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var groceryItem = await _context.GroceryItems.SingleOrDefaultAsync(m => m.Id == id);
            if (groceryItem == null)
            {
                return NotFound();
            }
            return View(groceryItem);
        }

        // POST: GroceryItem/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,GroceryItemName")] GroceryItem groceryItem)
        {
            if (id != groceryItem.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(groceryItem);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!GroceryItemExists(groceryItem.Id))
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
            return View(groceryItem);
        }

        // GET: GroceryItem/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var groceryItem = await _context.GroceryItems
                .SingleOrDefaultAsync(m => m.Id == id);
            if (groceryItem == null)
            {
                return NotFound();
            }

            return View(groceryItem);
        }

        // POST: GroceryItem/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var groceryItem = await _context.GroceryItems.SingleOrDefaultAsync(m => m.Id == id);
            _context.GroceryItems.Remove(groceryItem);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool GroceryItemExists(int id)
        {
            return _context.GroceryItems.Any(e => e.Id == id);
        }
    }
}
