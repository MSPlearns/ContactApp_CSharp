using DataManagement.Services;
using Domain.Factories;
using Domain.Models;
using Dtos;


namespace Business.Services
{
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

        public bool Add(ContactCreationForm form) //Takes in a DTO,
                                                  //sends it to the factory to create a new Contact object,
                                                  //then adds it to the list/file.
                                                  //Lastly, it returns a bool to confirm the contact creation.
        {
            Contact contact = _contactFactory.Create(form);
            // Add user
            try
            {
                if (contact != null)
                {
                    //TODO: Maybe check if contact already exists (same email, same name etc)
                    _contacts.Add(contact);
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
                    _fileService.SaveListToFile<Contact>(_contacts);
                    return true;
                }
                catch
                {
                    return false;
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
                    _fileService.SaveListToFile<Contact>(_contacts);
                    return true;
                }
                catch
                {
                    return false;
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
            _contacts = _fileService.LoadListFromFile<Contact>();
            return _contacts.Any(x => x.Id == contact.Id);

        }

        public Contact? GetContactById(string id)
        {
            _contacts = _fileService.LoadListFromFile<Contact>();
            return _contacts.FirstOrDefault(x => x.Id == id);
        }
    }
}

