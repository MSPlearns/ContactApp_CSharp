﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Business.Services;
using Dtos;
using Domain.Models;

namespace Presentation.ConsoleApp
{
    public class MenuService
    {
        public bool MenuRunning { get; set; } = true;
        private int _selectedOption = 0;
        private readonly List<string> _menuOptions = ["Add contact", "Show all contacts", "Exit"]; //If changed, ajust the HandleSelection method accordingly
        private readonly ContactService _contactService;

        public MenuService(ContactService contactService)
        {
            _contactService = contactService;
        }
        public void Show() //Displays the main menu and waits for user input
        {
            while (MenuRunning)
            {
                Console.Clear();
                TextDisplayService.Header("Main Menu");

                foreach (var option in _menuOptions)
                {
                    if (_menuOptions[_selectedOption] == option)
                    {
                        TextDisplayService.Selected(option);
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
                    TextDisplayService.AwaitKeyPress();
                    Environment.Exit(0);
                    break;
                default:
                    break;
            }
        }

        public void AddContactDialog() //Creates a new contact registration form and populate it with contact input

        {
            Console.Clear();
            TextDisplayService.Header("Add Contact");

            //TODO: VALIDATION SO THAT A METHOD CAN BE USED AND LOOP IF THE INPUT IS INVALID
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
                TextDisplayService.ConfirmationMessage("Contact added successfully!");
            }
            else
            {
                TextDisplayService.ErrorMessage("Unknown error adding contact!");
            }
        }

        public string PromptAndValidate(string prompt, string propertyName)
        {
            while (true) 
            {
                Console.WriteLine();
                Console.Write(prompt);
                var userInput= Console.ReadLine() ?? string.Empty;
                var results = new List<ValidationResult>();
                var context = new ValidationContext(new Contact()) {MemberName = propertyName};
                if (Validator.TryValidateProperty(userInput, context, results))
                {
                    return userInput;
                }
                else
                {
                    foreach (var result in results)
                    {
                        TextDisplayService.ErrorMessage(result.ErrorMessage);
                    }
                }
            }
        }


        public void ShowAllContactsDialog()  //Get all contacts 
        {
            Console.Clear();
            TextDisplayService.Header("Contacts");
           
            var contacts = _contactService.GetAll();
            //Display all contacts
            int counter = 1;
            foreach (var contact in contacts)
            {
                TextDisplayService.ContactList(counter++, contact);
            }
            TextDisplayService.AwaitKeyPress();
        }
    }
}
