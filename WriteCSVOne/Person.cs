namespace WriteCSVOne
{
    public class Person
    {
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