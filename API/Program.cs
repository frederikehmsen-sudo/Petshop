using Infrastructure;
using LinqToDB;
using Microsoft.AspNetCore.Mvc;
using Service;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
builder.Services.AddScoped<IPetService,PetService>();

var connectionString = "Data Source=dev.db";
var options = new DataOptions().UseSQLite(connectionString);
var dataOptions = new DataOptions<PetshopDB>(options);

builder.Services.AddScoped<PetshopDB>(_ => new PetshopDB(dataOptions));

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    scope
        .ServiceProvider
        .GetRequiredService<PetshopDB>()
        .CreateTable<Pet>(tableOptions: TableOptions.CreateIfNotExists);
}
app.MapControllers();
app.Run();


public class MyPetshopController(PetService petService, PetshopDB db) : ControllerBase
{
    [HttpGet(nameof(GetPets))]
    public List<Pet> GetPets()
    {
        var pet = new Pet()
        {
            Id = "hamburger" + new Random().Next(),
            Name = "Hamburger"
        };
        db.Insert(pet);
        return petService.GetPets();
    }
}