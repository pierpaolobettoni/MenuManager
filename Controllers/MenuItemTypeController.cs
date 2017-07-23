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
    public class MenuItemTypeController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public MenuItemTypeController(ApplicationDbContext context)
        : base(context)
        {
            _context = context;
        }

        // GET: MenuItemType
        public async Task<IActionResult> Index()
        {
            return View(await _context.MenuItemTypes.Where(x => x.Location == GetLoggedInUser().GetCurrentLocation()).ToListAsync());
        }

        // GET: MenuItemType/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var menuItemType = await _context.MenuItemTypes
                .SingleOrDefaultAsync(m => m.Id == id);
            if (menuItemType == null)
            {
                return NotFound();
            }

            return View(menuItemType);
        }

        // GET: MenuItemType/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: MenuItemType/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name")] MenuItemType menuItemType)
        {
            if (ModelState.IsValid)
            {
                _context.Add(menuItemType);
                await SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(menuItemType);
        }

        // GET: MenuItemType/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var menuItemType = await _context.MenuItemTypes.SingleOrDefaultAsync(m => m.Id == id);
            if (menuItemType == null)
            {
                return NotFound();
            }
            return View(menuItemType);
        }

        // POST: MenuItemType/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name")] MenuItemType menuItemType)
        {
            if (id != menuItemType.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(menuItemType);
                    await base.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MenuItemTypeExists(menuItemType.Id))
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
            return View(menuItemType);
        }

        // GET: MenuItemType/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var menuItemType = await _context.MenuItemTypes
                .SingleOrDefaultAsync(m => m.Id == id);
            if (menuItemType == null)
            {
                return NotFound();
            }

            return View(menuItemType);
        }

        // POST: MenuItemType/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var menuItemType = await _context.MenuItemTypes.SingleOrDefaultAsync(m => m.Id == id);
            _context.MenuItemTypes.Remove(menuItemType);
            await base.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool MenuItemTypeExists(int id)
        {
            return _context.MenuItemTypes.Any(e => e.Id == id);
        }
    }
}
