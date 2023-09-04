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
            {"�ilek", 0},
            {"�z�m", 0},
            {"Elma", 0},
            {"Galu", 0},
            {"Malda", 0},
            {"Patates", 0},
            {"Patl�can", 0},
            {"Ekmek", 0},
            {"Karpuz", 0},
            {"Havu�", 0},
            {"Biber", 0},
            {"TavukEti", 0},
            {"�nekKo�KoyunEti", 0},
            {"Bal�kEti", 0},
            {"Pirin�", 0},
            {"Mantar", 0},
            {"HindistanCevizi", 0},
            {"S�t", 0},
            {"Kahve�ekirde�i", 0}
        };
    }

    private bool CheckIngredients(Dictionary<string, int> recipeIngredients)
    {
        // Verilen tarifte gereken malzemeleri kontrol ediyorz
        // E�er envanterde yeterli malzeme yoksa false oluyor
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
        // Tarife g�re kullan�lan malzemeleri envanterden azalt�yoruz
        foreach (var ingredient in recipeIngredients)
        {
            inventory[ingredient.Key] -= ingredient.Value;
        }
    }

    public void AddToInventory(string item, int quantity)
    {
        // Yemek f�r�n� envanterine malzeme ekleme
        if (inventory.ContainsKey(item))
        {
            inventory[item] += quantity;
        }
    }

    public void MakeMeal(string recipe, Dictionary<string, int> recipeIngredients)
    {
        // Yemek yapma i�lemleri
        if (CheckIngredients(recipeIngredients))
        {
            ConsumeIngredients(recipeIngredients);
            AddToInventory(recipe, 1);
            Console.WriteLine($"Yemek yap�ld�: {recipe}");
        }
    }


}