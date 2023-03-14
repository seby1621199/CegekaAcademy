using System.Collections.Immutable;
using PetShelter.DataAccessLayer.Models;
using PetShelter.DataAccessLayer.Repository;
using PetShelter.Domain.Extensions.DataAccess;
using PetShelter.Domain.Extensions.DomainModel;

namespace PetShelter.Domain.Services;

public class FundraiserService : IFundraiserService
{
    private readonly IPersonRepository _personRepository;
    private readonly IDonationRepository _donationRepository;
    private readonly IFundraiserRepository _fundraiserRepository;

    public FundraiserService(IPersonRepository personRepository, IDonationRepository donationRepository,
        IFundraiserRepository fundraiserRepository)
    {
        _personRepository = personRepository;
        _donationRepository = donationRepository;
        _fundraiserRepository = fundraiserRepository;
    }

    public async Task<int> CreateFundraiser(Fundraiser fundraiserInformation, Person owner)
    {
        var fundraiser = await FundraiserAddOwner(fundraiserInformation, owner);

        CheckEndDate(fundraiser);
        await _fundraiserRepository.Add(fundraiser);
        return fundraiser.Id;
    }

    private async Task<DataAccessLayer.Models.Fundraiser> FundraiserAddOwner(Fundraiser fundraiserInformation, Person OwnerInformation)
    {
        var owner = await _personRepository.GetOrAddPersonAsync(OwnerInformation.FromDomainModel());
        var fundraiser = fundraiserInformation.FromDomainModel();
        fundraiser.OwnerId = owner.Id;
        fundraiser.Owner = owner;

        return fundraiser;
    }

    private static void CheckEndDate(DataAccessLayer.Models.Fundraiser fundraiser)
    {
        if (DateTime.Now > fundraiser.EndDate)
            fundraiser.Status = "Ended";
    }

    public async Task<IReadOnlyCollection<FundraiserSummaryinformation>> GetFundraisersAsync()
    {
        var fundraisers = await _fundraiserRepository.GetAll();
        return fundraisers.Select(p => p.ToFundraiserInfoAll())
            .ToImmutableArray();
    }

    public async Task<FundraiserInformation> GetFundraiser(int id)
    {
        var fundraiserDatabase = await _fundraiserRepository.GetById(id);

        if (fundraiserDatabase == null)
            return null;

        fundraiserDatabase.Owner = await _personRepository.GetById(fundraiserDatabase.OwnerId);
        fundraiserDatabase.Donations = await _donationRepository.GetDonationsByFundraiserId(id);
        var fundraiserInformation = fundraiserDatabase.ToFundraiserInformation();

        await CheckDonorsInDatabase(fundraiserDatabase, fundraiserInformation);

        return fundraiserInformation;
    }

    private async Task CheckDonorsInDatabase(DataAccessLayer.Models.Fundraiser fundraiserDatabase,
        FundraiserInformation fundraiserInformation)
    {
        var donorsIds = fundraiserDatabase.Donations.Select(d => d.DonorId).Distinct();
        var donors = await _personRepository.GetPersonsByIds(donorsIds);
        fundraiserInformation.Donors = donors.Select(d => d.ToDomainModel()).ToImmutableArray();
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
        CheckTarget(fundraiser);

        await _donationRepository.Add(donation);
        await _fundraiserRepository.Update(fundraiser);
    }

    private static void CheckTarget(DataAccessLayer.Models.Fundraiser fundraiser)
    {
        if (fundraiser.Current_Donation_Amount >= fundraiser.Target)
            fundraiser.Status = "Ended";
    }

    public async Task CloseFundraiser(int id)
    {
        var fundraiser = await _fundraiserRepository.GetById(id);
        fundraiser.Status = "Ended";

        await _fundraiserRepository.Update(fundraiser);
    }
}