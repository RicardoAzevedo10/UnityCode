using System;
using System.Collections.Generic;
using System.IO;
using System.Linq.Expressions;

namespace ConsumeParseCSVOne
{
    class Program
    {
        static void Main(string[] args)
        {
            List<Person> people = new List<Person>();
            using (StreamReader reader = new StreamReader("data.txt"))
            {
                string line;
            
                while ((line = reader.ReadLine()) != null)
                {
                    string[] tokens = line.Split('|');
                    people.Add(new Person(tokens[0], tokens[1], tokens[2]));
                }   
            }
        }
    }
}