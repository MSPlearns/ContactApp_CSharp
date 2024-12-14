using DataManagement.Services;
using Domain.Factories;
using Domain.Models;
using Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Services
{
    public class ContactService
    {
        private List<Contact> _contacts = [];
        private readonly DataService _fileService = new(); //Dependency Injection would allow for a single instance of the DataService to be used for the whole application
        public bool Add(ContactCreationForm form) //Takes in a DTO,
                                                  //sends it to the factory to create a new Contact object,
                                                  //then adds it to the list/file.
                                                  //Lastly, it returns a bool to confirm the contact creation.
        {
            Contact contact = ContactFactory.CreateUser(form);
            // Add user
            if (contact != null)
            {

                //TO DO: ADD A CHECK TO SEE IF THE USER ALREADY EXISTS
                _contacts.Add(contact);
                _fileService.SaveListToFile<Contact>(_contacts);
                return true;
            }
            return false;
        }

        public IEnumerable<Contact> GetAll() //Returns all contacts in the file as IEnumerable so that the list can't be modified
        {
            _contacts = _fileService.LoadListToFile<Contact>();
            return _contacts;
        }
    }
}
