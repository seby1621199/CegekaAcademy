namespace PetShelter.DataAccessLayer.Models
{
    public class Fundraiser : IEntity
    {

        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public Person Owner { get; set; }
        public int OwnerId { get; set; }
        public DateTime CreationDate { get; set; } = DateTime.Now;
        public DateTime EndDate { get; set; }
        public int Current_Donation_Amount { get; set; } = 0;
        public string Status { get; set; }
        public int Target { get; set; }
        public ICollection<Donation>? Donations { get; set; }
    }
}
