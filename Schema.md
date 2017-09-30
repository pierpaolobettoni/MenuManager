# Schema explained

The MenuManager schema is highly hierarchical (and at times confusing). For us to be able to compute a shopping list we have to fill up a setof bottom up relationships.


### Locations
It identifies the phisical place for which we are managing menus. Most user accounts will be associated to one location, but it's possible to have multiple locations per account.

### GroceryItems AKA Ingredients
They are at the same time items to be bought (Grocery Items) and items that can be added to a recipe (Ingredients).

### MealItemIngredients 
These are stored in the database as MealItemIngredients. They associate a GroceryItem with a meal Item.

### MealItems AKA Recipes
They represent single recipes that can be added to a menu. In other words you compose a menu by adding Recipes to it. Another way of thinking about it is to think of Recipes as the different types of foods that are included in a menu.


