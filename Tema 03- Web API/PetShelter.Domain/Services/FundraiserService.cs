
using PetShelter.DataAccessLayer.Models;
using PetShelter.DataAccessLayer.Repository;
using PetShelter.Domain.Extensions.DataAccess;
using PetShelter.Domain.Extensions.DomainModel;
using System.Collections.Immutable;

namespace PetShelter.Domain.Services
{
    public class FundraiserService : IFundraiserService
    {
        private readonly IPersonRepository _personRepository;
        private readonly IDonationRepository _donationRepository;
        private readonly IFundraiserRepository _fundraiserRepository;

        public FundraiserService(IPersonRepository personRepository, IDonationRepository donationRepository, IFundraiserRepository fundraiserRepository)
        {
            _personRepository = personRepository;
            _donationRepository = donationRepository;
            _fundraiserRepository = fundraiserRepository;
        }

        public async Task<int> CreateFundraiser(Fundraiser fundraiser, Person Owner)
        {
            var person = await _personRepository.GetOrAddPersonAsync(Owner.FromDomainModel());
            var fundraisernew = new DataAccessLayer.Models.Fundraiser
            {
                Name = fundraiser.Name,
                Description = fundraiser.Description,
                Owner = person,
                OwnerId = person.Id,
                EndDate = fundraiser.EndDate,
                CreationDate = DateTime.Now,
                Current_Donation_Amount = 0,
                Donations = new List<Donation>(),
                Status = "Active",
                Target = fundraiser.Target
            };
            if (DateTime.Now > fundraiser.EndDate)
                fundraisernew.Status = "Ended";
            await _fundraiserRepository.Add(fundraisernew);
            return fundraisernew.Id;
        }

        public async Task<IReadOnlyCollection<FundraiserSummaryInformation>> GetFundraisersAsync()
        {
            var fundraisers = await _fundraiserRepository.GetAll();
            return fundraisers.Select(p => p.ToFundraiserInfoAll())
                .ToImmutableArray();
        }

        public async Task<FundraiserInformation> GetFundraiser(int id)
        {
            var fundraiser = await _fundraiserRepository.GetById(id);
            if (fundraiser == null)
                return null;
            fundraiser.Owner = await _personRepository.GetById(fundraiser.OwnerId);
            fundraiser.Donations = await _donationRepository.GetDonationsByFundraiserId(id);
            var data = fundraiser.ToFundraiserInformation();
            var donorsids = fundraiser.Donations.Select(d => d.DonorId).Distinct();
            var donors = await _personRepository.GetPersonsByIds(donorsids);
            data.Donors = donors.Select(d => d.ToDomainModel()).ToImmutableArray();
            return data;
        }

        public async Task DonateToFundraiser(int id, Person donor, int amount)
        {
            var fundraiser = await _fundraiserRepository.GetById(id);
            var person = await _personRepository.GetOrAddPersonAsync(donor.FromDomainModel());
            var donation = new Donation
            {
                Amount = amount,
                DonorId = person.Id,
                FundraiserId = fundraiser.Id
            };

            fundraiser.Current_Donation_Amount += amount;

            if (fundraiser.Current_Donation_Amount >= fundraiser.Target)
            {
                fundraiser.Status = "Ended";
            }
            await _donationRepository.Add(donation);
            await _fundraiserRepository.Update(fundraiser);
        }

        public async Task CloseFundraiser(int id)
        {
            var fundraiser = await _fundraiserRepository.GetById(id);
            fundraiser.Status = "Ended";
            await _fundraiserRepository.Update(fundraiser);
        }
    }
}
