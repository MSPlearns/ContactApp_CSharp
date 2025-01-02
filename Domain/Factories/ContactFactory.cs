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
        public static Contact Create(ContactCreationForm form)
        {
            if (!ValidateFormField(form))
            {
                return null!;
            }
            try
            {
                //Problem with circular dependency - UniqueIdentifierGenerator.Generate() is not accessible
                //Generating GUID here instead as a quick fix
                //string uniqueId = UniqueIdentifierGenerator.Generate();
                string uniqueId = Guid.NewGuid().ToString();
                return new Contact {
                    Id = uniqueId,
                    FirstName = form.FirstName,
                    LastName = form.LastName,
                    Email = form.Email!,
                    PhoneNumber = form.PhoneNumber!,
                    Address = form.Address!,
                    Postcode = form.Postcode!,
                    City = form.City!};
            }
            catch (Exception)
            {
                return null!;
            }
        }

        private static bool ValidateFormField(ContactCreationForm form)
        {
            if (string.IsNullOrEmpty(form.FirstName) || string.IsNullOrEmpty(form.LastName))
            {
                return false;
            }
            if (string.IsNullOrEmpty(form.Email) && string.IsNullOrEmpty(form.PhoneNumber)) 
            {
                return false; 
            }

            return true;
            
        }
    }
}
