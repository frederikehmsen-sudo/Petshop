using Infrastructure;
using LinqToDB;
using Microsoft.AspNetCore.Mvc;
using Service;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
builder.Services.AddScoped<IPetService,PetService>();
builder.Services.AddSingleton<PetshopDB>();

var connectionString = "Data Source=dev.db";
var options = new DataOptions().UseSQLite(connectionString);
var dataOptions = new DataOptions<PetshopDB>(options);

builder.Services.AddScoped<PetshopDB>(_ => new PetshopDB(dataOptions));

var app = builder.Build();
app.MapControllers();
app.Run();


public class MyPetshopController(PetService petService) : ControllerBase
{
    [HttpGet(nameof(GetPets))]
    public List<Pet> GetPets()
    {
        return petService.GetPets();
    }
}