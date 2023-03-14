using Microsoft.EntityFrameworkCore;
using PetShelter.DataAccessLayer.Models;

namespace PetShelter.DataAccessLayer.Repository
{
    public class FundraiserRepository : BaseRepository<Fundraiser>
    {

        public FundraiserRepository(PetShelterContext context) : base(context)
        {
        }

        public async Task<decimal> GetTotalAmount(int fundraiserId)
        {
            return await _context.Donations
                  .Where(d => d.FundraiserId == fundraiserId)
                  .SumAsync(d => d.Amount);
        }
    }
}
