namespace PetShelter.Api.Resources
{
    public class Fundraiser
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int Target { get; set; }
        public DateTime EndDate { get; set; }
        public Person Owner { get; set; }
    }
}
