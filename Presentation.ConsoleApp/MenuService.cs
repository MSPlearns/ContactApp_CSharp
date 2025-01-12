using System.ComponentModel.DataAnnotations;
using Business.Services;
using Dtos;
using Domain.Models;

namespace Presentation.ConsoleApp;
public class MenuService : IMenuService
{
    public bool MenuRunning { get; set; } = true;
    private int _selectedOption = 0;
    private readonly List<string> _menuOptions = ["Add contact", "Show all contacts", "Exit"]; //If changed, ajust the HandleSelection method accordingly
    private readonly IContactService _contactService;
    private readonly ITextDisplayService _textDisplayService;

    public MenuService(IContactService contactService, ITextDisplayService textDisplayService)
    {
        _contactService = contactService;
        _textDisplayService = textDisplayService;
    }
    #region Menu Options Methods
    public void Show() //Displays the main menu and waits for user input
    {
        while (MenuRunning)
        {
            Console.Clear();
            _textDisplayService.Header("Main Menu");

            foreach (var option in _menuOptions)
            {
                if (_menuOptions[_selectedOption] == option)
                {
                    _textDisplayService.Selected(option);
                }
                else
                {
                    Console.WriteLine(option);
                }
            }
            HandleKeyPress(Console.ReadKey());
        }
    }

    private void HandleKeyPress(ConsoleKeyInfo pressedKey) //Handles the user input
    {
        // Add or reduce the selection variable if up or down arrows are pressed
        if (pressedKey.Key == ConsoleKey.DownArrow && _selectedOption != _menuOptions.Count - 1)
        {
            _selectedOption++;
        }
        else if (pressedKey.Key == ConsoleKey.UpArrow && _selectedOption >= 1)
        {
            _selectedOption--;
        }
        // Determine actions when contact press ENTER based the value of menuSelect
        else if (pressedKey.Key == ConsoleKey.Enter)
        {
            //Reset the boolean variable that determines menus is running
            MenuRunning = false;
            HandleSelection(_selectedOption);
        }
    }

    private void HandleSelection(int option) //Handles the user selection, should match _menuOptions
    {
        switch (option)
        {
            case 0:
                //Add contact
                AddContactDialog();
                MenuRunning = true;
                break;
            case 1:
                //Show all contacts
                ShowAllContactsDialog();
                MenuRunning = true;
                break;
            case 2:
                //Exit
                Console.WriteLine("Exiting...");
                _textDisplayService.AwaitKeyPress();
                Environment.Exit(0);
                break;
            default:
                break;
        }
    }
    #endregion

    #region Add Contact Dialog & Validation
    public void AddContactDialog() //Creates a new contact registration form and populate it with contact input

    {
        Console.Clear();
        _textDisplayService.Header("Add Contact");

        ContactCreationForm form = new();
        form.FirstName = PromptAndValidate("First name:", nameof(form.FirstName));
        form.LastName = PromptAndValidate("Last name:", nameof(form.LastName));
        form.Email = PromptAndValidate("Email:", nameof(form.Email));
        form.PhoneNumber = PromptAndValidate("Phone number:", nameof(form.PhoneNumber));
        form.Address = PromptAndValidate("Address:", nameof(form.Address));
        form.Postcode = PromptAndValidate("Postcode:", nameof(form.Postcode));
        form.City = PromptAndValidate("City:", nameof(form.City));

        //Call the domain service to add the contact
        if (_contactService.Add(form))
        {
            _textDisplayService.ConfirmationMessage("Contact added successfully!");
            _textDisplayService.AwaitKeyPress();
        }
        else
        {
            _textDisplayService.ErrorMessage("Error adding contact! Make sure you are providing a full name and either an email or phone number");
            _textDisplayService.AwaitKeyPress();
        }
    }
    public string PromptAndValidate(string prompt, string propertyName)
    {
        while (true)
        {
            Console.WriteLine();
            Console.Write(prompt);
            var userInput = Console.ReadLine() ?? string.Empty;
            var results = new List<ValidationResult>();
            var context = new ValidationContext(new Contact()) { MemberName = propertyName };
            if (Validator.TryValidateProperty(userInput, context, results))
            {
                return userInput;
            }
            else
            {
                foreach (var result in results)
                {
                    _textDisplayService.ErrorMessage(result.ErrorMessage!);

                }
                _textDisplayService.AwaitKeyPress();
                _textDisplayService.ClearConsoleLines(results.Count() + 3);
            }
        }
    }

    #endregion
    public void ShowAllContactsDialog()  //Get all contacts 
    {
        Console.Clear();
        _textDisplayService.Header("Contacts");

        var contacts = _contactService.GetAll();
        //Display all contacts
        int counter = 1;
        foreach (var contact in contacts)
        {
            _textDisplayService.ContactList(counter++, contact);
        }
        _textDisplayService.AwaitKeyPress();
    }
}