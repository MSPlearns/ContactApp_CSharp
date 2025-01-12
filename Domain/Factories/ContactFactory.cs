using Domain.Models;
using Dtos;
using System.ComponentModel.DataAnnotations;

namespace Domain.Factories;
public class ContactFactory : IContactFactory
{
    private readonly IUniqueIdentifierGenerator _uniqueIdentifierGenerator;
    public ContactFactory(IUniqueIdentifierGenerator UIG)
    {
        _uniqueIdentifierGenerator = UIG;
    }
    public Contact Create(ContactCreationForm form)
    {
        string id = _uniqueIdentifierGenerator.Generate();
        
        if (!ValidateId(id) || !ValidateMandatoryFields(form))
        {
            return null!;//Returning null triggers an error message in the application layer.
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
            return null!; //Returning null triggers an error message in the application layer.
        }
    }
    #region validation
    //input validation done at the application level. ID (generated) validation here - before creating a new contact.
    //Specific validation for email/phone (one of them must be filled) here, which is not ideal. 
    public static bool ValidateMandatoryFields(ContactCreationForm form) //Manual validation of mandatory fields.                                                              
    { 
        if (string.IsNullOrEmpty(form.Email) && string.IsNullOrEmpty(form.PhoneNumber)) //Either email or phone number must be filled
        {
            return false; 
        }
        return true;       
    }

    public static bool ValidateId(string id)
    {
        var context = new ValidationContext(new Contact()) { MemberName = "Id" };
        var results = new List<ValidationResult>();
        return Validator.TryValidateProperty(id, context, results);
    }
    #endregion
}
