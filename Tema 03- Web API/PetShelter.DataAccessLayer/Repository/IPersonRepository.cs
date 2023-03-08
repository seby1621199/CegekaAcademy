using PetShelter.DataAccessLayer.Models;

namespace PetShelter.DataAccessLayer.Repository;

public interface IPersonRepository : IBaseRepository<Person>
{
    Task<Person?> GetPersonByIdNumber(string idNumber);
    public Task<ICollection<Person>?> GetPersonsByIds(IEnumerable<int> donorsids);
}