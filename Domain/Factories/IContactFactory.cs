using Domain.Models;
using Dtos;

namespace Domain.Factories;
public interface IContactFactory
{
    Contact Create(ContactCreationForm form);
}
