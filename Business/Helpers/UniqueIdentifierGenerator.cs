using Domain.Factories;

namespace Business.Helpers;
public class UniqueIdentifierGenerator : IUniqueIdentifierGenerator
{
    public string Generate()
    {
        return Guid.NewGuid().ToString();
    }
}
