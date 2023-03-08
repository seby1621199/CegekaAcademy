namespace PetShelter.Api.Resources.Extensions
{
    public static class FundraiserExtensions
    {
        public static Domain.Fundraiser AsDomainModel(this Fundraiser fundraiser)
        {
            return new Domain.Fundraiser
            {
                Name = fundraiser.Name,
                Description = fundraiser.Description,
                Target = fundraiser.Target,
                EndDate = fundraiser.EndDate,
                Owner = fundraiser.Owner.AsDomainModel(),
            };
        }

    }
}
