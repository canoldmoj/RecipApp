using System;
using System.Collections.Generic;
using System.Linq;

namespace RecipeApp
{



    // class that consists of declarations that will be used to store our ingridient information
    public class Ingredient
    {
        public string Name { get; set; } // declarations consists of getter and setters to allow access for recepe class. 
        public double Quantity { get; set; }
        public string Unit { get; set; }
        public double Calories { get; set; }
        public string FoodGroup { get; set; }
    }


    // class that consists of declarations that will be used to store our steps information
    public class Step
    {
        public int Number { get; set; }
        public string Description { get; set; }
    }

    // class that consists of declarations that will be used to store our ingridient information
    public class Recipe
    {
        public string Name { get; set; }
        public List<Ingredient> Ingredients { get; set; }  // arraylist where our ingriedients input will be stored
        public List<Step> Steps { get; set; }


        // Method that will calculate and return total calories for selected recipes
        public double CalculateTotalCalories()
        {
            return Ingredients.Sum(i => i.Calories);
        }


        public void ScaleRecipe(double factor)
        {
            foreach (var ingredient in Ingredients)
            {
                ingredient.Quantity *= factor;
            }
        }
        // reset quanties method
        public void ResetQuantities()
        {
            foreach (var ingredient in Ingredients)
            {
                ingredient.Quantity /= ingredient.Quantity;
            }
        }
    }


    // main class
    public class Program
    {
        // Arraylist that stores recipes input from user

        private static List<Recipe> recipes = new List<Recipe>();

        private static void Main(string[] args)  // main method
        {
            while (true)
            {
                Console.WriteLine("1. Enter a new recipe");
                Console.WriteLine("2. Display all recipes");
                Console.WriteLine("3. Select a recipe to display");
                Console.WriteLine("4. Clear all data");
                Console.WriteLine("5. Exit");
                Console.WriteLine();

                Console.Write("Enter your choice: ");
                string choice = Console.ReadLine();
                Console.WriteLine();

                switch (choice)
                {
                    case "1":
                        EnterNewRecipe();
                        break;
                    case "2":
                        DisplayAllRecipes();
                        break;
                    case "3":
                        SelectRecipeToDisplay();
                        break;
                    case "4":
                        ClearAllData();
                        break;
                    case "5":
                        Environment.Exit(0);
                        break;
                    default:
                        Console.WriteLine("Invalid choice. Please try again.");
                        break;
                }

                Console.WriteLine();
            }
        }

        // new recepe method
        private static void EnterNewRecipe()
        {
            Recipe recipe = new Recipe();

            Console.Write("Enter the name of the recipe: ");
            recipe.Name = Console.ReadLine();
            Console.WriteLine();

            Console.Write("Enter the number of ingredients: ");
            int ingredientCount = int.Parse(Console.ReadLine());
            Console.WriteLine();

            recipe.Ingredients = new List<Ingredient>();
            for (int i = 1; i <= ingredientCount; i++)
            {
                Ingredient ingredient = new Ingredient();

                Console.Write($"Enter the name of ingredient {i}: ");
                ingredient.Name = Console.ReadLine();

                Console.Write($"Enter the quantity of ingredient {i}: ");
                ingredient.Quantity = double.Parse(Console.ReadLine());

                Console.Write($"Enter the unit of measurement for ingredient {i}: ");
                ingredient.Unit = Console.ReadLine();

                Console.Write($"Enter the number of calories for ingredient {i}: ");
                ingredient.Calories = double.Parse(Console.ReadLine());

                Console.Write($"Enter the food group for ingredient {i}: ");
                ingredient.FoodGroup = Console.ReadLine();

                recipe.Ingredients.Add(ingredient);
                Console.WriteLine();
            }

            Console.Write("Enter the number of steps: ");
            int stepCount = int.Parse(Console.ReadLine());
            Console.WriteLine();

            recipe.Steps = new List<Step>();
            for (int i = 1; i <= stepCount; i++)
            {
                Step step = new Step();

                Console.Write($"Enter step {i}: ");
                step.Description = Console.ReadLine();
                step.Number = i;

                recipe.Steps.Add(step);
                Console.WriteLine();
            }

            recipes.Add(recipe);

            Console.WriteLine("Recipe added successfully!");
        }

        // display recepi method
        private static void DisplayAllRecipes()
        {
            if (recipes.Count == 0)
            {
                Console.WriteLine("No recipes available.");
            }
            else
            {
                Console.WriteLine("All Recipes:");
                Console.WriteLine();

                foreach (var recipe in recipes.OrderBy(r => r.Name))
                {
                    Console.WriteLine(recipe.Name);
                }
            }
        }

        // display recepi method
        private static void SelectRecipeToDisplay()
        {
            if (recipes.Count == 0)
            {
                Console.WriteLine("No recipes available.");
            }
            else
            {
                Console.WriteLine("Select a recipe to display:");
                Console.WriteLine();

                int i = 1;
                foreach (var recipe in recipes.OrderBy(r => r.Name))
                {
                    Console.WriteLine($"{i}. {recipe.Name}");
                    i++;
                }

                Console.WriteLine();

                Console.Write("Enter the number of the recipe: ");
                int recipeNumber = int.Parse(Console.ReadLine());
                Console.WriteLine();

                if (recipeNumber >= 1 && recipeNumber <= recipes.Count)
                {
                    Recipe selectedRecipe = recipes.OrderBy(r => r.Name).ToList()[recipeNumber - 1];
                    DisplayRecipe(selectedRecipe);
                }
                else
                {
                    Console.WriteLine("Invalid recipe number.");
                }
            }
        }
        // this method calls the the user input to display all information stored in the recipi arraylist
        private static void DisplayRecipe(Recipe recipe)
        {
            Console.WriteLine($"Recipe: {recipe.Name}");
            Console.WriteLine();

            Console.WriteLine("Ingredients:");
            foreach (var ingredient in recipe.Ingredients)
            {
                Console.WriteLine($"{ingredient.Name}: {ingredient.Quantity} {ingredient.Unit}");
            }

            Console.WriteLine();

            Console.WriteLine("Steps:");
            foreach (var step in recipe.Steps)
            {
                Console.WriteLine($"{step.Number}. {step.Description}");
            }

            Console.WriteLine();

            Console.Write("Do you want to scale the recipe (0.5, 2, 3) or reset the quantities? (s/r/n): "); // this line allows the user to adjust the recipe scale
            string choice = Console.ReadLine();

            switch (choice)    // switch statement to calculate based on user choice
            {
                case "0.5":
                    recipe.ScaleRecipe(0.5);
                    DisplayRecipe(recipe);
                    break;
                case "2":
                    recipe.ScaleRecipe(2);
                    DisplayRecipe(recipe);
                    break;
                case "3":
                    recipe.ScaleRecipe(3);
                    DisplayRecipe(recipe);
                    break;
                case "r":
                    recipe.ResetQuantities();
                    DisplayRecipe(recipe);
                    break;
                case "n":
                    break;
                default:
                    Console.WriteLine("Invalid choice.");
                    break;
            }
        }

        // clear all data method
        private static void ClearAllData()
        {
            recipes.Clear();
            Console.WriteLine("All data cleared.");
        }
    }
}