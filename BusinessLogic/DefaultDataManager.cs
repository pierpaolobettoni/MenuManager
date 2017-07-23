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

        private async Task PrefillGroceryItemsAsynch()
        {

            await Task.Factory.StartNew(() =>
            {
                PrefillGroceryItems();
            });

        }

        private void PrefillMealSlots()
        {
            var slots = GetTxtFile("MealSlotTypes.txt");
            foreach (string slot in slots)
            {
                _currentUser.DBContext.EventMealSlotTypes.Add(new EventMealSlotType() { Name = slot, Location = _location });
            }
            _currentUser.DBContext.SaveChanges();
        }

        private void PrefillGroceryCategory()
        {
            var categories = GetTxtFile("Grocery_Category.txt");
            foreach (string category in categories)
            {
                _currentUser.DBContext.GroceryCategory.Add(new GroceryCategory() { GroceryCategoryName = category, Location = _location });
            }
            _currentUser.DBContext.SaveChanges();
        }

        private void PrefillEventTypes()
        {
            var eventTypes = GetTxtFile("EventTypes.txt");
            foreach (string eventType in eventTypes)
            {
                _currentUser.DBContext.EventTypes.Add(new EventType() { EventTypeName = eventType, Location = _location });
            }
            _currentUser.DBContext.SaveChanges();
        }

        private void PrefillGroceryItems()
        {
            var categories = _currentUser.DBContext.GroceryCategory.Where(x => x.Location == _location).ToList();
            var rows = GetTxtFile("GroceryItems.txt");
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

        private string[] GetTxtFile(string fileName)
        {
            string localPath = Path.Combine(_evironment.WebRootPath, @"data/DefaultImports/" + fileName);
            return File.ReadAllLines(localPath);
        }


    }
}
