using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace clean_aspnet_mvc.Controllers
{
    public class HomeController : Controller
    {
        private IServiceProvider _serviceProvider;

        public HomeController(IServiceProvider provider)
        {
            _serviceProvider = provider;

        }
        public IActionResult Index()
        {
            if (User.Identity.IsAuthenticated)
            {
                var locationManager = new LocationManager(_serviceProvider);
                if (locationManager.LocationsForUser(User.Identity.Name).Count ==0) {
                    RedirectToAction("Locations");
                }
            }
            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}
