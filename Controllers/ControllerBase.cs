using System.Threading.Tasks;
using clean_aspnet_mvc.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

[Authorize]
public class ControllerBase : Controller
{


    private CurrentLoggedInUser _currentLoggedInuser;

    public ApplicationDbContext DBContext {get; private set;}

    public override void OnActionExecuting(ActionExecutingContext filterContext)
    {
        if (GetLoggedInUser() != null)
        {

            Locations currentLocation = _currentLoggedInuser.GetCurrentLocation();
            ViewData["CurrentLocationName"] = "No Location Set";
            if (currentLocation != null )
            {
                ViewData["CurrentLocationName"] = currentLocation.LocationName;
            }
        }
    }
    public ControllerBase(ApplicationDbContext dbContext)
    {
        DBContext = dbContext;

    }
    public CurrentLoggedInUser GetLoggedInUser()
    {

        if (_currentLoggedInuser == null)
        {
            try{
                if (User!= null && User.Identity != null && User.Identity.IsAuthenticated)
                {
                    _currentLoggedInuser = new CurrentLoggedInUser(HttpContext, DBContext);
                }
            }
            catch{

            }
        }
        return _currentLoggedInuser;
    }

    public async Task<int> SaveChangesAsync()
    {
        return await DBContext.SaveChangesAsync(GetLoggedInUser().GetCurrentLocation());
    }

    public int SaveChanges()
    {
        return DBContext.SaveChanges(GetLoggedInUser().GetCurrentLocation());
    }

}
