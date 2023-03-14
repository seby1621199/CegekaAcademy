using System.Collections.Immutable;
using PetShelter.DataAccessLayer.Models;

namespace PetShelter.Domain.Extensions.DomainModel
{
    public static class FundraiserExtensions
    {
        public static FundraiserSummaryinformation ToFundraiserInfoAll(this DataAccessLayer.Models.Fundraiser fundraiser)
        {
            return
                new FundraiserSummaryinformation
                {
                    Id = fundraiser.Id,
                    Name = fundraiser.Name,
                    Status = fundraiser.Status
                };
        }

        public static Fundraiser? ToDomainModel(this DataAccessLayer.Models.Fundraiser fundraiser)
        {
            if (fundraiser == null)
            {
                return null;
            }
            return new Fundraiser
            {
                Name = fundraiser.Name,
                Description = fundraiser.Description,
                Target = fundraiser.Target,
                EndDate = fundraiser.EndDate,
                Current_Donation_Amount = fundraiser.Current_Donation_Amount,
                Owner = fundraiser.Owner.ToDomainModel(),
            };
        }

        public static FundraiserInformation ToFundraiserInformation(this DataAccessLayer.Models.Fundraiser fundraiser)
        {
            if (fundraiser == null)
            {
                return null;
            }
            return new FundraiserInformation
            {
                Name = fundraiser.Name,
                Id = fundraiser.Id,
                AmountDonated = (int)fundraiser.Donations.Sum(f => f.Amount),
                EndDate = fundraiser.EndDate,
                Status = fundraiser.Status,
                Target = fundraiser.Target,
                Donors = fundraiser.Donations.Select(d => d.Donor.ToDomainModel()).ToImmutableArray()
            };

        }

        public static PetShelter.DataAccessLayer.Models.Fundraiser FromDomainModel(this Fundraiser fundraiser)
        {
            return new PetShelter.DataAccessLayer.Models.Fundraiser
            {
                Name = fundraiser.Name,
                Description = fundraiser.Description,
                Owner = fundraiser.Owner.FromDomainModel(),
                EndDate = fundraiser.EndDate,
                Target = fundraiser.Target,
                CreationDate=DateTime.Now,
                Donations =new List<Donation>(),
                Status="Active",
                Current_Donation_Amount = fundraiser.Current_Donation_Amount
            };
        }

    }
}
