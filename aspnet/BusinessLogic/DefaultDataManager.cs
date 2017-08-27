using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using clean_aspnet_mvc.Data;
using Microsoft.AspNetCore.Hosting;

namespace clean_aspnet_mvc.BusinessLogic
{
    public class DefaultDataManager
    {
        CurrentLoggedInUser _currentUser;
        private IHostingEnvironment _evironment;
        private Locations _location;

        public DefaultDataManager(IHostingEnvironment environment)
        {
            _evironment = environment;
        }

        public async Task Prefill(Locations location, CurrentLoggedInUser currentUser)
        {
            _location = location;
            _currentUser = currentUser;
            await PrefillMealSlotsAsynch();
            await PrefillGroceryCategoryAsynch();
            await PrefillEventTypesAsynch();
            await PrefillGroceryItemsAsynch();
            await PrefillMenuItemTypesAsynch();
            await PrefillMealItemsAsynch();
            await PrefillMealItemIngredientsAsynch();
        }

        private async Task PrefillMealItemsAsynch()
        {

            await Task.Factory.StartNew(() =>
            {
                PrefillMealItems();
            });

        }

        private async Task PrefillMealItemIngredientsAsynch()
        {

            await Task.Factory.StartNew(() =>
            {
                PrefillMealItemIngredients();
            });

        }

        private async Task PrefillMealSlotsAsynch()
        {

            await Task.Factory.StartNew(() =>
            {
                PrefillMealSlots();
            });

        }

        private async Task PrefillGroceryCategoryAsynch()
        {

            await Task.Factory.StartNew(() =>
            {
                PrefillGroceryCategory();
            });

        }

        private async Task PrefillEventTypesAsynch()
        {

            await Task.Factory.StartNew(() =>
            {
                PrefillEventTypes();
            });

        }

        private async Task PrefillMenuItemTypesAsynch()
        {

            await Task.Factory.StartNew(() =>
            {
                PrefillMenuItemTypes();
            });

        }

        private async Task PrefillGroceryItemsAsynch()
        {

            await Task.Factory.StartNew(() =>
            {
                PrefillGroceryItems();
            });

        }

        private void PrefillEventTypes()
        {
            var eventTypes = GetTxtFile("1eventtypes.txt");
            foreach (string eventType in eventTypes)
            {
                _currentUser.DBContext.EventTypes.Add(new EventType() { EventTypeName = eventType, Location = _location });
            }
            _currentUser.DBContext.SaveChanges();
        }
        private void PrefillGroceryCategory()
        {
            var categories = GetTxtFile("2grocerycategory.txt");
            foreach (string category in categories)
            {
                _currentUser.DBContext.GroceryCategory.Add(new GroceryCategory() { GroceryCategoryName = category, Location = _location });
            }
            _currentUser.DBContext.SaveChanges();
        }
        private void PrefillMealSlots()
        {
            var slots = GetTxtFile("3mealslottypes.txt");
            foreach (string slot in slots)
            {
                _currentUser.DBContext.EventMealSlotTypes.Add(new EventMealSlotType() { Name = slot, Location = _location });
            }
            _currentUser.DBContext.SaveChanges();
        }

        private void PrefillGroceryItems()
        {
            var categories = _currentUser.DBContext.GroceryCategory.Where(x => x.Location == _location).ToList();
            var rows = GetTxtFile("4groceryitems.txt");
            foreach (string thisRow in rows)
            {
                var rowCells = thisRow.Split(',');
                var categoryName = rowCells[1].Trim();
                var groceryItemName = rowCells[0].Trim();
                var category = categories.Where(x => x.GroceryCategoryName == categoryName).FirstOrDefault();
                if (category != null)
                {
                    _currentUser.DBContext.GroceryItems.Add(
                        new GroceryItem()
                        {
                            GroceryItemName = groceryItemName,
                            Location = _location,
                            GroceryCategory = category
                        });
                }
                else
                {
                    throw new Exception("Cannot find category for GroceryItem " + groceryItemName);
                }

            }
            _currentUser.DBContext.SaveChanges();
        }



        private void PrefillMenuItemTypes()
        {
            var rows = GetTxtFile("5mealitemtypes.txt");
            foreach (string thisRow in rows)
            {
                _currentUser.DBContext.MenuItemType.Add(new MenuItemType() { Name = thisRow, Location = _location });
            }
            _currentUser.DBContext.SaveChanges();
        }

        private void PrefillMealItems()
        {
            var menuItemTypes = _currentUser.DBContext.MenuItemTypes.Where(x => x.Location == _location).ToList();

            var rows = GetTxtFile("6mealitems.txt");
            foreach (string thisRow in rows)
            {
                if (!string.IsNullOrEmpty(thisRow))
                {
                    var rowCells = thisRow.Split(',');
                    var name = rowCells[0].Trim();
                    var description = rowCells[1].Trim();
                    var typeOfServing = rowCells[2].Trim();
                    var menuItemType = rowCells[3].Trim();
                    var numberOfServings = int.Parse(rowCells[4].Trim());
                    var thisMenuItemType = menuItemTypes.Where(x => x.Name == menuItemType).FirstOrDefault();
                    if (thisMenuItemType == null)

                        throw new Exception("Can't find menu item type for " + menuItemType);

                    _currentUser.DBContext.MealItems.Add(
                        new MealItem()
                        {
                            MealItemName = name,
                            MealItemDescription = description,
                            MenuItemTypeId = thisMenuItemType.Id,
                            TypeOfServing = typeOfServing,
                            NumberOfServings = numberOfServings,
                            Location = _location
                        });
                }
            }
            _currentUser.DBContext.SaveChanges();
        }

        private void PrefillMealItemIngredients()
        {
            var allMealItems = _currentUser.DBContext.MealItems.Where(x => x.Location == _location).ToList();
            var allGroceryItems = _currentUser.DBContext.GroceryItems.Where(x => x.Location == _location).ToList();

            var rows = GetTxtFile("7mealitemingredients.txt");
            foreach (string thisRow in rows)
            {
                if (!string.IsNullOrEmpty(thisRow))
                {
                    var rowCells = thisRow.Split(',');
                    var groceryItemName = rowCells[0].Trim();
                    var quantity = Decimal.Parse(rowCells[1].Trim());
                    var measure = rowCells[2].Trim();
                    var mealItemName = rowCells[3].Trim();


                    var thisMealItem = allMealItems.Where(x => x.MealItemName == mealItemName).FirstOrDefault();
                    if (thisMealItem == null)
                        throw new Exception("Can't find menu meal item for " + mealItemName);
                    var thisGroceryItem = allGroceryItems.Where(x => x.GroceryItemName == groceryItemName).FirstOrDefault();
                    if (groceryItemName == null)
                        throw new Exception("Can't find grocoery item type for " + groceryItemName);
                    _currentUser.DBContext.MealItemIngredients.Add(
                        new MealItemIngredient()
                        {
                            GroceryItem = thisGroceryItem,
                            MealItem = thisMealItem,
                            MeasureType = measure,
                            Quantity = quantity,
                            Location = _location
                        });
                }
            }
            _currentUser.DBContext.SaveChanges();
        }

        private string[] GetTxtFile(string fileName)
        {
            string localPath = Path.Combine(_evironment.WebRootPath, @"data/defaultimports/" + fileName);
            return File.ReadAllLines(localPath);
        }


    }
}
