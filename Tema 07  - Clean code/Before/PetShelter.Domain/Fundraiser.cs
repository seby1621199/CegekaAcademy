namespace PetShelter.Domain
{
    public class Fundraiser : INamedEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public Person Owner { get; set; }
        public int Target { get; set; }
        public DateTime EndDate { get; set; }
        public int Current_Donation_Amount { get; set; }
        public string Status { get; set; }
    }
}
