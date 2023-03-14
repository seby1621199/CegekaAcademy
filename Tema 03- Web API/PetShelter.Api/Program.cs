using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Converters;
using PetShelter.Api.SwaggerHelper;
using PetShelter.DataAccessLayer;
using PetShelter.DataAccessLayer.Repository;
using PetShelter.Domain.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddDbContext<PetShelterContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("PetShelterConnection"),
        providerOptions =>
        {
            providerOptions.MigrationsAssembly("PetShelter.DataAccessLayer");
            providerOptions.EnableRetryOnFailure();
        }));
builder.Services.AddScoped<IPetService, PetService>();
builder.Services.AddScoped<IPetRepository, PetRepository>();
builder.Services.AddScoped<IPersonRepository, PersonRepository>();
builder.Services.AddScoped<IFundraiserService, FundraiserService>();
builder.Services.AddScoped<IFundraiserRepository, FundraiserRepository>();
builder.Services.AddScoped<IDonationRepository, DonationRepository>();

builder.Services.AddValidatorsFromAssemblyContaining<Program>();
builder.Services.AddFluentValidationAutoValidation(fv =>
{
    fv.DisableDataAnnotationsValidation = true;
}).AddFluentValidationClientsideAdapters();

builder.Services.AddControllers().AddNewtonsoftJson(options =>
{
    options.SerializerSettings.Converters.Add(new StringEnumConverter());
});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    var schemaHelper = new SwashbuckleSchemaHelper();
    options.CustomSchemaIds(type => schemaHelper.GetSchemaId(type));
});

var app = builder.Build();
app.UseCors(builder => builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());



// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
