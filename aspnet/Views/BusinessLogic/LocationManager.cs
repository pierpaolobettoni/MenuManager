using clean_aspnet_mvc.Data;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;
using System;

public class LocationManager
{
    private ApplicationDbContext _dbContext;

    public LocationManager( IServiceProvider provider)
    {

        _dbContext = provider.GetService<ApplicationDbContext>();
    }

    public LocationManager( ApplicationDbContext applicationDbContext)
    {

       _dbContext = applicationDbContext;
    }
    public List<Locations> LocationsForUser(string userName)
    {
        return _dbContext.UserLocations.Where(l => l.UserName == userName).Select(l => l.Location).ToList();
    }

}
