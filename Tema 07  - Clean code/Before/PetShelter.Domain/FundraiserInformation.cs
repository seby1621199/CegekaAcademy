namespace PetShelter.Domain
{
    public class FundraiserInformation
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public IReadOnlyCollection<Person> Donors { get; set; }
        public int AmountDonated { get; set; }
        public int Target { get; set; }
        public string Status { get; set; }
        public DateTime EndDate { get; set; }
    }
}
