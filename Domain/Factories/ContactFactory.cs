using Domain.Models;
using Dtos;


namespace Domain.Factories
{
    public class ContactFactory : IContactFactory
    {
        private IUniqueIdentifierGenerator UniqueIdentifierGenerator { get; }

        public ContactFactory(IUniqueIdentifierGenerator UIG)
        {
            UniqueIdentifierGenerator = UIG;
        }

        public Contact Create(ContactCreationForm form)
        {
            if (!ValidateFormField(form))
            {
                return null!;
            }
            try
            {
                //Interface for generating unique identifiers implemented to solve circular dependency - now I have to inject dependency everywhere so shit works
                string uniqueId = UniqueIdentifierGenerator.Generate();
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
