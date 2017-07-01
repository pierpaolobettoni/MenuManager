
using clean_aspnet_mvc.Data;
using System.Linq;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;



public class CurrentLoggedInUser
{
    private ApplicationDbContext _dbContext;
    private HttpContext _httpContext;

    public CurrentLoggedInUser(HttpContext httpContext, ApplicationDbContext dbContext)
    {
        if (!httpContext.User.Identity.IsAuthenticated)
        {
            throw new System.InvalidOperationException("There's no logged in user");
        }
        _dbContext = dbContext;
        _httpContext = httpContext;
    }

    public string UserName()
    {
        return _httpContext.User.Identity.Name;
    }

    public Locations GetCurrentLocation()
    {
        return _dbContext.UserLocations
        .Where(l => l.UserName == UserName() && l.IsDefaultLocationForUser == true)
        .Select(l => l.Location)
        .FirstOrDefault();
    }

    public async Task<List<Event>> GetEvents()
    {
        return await _dbContext.Events
        .Where(e => e.Location == GetCurrentLocation())
        .ToListAsync<Event>();
    }

    public List<EventType> GetEventTypes()
    {
        var retValue = new List<EventType>();
        var currentLocation = GetCurrentLocation();
        if (currentLocation!= null)
        {
            retValue = _dbContext.EventTypes.Where(x => x.Location == GetCurrentLocation()).ToList();
        }
        return retValue;
    }
}
