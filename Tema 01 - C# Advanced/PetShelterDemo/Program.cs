//// See https://aka.ms/new-console-template for more information
//// Syntactic sugar: Starting with .Net 6, Program.cs only contains the code that is in the Main method.
//// This means we no longer need to write the following code, but the compiler still creates the Program class with the Main method:
//// namespace PetShelterDemo
//// {
////    internal class Program
////    {
////        static void Main(string[] args)
////        { actual code here }
////    }
//// }

using PetShelterDemo.DAL;
using PetShelterDemo.Domain;

var shelter = new PetShelter();

Console.WriteLine("Hello, Welcome the the Pet Shelter!");

var exit = false;
var exitfund = false;
try
{
    while (!exit)
    {
        PresentOptions(
            "Here's what you can do.. ",
            new Dictionary<string, Action>
            {
                { "Register a newly rescued pet", RegisterPet },
                { "Donate", Donate },
                { "See current donations total", SeeDonations },
                { "See our residents", SeePets },
                { "Access our fundraisers", AccessFundraisers },
                { "Break our database connection", BreakDatabaseConnection },
                { "Leave:(", Leave }
            }
        );
    }
}
catch (Exception e)
{
    Console.WriteLine($"Unfortunately we ran into an issue: {e.Message}.");
    Console.WriteLine("Please try again later.");
}


void RegisterPet()
{
    var name = ReadString("Name?");
    var description = ReadString("Description?");

    var pet = new Pet(name, description);

    shelter.RegisterPet(pet);
}

void Donate()
{
    Console.WriteLine("What's your name? (So we can credit you.)");
    var name = ReadString();

    Console.WriteLine("What's your personal Id? (No, I don't know what GDPR is. Why do you ask?)");
    var id = ReadString();
    var person = new Person(name, id);

    Console.WriteLine("How much would you like to donate? (RON)");
    var amountInRon = ReadInteger();
    shelter.Donate(person, amountInRon);
}

void SeeDonations()
{
    Console.WriteLine($"Our current donation total is {shelter.GetTotalDonationsInRON()}RON");
    Console.WriteLine("Special thanks to our donors:");
    var donors = shelter.GetAllDonors();
    foreach (var donor in donors)
    {
        Console.WriteLine(donor.Name);
    }
}

void SeePets()
{

    var pets = shelter.GetAllPets();

    var petOptions = new Dictionary<string, Action>();
    foreach (var pet in pets)
    {
        petOptions.Add(pet.Name, () => SeePetDetailsByName(pet.Name));
    }

    PresentOptions("We got..", petOptions);
}

void SeePetDetailsByName(string name)
{
    var pet = shelter.GetByName(name);
    Console.WriteLine($"A few words about {pet.Name}: {pet.Description}");
}

void BreakDatabaseConnection()
{
    Database.ConnectionIsDown = true;
}

void Leave()
{
    Console.WriteLine("Good bye!");
    exit = true;
}


void AccessFundraisers()
{
    while (!exitfund)
    {
        PresentOptions(
           "Here's what you can do.. ",
           new Dictionary<string, Action>
           {
                { "Create fundraiser", CreateFundraiser },
                { "Show all", SeeFundraisers },
                { "Donate to fundraiser",DonateFundraisers },
                { "Leave:(", LeaveFundraiser }
           }
       );
    }
}

void SeeFundraisers()
{
    var fundraisers = shelter.GetAllFundraisers();
    Console.WriteLine("Fundraisers:");
    foreach (var fundraiser in fundraisers)
    {
        Console.WriteLine($"Title: {fundraiser.Name}\nDescription:{fundraiser.Description}\nTarget:{fundraiser.TotalAmount}/{fundraiser.Target} RON\n");
    }
}

void LeaveFundraiser()
{
    exitfund = true;
}

void CreateFundraiser()
{
    var name = ReadString("Name?");
    var description = ReadString("Description?");
    Console.WriteLine("How much would you like to donate? (RON)");
    var target = ReadInteger();
    var fundraiser = new Fundraiser(name, description, target);
    shelter.CreateFundraiser(fundraiser);
}
void DonateFundraisers()
{
    var fundraisers = shelter.GetAllFundraisers();
    Console.WriteLine("Fundraisers:");
    foreach(var fundraiser in fundraisers)
    {
        Console.WriteLine($"Title: {fundraiser.Name}\nDescription:{fundraiser.Description}\nTarget:{fundraiser.TotalAmount}/{fundraiser.Target} RON\n");
    }

    var nameFundraiser = ReadString("Please select the fundraiser");
    var selectedFundraiser = shelter.GetFundraiserByName(nameFundraiser);

    Console.WriteLine("What's your name? (So we can credit you.)");
    var nameDonor = ReadString();
    Console.WriteLine("What's your personal Id? (No, I don't know what GDPR is. Why do you ask?)");
    var id = ReadString();
    var person = new Person(nameDonor, id);
    Console.WriteLine("How much would you like to donate? (RON)");
    var amountInRon = ReadInteger();

    shelter.DonateToFundraiser(person, selectedFundraiser, amountInRon);
}

void PresentOptions(string header, IDictionary<string, Action> options)
{

    Console.WriteLine(header);

    for (var index = 0; index < options.Count; index++)
    {
        Console.WriteLine(index + 1 + ". " + options.ElementAt(index).Key);
    }

    var userInput = ReadInteger(options.Count);

    options.ElementAt(userInput - 1).Value();
}

string ReadString(string? header = null)
{
    if (header != null) Console.WriteLine(header);

    var value = Console.ReadLine();
    Console.WriteLine("");
    return value;
}

int ReadInteger(int maxValue = int.MaxValue, string? header = null)
{
    if (header != null) Console.WriteLine(header);

    var isUserInputValid = int.TryParse(Console.ReadLine(), out var userInput);
    if (!isUserInputValid || userInput > maxValue)
    {
        Console.WriteLine("Invalid input");
        Console.WriteLine("");
        return ReadInteger(maxValue, header);
    }

    Console.WriteLine("");
    return userInput;
}