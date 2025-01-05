using Domain.Models;
using Dtos;
using System.ComponentModel.DataAnnotations;


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
            string id = UniqueIdentifierGenerator.Generate();
            var context = new ValidationContext(new Contact()) { MemberName = "Id" };
            var results = new List<ValidationResult>();
            if (!Validator.TryValidateProperty(id, context, results))
            {
                return null!;
            }
            try
            {
                return new Contact {
                    Id = id,
                    FirstName = form.FirstName!,
                    LastName = form.LastName!,
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
