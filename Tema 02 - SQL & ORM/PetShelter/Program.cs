
using PetShelter.DataAccessLayer;
using PetShelter.DataAccessLayer.Models;
using PetShelter.DataAccessLayer.Repository;


var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();
PetShelterContext context = new PetShelterContext();
DonationRepository DonationRepository = new DonationRepository(context);
FundraiserRepository FundraiserRepository = new FundraiserRepository(context);
app.MapGet("/", () =>
{
    //return DonationRepository.GetPeopleDonors();
    return FundraiserRepository.GetTotalAmount(1); 
});





app.Run();
