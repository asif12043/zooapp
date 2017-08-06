using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ZooApp.Models
{
    public class AnimalFoodTotal
    {
        public AnimalFoodTotal()
        {
                
        }       

        public AnimalFoodTotal(AnimalFood animalFood)
        {
            FoodName = animalFood.Food.Name;
            FoodPrice = animalFood.Food.Price;
            TotalQuantity = animalFood.Animal.Quantiy * animalFood.Quantiy;
            TotalPrice = animalFood.Food.Price * TotalQuantity;

        }

        public string FoodName { get; set; }

        public double FoodPrice { get;  set; }

        public double TotalPrice { get;  set; }

        public double TotalQuantity { get;  set; }
    }
}