using System;
using System.Collections.Generic;

class FoodOven
{
    private Dictionary<string, int> inventory;

    public FoodOven()
    {
        inventory = new Dictionary<string, int>()
        {
            {"Muz", 0},
            {"Karadut", 0},
            {"Çilek", 0},
            {"Üzüm", 0},
            {"Elma", 0},
            {"Galu", 0},
            {"Malda", 0},
            {"Patates", 0},
            {"Patlýcan", 0},
            {"Ekmek", 0},
            {"Karpuz", 0},
            {"Havuç", 0},
            {"Biber", 0},
            {"TavukEti", 0},
            {"ÝnekKoçKoyunEti", 0},
            {"BalýkEti", 0},
            {"Pirinç", 0},
            {"Mantar", 0},
            {"HindistanCevizi", 0},
            {"Süt", 0},
            {"KahveÇekirdeði", 0}
        };
    }

    private bool CheckIngredients(Dictionary<string, int> recipeIngredients)
    {
        // Verilen tarifte gereken malzemeleri kontrol ediyorz
        // Eðer envanterde yeterli malzeme yoksa false oluyor
        foreach (var ingredient in recipeIngredients)
        {
            if (inventory.ContainsKey(ingredient.Key) && inventory[ingredient.Key] >= ingredient.Value)
            {
                continue;
            }
            else
            {
                Console.WriteLine($"Yetersiz malzeme: {ingredient.Key}");
                return false;
            }
        }
        return true;
    }

    private void ConsumeIngredients(Dictionary<string, int> recipeIngredients)
    {
        // Tarife göre kullanýlan malzemeleri envanterden azaltýyoruz
        foreach (var ingredient in recipeIngredients)
        {
            inventory[ingredient.Key] -= ingredient.Value;
        }
    }

    public void AddToInventory(string item, int quantity)
    {
        // Yemek fýrýný envanterine malzeme ekleme
        if (inventory.ContainsKey(item))
        {
            inventory[item] += quantity;
        }
    }

    public void MakeMeal(string recipe, Dictionary<string, int> recipeIngredients)
    {
        // Yemek yapma iþlemleri
        if (CheckIngredients(recipeIngredients))
        {
            ConsumeIngredients(recipeIngredients);
            AddToInventory(recipe, 1);
            Console.WriteLine($"Yemek yapýldý: {recipe}");
        }
    }


}