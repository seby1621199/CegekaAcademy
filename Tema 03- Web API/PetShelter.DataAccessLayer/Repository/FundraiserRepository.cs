using Microsoft.EntityFrameworkCore;
using PetShelter.DataAccessLayer.Models;

namespace PetShelter.DataAccessLayer.Repository
{
    public class FundraiserRepository : BaseRepository<Fundraiser>, IFundraiserRepository
    {

        public FundraiserRepository(PetShelterContext context) : base(context)
        {
        }


        public async Task<Fundraiser?> GetFundraiserById(int id)
        {
            return await _context.Fundraisers.SingleOrDefaultAsync(p => p.Id == id);
        }


    }
}
