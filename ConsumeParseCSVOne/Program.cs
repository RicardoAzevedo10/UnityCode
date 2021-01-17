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
            // Make a array of people
            List<Person> people = new List<Person>();
            //use streamreader to read all the data and write in .txt format
            using (StreamReader reader = new StreamReader("data.txt"))
            {
                string line;
            
                while ((line = reader.ReadLine()) != null)
                {
                    string[] tokens = line.Split('|');//make another paragraph
                    people.Add(new Person(tokens[0], tokens[1], tokens[2]));
                }   
            }
        }
    }
}
