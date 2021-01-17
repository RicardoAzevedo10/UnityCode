using Microsoft.VisualBasic.CompilerServices;

namespace ConsumeParseCSVOne
{
    public class Person
    {//This is for the user put the data in this case name,email and country and consume that data
        public string Name { get; set; }
        public string Email { get; set; }
        public string Coutry { get; set; }

        public Person(string name, string email, string country)
        {
            Name = name;
            Email = email;
            Coutry = country;
        }
    }
}
