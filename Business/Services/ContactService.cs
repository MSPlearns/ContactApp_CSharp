using DataManagement.Services;
using Domain.Factories;
using Domain.Models;
using Dtos;

namespace Business.Services;
public class ContactService : IContactService
{
    private List<Contact> _contacts = [];
    private readonly IDataService _fileService;
    private readonly IContactFactory _contactFactory;

    public ContactService(IContactFactory contactFactory, IDataService dataService)
    {
        _contactFactory = contactFactory;
        _fileService = dataService;
        _contacts = _fileService.LoadListFromFile<Contact>();
    }

    public bool Add(ContactCreationForm form)
    {
        Contact contact = _contactFactory.Create(form);
        try
        {
            if (contact != null)
            {
                //TODO: Maybe check if contact already exists (same email, same name etc)
                _contacts.Add(contact);
                _contacts.Sort((x, y) => x.FirstName.CompareTo(y.FirstName));
                _fileService.SaveListToFile<Contact>(_contacts);
                return true;
            }
            return false;
        }
        catch
        {
            return false;
        }
    }

    public bool Delete(Contact contact)
    {
        if (DoesExist(contact))
        {
            try
            {
                _contacts.RemoveAll(x => x.Id == contact.Id);
                _contacts.Sort((x, y) => x.FirstName.CompareTo(y.FirstName));
                _fileService.SaveListToFile<Contact>(_contacts);
                return true;
            }
            catch
            {
                return false; //TODO: Maybe add a log here
            }
        }
        return false;
    }

    public bool Update(Contact editedContact)
    {
        //TODO: To avoid having two contacts with the same ID (the contact in the file/list and the edited contact) 
        //in memory, a new dto (updateform) can be used to send edit input to...a method in contactModel(?)
        //that updates the contact fields directly. This would require aditional testing, maybe aditional validation.

        if (DoesExist(editedContact))
        {
            try
            {
                var ogContact = GetContactById(editedContact.Id)!;
                int index = _contacts.IndexOf(ogContact);
                _contacts[index] = editedContact;
                _contacts.Sort((x, y) => x.FirstName.CompareTo(y.FirstName));
                _fileService.SaveListToFile<Contact>(_contacts);
                return true;
            }
            catch
            {
                return false; //TODO: Maybe add a log here
            }
        }
        return false;
    }

    public IEnumerable<Contact> GetAll() //Returns all contacts in the file as IEnumerable so that the list can't be modified
    {
        _contacts = _fileService.LoadListFromFile<Contact>();
        return _contacts;
    }

    public bool DoesExist(Contact contact)
    {
        return _contacts.Any(x => x.Id == contact.Id);

    }
    public Contact? GetContactById(string id)
    {
        return _contacts.FirstOrDefault(x => x.Id == id);
    }

    public bool IsEmpty()
    {
        GetAll();
        return _contacts.Count == 0;
    }
}

