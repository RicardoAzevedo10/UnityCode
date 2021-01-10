using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.IO;
using System.Net;

namespace WriteJsonOne
{
    class Program
    {
        static void Main(string[] args)
        {
            Dictionary<string, List<Item>> items = new Dictionary<string, List<Item>>();
            List<Item> hams = new List<Item>()
            {
                new Item()
                {
                    Name = "Big Mac",
                    Price = 1.5f,
                    Calories = 300
                },

                new Item()
                {
                    Name = "Cheese",
                    Price = 0.5f,
                    Calories = 125
                }
            };
            
            List<Item> sides = new List<Item>()
            {
                new Item()
                {
                    Name = "Chips",
                    Price = 1.5f,
                    Calories = 300
                },

                new Item()
                {
                    Name = "Nugets",
                    Price = 0.5f,
                    Calories = 125
                }
            };
            
            items.Add("Hams", hams);
            items.Add("Sides", sides);
            
            JsonSerializerSettings settings = new JsonSerializerSettings();
            settings.Formatting = Formatting.Indented;
            string json = JsonConvert.SerializeObject(items, settings);
            
            File.WriteAllText("output.json", json);
        }
    }
}