using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using clean_aspnet_mvc.Data;
using clean_aspnet_mvc.Models.LocationsList;
using clean_aspnet_mvc.BusinessLogic;
using Microsoft.AspNetCore.Hosting;

namespace clean_aspnet_mvc.Controllers
{
    public class LocationsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private IHostingEnvironment _environment;

        public LocationsController(ApplicationDbContext context, IHostingEnvironment environment)
        : base(context)
        {
            _context = context;
            _environment = environment;
        }

        // GET: Locations
        public async Task<IActionResult> Index()
        {
            LocationsListViewModel viewModel = new LocationsListViewModel();
            viewModel.LocationsList = (from l in _context.UserLocations where l.UserName == User.Identity.Name select l.Location).ToList();
            viewModel.DefaultLocationId = await (from l in _context.UserLocations where l.UserName == User.Identity.Name && l.IsDefaultLocationForUser == true select l.Location.Id).FirstOrDefaultAsync();
            return View(viewModel);
        }

        // GET: Locations/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var locations = await _context.Locations
                .SingleOrDefaultAsync(m => m.Id == id);
            if (locations == null)
            {
                return NotFound();
            }

            return View(locations);
        }

        public async Task<IActionResult> MakeDefault(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }



            var defaultLocationRecord = (from l in _context.UserLocations where l.UserName == User.Identity.Name && l.IsDefaultLocationForUser == true select l).FirstOrDefault();
            if (defaultLocationRecord != null)
            {
                defaultLocationRecord.IsDefaultLocationForUser = false;
            }

            var userLocationRecord = (from l in _context.UserLocations where l.UserName == User.Identity.Name && l.LocationId == id select l).FirstOrDefault();
            userLocationRecord.IsDefaultLocationForUser = true;
            await base.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        // GET: Locations/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Locations/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,LocationName,Description")] Locations locations)
        {
            if (ModelState.IsValid)
            {
                var locationManager = new LocationManager(_context);
                bool isDefaultLocation = false;
                if (locationManager.LocationsForUser(User.Identity.Name).Count == 0)
                {
                    isDefaultLocation = true;
                }


                _context.Add(locations);
                await base.SaveChangesAsync();

                UserLocations userLocation = new UserLocations();
                userLocation.UserName = User.Identity.Name;
                userLocation.IsDefaultLocationForUser = isDefaultLocation;
                userLocation.Location = locations;
                _context.Add(userLocation);
                await base.SaveChangesAsync();
                DefaultDataManager defaultDataManager = new DefaultDataManager(_environment);
                await defaultDataManager.Prefill(locations, GetLoggedInUser());
                return RedirectToAction("Index");
            }
            return View(locations);
        }

        // GET: Locations/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var locations = await _context.Locations.SingleOrDefaultAsync(m => m.Id == id);
            if (locations == null)
            {
                return NotFound();
            }
            return View(locations);
        }

        // POST: Locations/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,LocationName,Description")] Locations locations)
        {
            if (id != locations.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var previousLocation = await (from l in DBContext.Locations where l.Id == locations.Id select l).FirstOrDefaultAsync();
                    if (previousLocation != null)
                    {
                        previousLocation.Description = locations.Description;
                        previousLocation.LocationName = locations.LocationName;
                    }

                    await base.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LocationsExists(locations.Id))
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
            return View(locations);
        }

        // GET: Locations/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var locations = await _context.Locations
                .SingleOrDefaultAsync(m => m.Id == id);
            if (locations == null)
            {
                return NotFound();
            }

            return View(locations);
        }

        // POST: Locations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var locations = await _context.Locations.SingleOrDefaultAsync(m => m.Id == id);
            _context.Locations.Remove(locations);
            await base.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool LocationsExists(int id)
        {
            return _context.Locations.Any(e => e.Id == id);
        }
    }
}
