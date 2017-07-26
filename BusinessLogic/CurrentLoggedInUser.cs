
using clean_aspnet_mvc.Data;
using System.Linq;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System.Collections;
using clean_aspnet_mvc.Models.EmptyAccountModels;
using System;

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

    internal List<EventMealSlotType> GetEventMealSlotTypes()
    {
        return (from x in _dbContext.EventMealSlotTypes where x.Location == GetCurrentLocation() select x).ToList();
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

    internal async Task CalculateShoppingList(int[] selectedMealIds)
    {
        var meals = await DBContext.EventMeal.Include("Event").Include("EventMealSlot").Include("Menu").Where(x => selectedMealIds.Contains(x.Id)).OrderBy(x => x.MealDate).ToArrayAsync();
    }

    public ICollection<Locations> GetCurrentLocations()
    {
        return _dbContext.UserLocations
        .Where(l => l.UserName == UserName() && l.IsDefaultLocationForUser == true)
        .Select(l => l.Location).ToList();
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
        if (currentLocation != null)
        {
            retValue = _dbContext.EventTypes.Where(x => x.Location == GetCurrentLocation()).ToList();
        }
        return retValue;
    }

    internal IQueryable<Menu> GetMenus()
    {
        return (from m in _dbContext.Menus where m.Location == GetCurrentLocation() select m);
    }

    public List<MenuItemType> GetMenuItemTypes()
    {
        var retValue = new List<MenuItemType>();
        var currentLocation = GetCurrentLocation();
        if (currentLocation != null)
        {
            retValue = _dbContext.MenuItemTypes.Where(x => x.Location == GetCurrentLocation()).ToList();
        }
        return retValue;
    }
    public List<MealItem> GetMealItems()
    {
        return _dbContext.MealItems.Include("MenuItemType")
        .Where(e => e.Location == GetCurrentLocation())
        .OrderBy(x => x.MenuItemType.Name).ThenBy(x => x.MealItemName)
        .ToList<MealItem>();
    }

    public IQueryable<GroceryItem> GetGroceryItems()
    {
        return _dbContext.GroceryItems
        .Where(e => e.Location == GetCurrentLocation());
    }

    public List<GroceryCategory> GetGroceryCategories()
    {
        return _dbContext.GroceryCategory
        .Where(e => e.Location == GetCurrentLocation())
        .ToList();
    }

    public List<MealItemIngredient> GetMealItemIngredients()
    {
        return _dbContext.MealItemIngredients
        .Where(e => e.Location == GetCurrentLocation())
        .ToList();
    }

    public ApplicationDbContext DBContext => _dbContext;

    public List<MissingStep> GetMissingSteps()
    {
        List<MissingStep> retList = new List<MissingStep>();
        if (GetCurrentLocation() == null)
        {
            var step = new MissingStep();
            step.Name = "Please enter at least one location";
            step.RedirectToUrl = "/Locations";
            retList.Add(step);
        }
        else
        {
            retList = AddMisttingStepIfCollectionIsEmpty(GetEventTypes(), "There are no event types", "/EventType", retList);
            retList = AddMisttingStepIfCollectionIsEmpty(GetGroceryCategories(), "There are no Grocery Categories", "/GroceryCategory", retList);
            retList = AddMisttingStepIfCollectionIsEmpty(GetEventMealSlotTypes(), "There are no Event Meal Slots", "/EventMealSlotType", retList);
            retList = AddMisttingStepIfCollectionIsEmpty(GetMenuItemTypes(), "There are no Menu Item Types", "/MenuItemType", retList);
        }
        return retList;
    }

    private List<MissingStep> AddMisttingStepIfCollectionIsEmpty(ICollection collection, string message, string redirectTo, List<MissingStep> resultList)
    {

        if (collection == null || collection.Count == 0)
        {
            var step = new MissingStep();
            step.Name = message;
            step.RedirectToUrl = redirectTo;
            resultList.Add(step);
        }
        return resultList;

    }
}
