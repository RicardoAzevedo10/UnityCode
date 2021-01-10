using System;
using System.Collections.Generic;
using System.Net;
using System.Text.Json.Serialization;
using Newtonsoft.Json;
using System.IO;

namespace ConsumeJSONOne
{
    class Program
    {
        static void Main(string[] args)
        {
            string content = File.ReadAllText("items.json");
            Dictionary<string, List<Item>> items = JsonConvert.DeserializeObject<Dictionary<string, List<Item>> >(content);

            List<Item> sides = items["Sides"];
            
            foreach (KeyValuePair<string,List<Item>> item in items)
            {    
                Console.WriteLine(item.Key);
                
                foreach (Item food in item.Value)
                {
                    Console.WriteLine(food.Name);
                }
            }
        }
    }
}