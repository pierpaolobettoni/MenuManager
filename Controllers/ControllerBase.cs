using clean_aspnet_mvc.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
 [Authorize]
public class ControllerBase : Controller
{


    private CurrentLoggedInUser _currentLoggedInuser;

    protected ApplicationDbContext DBContext {get; private set;}

    public ControllerBase(ApplicationDbContext dbContext)
    {
        DBContext = dbContext;
    }
    public CurrentLoggedInUser GetLoggedInUser()
    {

        if (_currentLoggedInuser == null)
        {
            if (User.Identity.IsAuthenticated)
            {
                _currentLoggedInuser = new CurrentLoggedInUser(HttpContext, DBContext);
            }
        }
        return _currentLoggedInuser;
    }

}
