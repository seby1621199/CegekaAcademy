using PetShelter.DataAccessLayer.Models;

namespace PetShelter.DataAccessLayer.Repository;

public interface IDonationRepository : IBaseRepository<Donation>
{
    Task<ICollection<Donation>?> GetDonationsByFundraiserId(int id);
}