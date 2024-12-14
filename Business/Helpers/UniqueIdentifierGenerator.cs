using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Helpers
{
    public static class UniqueIdentifierGenerator
    {
        //Currently unable to call this from the ContactFactory due to circular dependency. Need to fix this.
        public static string Generate()
        {
          return Guid.NewGuid().ToString();
        }
    }
}
