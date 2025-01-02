using Business.Helpers;
using Business.Services;
using DataManagement.Services;
using Domain.Factories;
using Presentation.ConsoleApp;

var IdGenerator = new UniqueIdentifierGenerator();
var contactFactory = new ContactFactory(IdGenerator);
var dataService = new DataService();
var contactService = new ContactService(contactFactory, dataService);
MenuService menu = new(contactService);
menu.Show();
