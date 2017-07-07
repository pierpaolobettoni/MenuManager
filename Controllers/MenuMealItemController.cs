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
    public class MenuMealItemController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public MenuMealItemController(ApplicationDbContext context)
        : base(context)
        {
            _context = context;
        }

        // GET: MenuMealItem
        public async Task<IActionResult> Index()
        {
            return View(await _context.MenuMealItem.ToListAsync());
        }

        // GET: MenuMealItem/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var menuMealItem = await _context.MenuMealItem
                .SingleOrDefaultAsync(m => m.Id == id);
            if (menuMealItem == null)
            {
                return NotFound();
            }

            return View(menuMealItem);
        }

        // GET: MenuMealItem/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: MenuMealItem/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id")] MenuMealItem menuMealItem)
        {
            if (ModelState.IsValid)
            {
                _context.Add(menuMealItem);
                await base.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(menuMealItem);
        }

        // GET: MenuMealItem/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var menuMealItem = await _context.MenuMealItem.SingleOrDefaultAsync(m => m.Id == id);
            if (menuMealItem == null)
            {
                return NotFound();
            }
            return View(menuMealItem);
        }

        // POST: MenuMealItem/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id")] MenuMealItem menuMealItem)
        {
            if (id != menuMealItem.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(menuMealItem);
                    await base.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MenuMealItemExists(menuMealItem.Id))
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
            return View(menuMealItem);
        }

        // GET: MenuMealItem/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var menuMealItem = await _context.MenuMealItem
                .SingleOrDefaultAsync(m => m.Id == id);
            if (menuMealItem == null)
            {
                return NotFound();
            }

            return View(menuMealItem);
        }

        // POST: MenuMealItem/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var menuMealItem = await _context.MenuMealItem.SingleOrDefaultAsync(m => m.Id == id);
            _context.MenuMealItem.Remove(menuMealItem);
            await base.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool MenuMealItemExists(int id)
        {
            return _context.MenuMealItem.Any(e => e.Id == id);
        }
    }
}
