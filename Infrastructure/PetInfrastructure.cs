using LinqToDB;
using LinqToDB.Data;
using LinqToDB.Mapping;

namespace Infrastructure;

public class PetshopDB(DataOptions<PetshopDB> dataOptions) : DataConnection(dataOptions.Options)
{
    public ITable<Pet> Pets()
    {
        return this.GetTable<Pet>();
    }
}

public class Pet
{
    [PrimaryKey] public string Id { get; set; }
    public string Name { get; set; }
}