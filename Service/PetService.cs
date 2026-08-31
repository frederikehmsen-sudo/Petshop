using Infrastructure;

namespace Service;

public class PetService : IPetService
{
    private readonly PetshopDB db;

    public PetService(PetshopDB db)
    {
        Console.WriteLine("Service has been instantiated");
        this.db = db;
    }
    public List<Pet> GetPets()
    {
        return db.Pets().ToList();
    }
}

public interface IPetService
{
    List<Pet> GetPets();
}