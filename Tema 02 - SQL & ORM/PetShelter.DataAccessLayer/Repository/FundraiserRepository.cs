using Microsoft.EntityFrameworkCore;
using PetShelter.DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetShelter.DataAccessLayer.Repository
{
    public class FundraiserRepository : BaseRepository<Fundraiser>
    {

        public FundraiserRepository(PetShelterContext context) : base(context)
        {
        }

        public async Task<decimal> GetTotalAmount(int fundraiserId)
        {
            var fundraiser = await _context.Fundraiser.Include(f => f.Donations).FirstOrDefaultAsync(f => f.Id == fundraiserId);
            return fundraiser.Donations.Sum(d => d.Amount);
        }
    }
}
