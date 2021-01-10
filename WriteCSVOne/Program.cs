using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;

namespace WriteCSVOne
{
    class Program
    {
        static void Main(string[] args)
        {
            List<Person> people = new List<Person>();
            people.Add(new Person("Rui", "rui@coiso.com", "Portugal"));
            people.Add(new Person("Ricardo", "ricardo@coiso.com", "Portugal")); 
            people.Add(new Person("Hugo", "hugo@coiso.com", "Portugal"));
            StringBuilder sb = new StringBuilder();

            using (StreamWriter writer = new StreamWriter("dados.txt"))
            {
                foreach (Person person in people)
                {
                    writer.WriteLine($"{person.Name}|{person.Email}|{person.Coutry}");
                }   
            }
            
            // foreach (Person person in people)
            // {
            //     sb.Append(person.Name).Append("|")
            //         .Append(person.Email).Append("|")
            //         .Append(person.Coutry).AppendLine();
            // }
            //
            // File.WriteAllText("dados2.txt", sb.ToString());
        }
    }
}