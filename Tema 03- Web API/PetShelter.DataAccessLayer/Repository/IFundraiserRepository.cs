using PetShelter.DataAccessLayer.Models;

namespace PetShelter.DataAccessLayer.Repository
{
    public interface IFundraiserRepository : IBaseRepository<Fundraiser>
    {
        public Task<Fundraiser?> GetFundraiserById(int id);
    }
}
