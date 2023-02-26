using Microsoft.EntityFrameworkCore;
using PetShelter.DataAccessLayer.Models;

namespace PetShelter.DataAccessLayer.Repository;

public class DonationRepository : BaseRepository<Donation>, IDonationRepository
{
    public DonationRepository(PetShelterContext context): base(context)
    {
    }

    public async Task<List<Person>> GetPeopleDonors()
    {
        return await _context.Persons.Where(p => p.Donations.Count > 0).ToListAsync();
    }
}