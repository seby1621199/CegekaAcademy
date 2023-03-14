using Microsoft.EntityFrameworkCore;
using PetShelter.DataAccessLayer.Models;

namespace PetShelter.DataAccessLayer.Repository;

public class PersonRepository : BaseRepository<Person>, IPersonRepository
{

    public PersonRepository(PetShelterContext context) : base(context)
    {
    }

    public async Task<Person?> GetPersonByIdNumber(string idNumber)
    {
        return await _context.Persons.SingleOrDefaultAsync(p => p.IdNumber == idNumber);
    }

    public async Task<ICollection<Person>?> GetPersonsByIds(IEnumerable<int> donorsids)
    {
        return await _context.Persons.Where(p => donorsids.Contains(p.Id)).ToListAsync();
    }
}