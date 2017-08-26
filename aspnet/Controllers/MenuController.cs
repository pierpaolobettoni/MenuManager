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
    public class MenuController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public MenuController(ApplicationDbContext context)
        : base(context)
        {
            _context = context;
        }

        // GET: Menu
        public async Task<IActionResult> Index()
        {
            return View(await GetLoggedInUser().GetMenus().ToListAsync());
        }

        // GET: Menu/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var menu = await _context.Menus
                .SingleOrDefaultAsync(m => m.Id == id);
            if (menu == null)
            {
                return NotFound();
            }

            return View(menu);
        }

        // GET: Menu/Create
        public IActionResult Create()
        {

            return View();
        }

        // POST: Menu/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name")] Menu menu)
        {
            if (ModelState.IsValid)
            {
                _context.Add(menu);
                await base.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(menu);
        }

        // GET: Menu/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var menu = await _context.Menus.Include("MealItems").SingleOrDefaultAsync(m => m.Id == id && m.Location == GetLoggedInUser().GetCurrentLocation());
            ViewBag.MealItems = GetLoggedInUser().GetMealItems();
            if (menu == null)
            {
                return NotFound();
            }

            return View(menu);
        }

        // POST: Menu/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name")] Menu menu)
        {
            if (id != menu.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(menu);
                    await base.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MenuExists(menu.Id))
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

            return View(menu);
        }

        // GET: Menu/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var menu = await _context.Menus
                .SingleOrDefaultAsync(m => m.Id == id);
            if (menu == null)
            {
                return NotFound();
            }

            return View(menu);
        }

        // POST: Menu/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var menu = await _context.Menus.SingleOrDefaultAsync(m => m.Id == id);
            _context.Menus.Remove(menu);
            await base.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        [HttpPost, ActionName("AddMealItem")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddMealItem(int id, [Bind("MealItemId")] MenuMealItem newMealItem)
        {

            newMealItem.MenuId = id;

            DBContext.Add(newMealItem);
            await base.SaveChangesAsync();
            return RedirectToAction("Edit", new {id = id});
        }

        public async Task<IActionResult> MenuMealItemDelete(int id, int menuMealItemId)
        {
            var itemToDelete = await (from x in DBContext.MenuMealItem where x.MenuId == id && x.Id == menuMealItemId && x.Location == GetLoggedInUser().GetCurrentLocation() select x).FirstOrDefaultAsync();
            if (itemToDelete != null)
            {
                DBContext.Remove(itemToDelete);
                await DBContext.SaveChangesAsync();
            }
            return RedirectToAction("Edit", new {id = id});
        }

        private bool MenuExists(int id)
        {
            return _context.Menus.Any(e => e.Id == id);
        }
    }
}
