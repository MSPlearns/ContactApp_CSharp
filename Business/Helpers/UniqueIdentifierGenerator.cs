using Domain.Factories;

namespace Business.Helpers
{
    public class UniqueIdentifierGenerator : IUniqueIdentifierGenerator
    {
        //Currently unable to call this from the ContactFactory due to circular dependency. Need to fix this.
        public string Generate()
        {
          return Guid.NewGuid().ToString();
            //To unitTest: Create a stringified GUID with this method, and try to parse it back to a GUID. Assert that is a guid.
        }
    }
}
