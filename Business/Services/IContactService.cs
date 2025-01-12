using Domain.Models;
using Dtos;

namespace Business.Services
{
    public interface IContactService
    {
        bool Add(ContactCreationForm form);
        bool Delete(Contact contact);
        bool Update(Contact contact);
        bool DoesExist(Contact contact);
        bool IsEmpty();
        Contact? GetContactById(string id);
        IEnumerable<Contact> GetAll();
    }
}