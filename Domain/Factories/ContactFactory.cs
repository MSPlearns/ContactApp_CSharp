using Domain.Models;
using Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Factories
{
    public class ContactFactory
    {
        public static Contact CreateUser(ContactCreationForm form)
        {
            // TODO: ADD TRY CATCH, IF FAILED, RETURN NULL
            try
            {
                //Problem with circular dependency - UniqueIdentifierGenerator.Generate() is not accessible
                //Generating GUID here instead as a quick fix
                //string uniqueId = UniqueIdentifierGenerator.Generate();
                string uniqueId = Guid.NewGuid().ToString();
                return new Contact(uniqueId, form.FirstName, form.LastName, form.Email, form.PhoneNumber, form.Address, form.Postcode, form.City);
            }
            catch (Exception)
            {
                return null!;
            }
        }
    }
}
