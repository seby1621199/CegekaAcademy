namespace PetShelterDemo.Domain
{
    public class Fundraiser : INamedEntity //l-am facut asa pentru a nu crea metode noi de Inserare&Afisare in baza de date
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int Target { get; set; }
        public int TotalAmount { get; set; } = 0;
        public List<Person> Persons { get; set; } = new List<Person>();
        public Fundraiser(string title, string description, int target)
        {
            Name = title;
            Description = description;
            Target = target;

        }
        public void Donate(Person person,int amountDonated)
        {
            if (TotalAmount+ amountDonated<=Target)
            {
                Persons.Add(person);
                TotalAmount += amountDonated;
            }
            else
            {
                if (TotalAmount == Target)
                    Console.WriteLine("Closed.");
            }
        }
    }
}
