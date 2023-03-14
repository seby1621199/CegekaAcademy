namespace PetShelter.Domain.Services
{
    public interface IFundraiserService
    {

        public Task<int> CreateFundraiser(Fundraiser fundraiser, Person Owner);
        public Task<IReadOnlyCollection<FundraiserSummaryInformation>> GetFundraisersAsync();
        public Task<FundraiserInformation> GetFundraiser(int id);
        public Task DonateToFundraiser(int id, Person donor, int amount);
        public Task CloseFundraiser(int id);
    }

}
