using Business.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Domain.Models;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.ObjectModel;

namespace Presentation.WinPF_App.ViewModels
{
    public partial class ContactsViewModel : ObservableObject
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly IContactService _contactService;

        [ObservableProperty]
        private ObservableCollection<Contact> _contacts = [];

        [ObservableProperty]
        private Contact _selectedContact = null!;

        [ObservableProperty]
        private string _headline = "CONTACT LIST";

        public ContactsViewModel(IServiceProvider serviceProvider, IContactService contactService)
        {
            _serviceProvider = serviceProvider;
            _contactService = contactService;
            _contacts = new ObservableCollection<Contact>(_contactService.GetAll());
        }

        [RelayCommand]
        private void GoToNewContact()
        {
            var mainViewModel = _serviceProvider.GetRequiredService<MainViewModel>();
            mainViewModel.CurrentViewModel = _serviceProvider.GetRequiredService<NewContactViewModel>();
        }
        [RelayCommand]
        private void DeleteContact(Contact contact)
        {
            try
            {
                SelectedContact = contact;
                if (_contactService.Delete(SelectedContact)) 
                    RefreshContacts();
            }
            catch (Exception)
            {
                throw;
            }
        }
        [RelayCommand]
        private void EditContact(Contact contact)
        {
            var editContactViewModel = _serviceProvider.GetRequiredService<EditContactViewModel>();
            editContactViewModel.SelectedContact = contact;
            var mainViewModel = _serviceProvider.GetRequiredService<MainViewModel>();
            mainViewModel.CurrentViewModel = editContactViewModel;
        }

        [RelayCommand]
        private void GoToContactDetail(Contact contact)
        {
            var contactDetailViewModel = _serviceProvider.GetRequiredService<ContactDetailViewModel>();
            contactDetailViewModel.SelectedContact = contact;

            var mainViewModel = _serviceProvider.GetRequiredService<MainViewModel>();
            mainViewModel.CurrentViewModel = contactDetailViewModel;
            

        }

        private void RefreshContacts()
        {
            Contacts = new ObservableCollection<Contact>(_contactService.GetAll());
        }

    }
}