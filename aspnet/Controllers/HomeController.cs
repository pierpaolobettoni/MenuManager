using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using clean_aspnet_mvc.Data;
using clean_aspnet_mvc.Models.EmptyAccountModels;
using Microsoft.AspNetCore.Mvc;

namespace clean_aspnet_mvc.Controllers
{
    public class HomeController : Controller
    {
        private IServiceProvider _serviceProvider;
        private ApplicationDbContext _dbContext;

        public HomeController(IServiceProvider provider, ApplicationDbContext context)

        {
            _serviceProvider = provider;
            _dbContext = context;
        }
        public IActionResult Index()
        {

            if (User.Identity.IsAuthenticated)
            {
                var currentUser = new CurrentLoggedInUser(this.HttpContext, _dbContext);
                var missingSteps = currentUser.GetMissingSteps();
                if (missingSteps.Count() ==0)
                {
                    return RedirectToAction("ManageCurrent", "Locations");
                }
                return View(missingSteps);
            }
            else
            {
                 List<MissingStep> missingSteps = new List<MissingStep>();
            return View();
            }

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
