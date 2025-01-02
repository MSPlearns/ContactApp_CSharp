using Domain.Models;
using Dtos;

namespace Business.Services
{
    public interface IContactService
    {
        bool Add(ContactCreationForm form);
        IEnumerable<Contact> GetAll();
    }
}