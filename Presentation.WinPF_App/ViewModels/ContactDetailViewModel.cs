using Business.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Domain.Models;
using Microsoft.Extensions.DependencyInjection;

namespace Presentation.WinPF_App.ViewModels
{
    public partial class ContactDetailViewModel : ObservableObject
    {
        private readonly IServiceProvider _serviceProvider;
        [ObservableProperty]
        private Contact _selectedContact = new();

        [ObservableProperty]
        private string _title = "detail";

        public ContactDetailViewModel(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        [RelayCommand]
        private void GoToContacts()
        {
            var mainViewModel = _serviceProvider.GetRequiredService<MainViewModel>();
            mainViewModel.CurrentViewModel = _serviceProvider.GetRequiredService<ContactsViewModel>();
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
        private void DeleteContact()
        {
            try
            {
                var contactService = _serviceProvider.GetRequiredService<IContactService>();
                if (contactService.Delete(SelectedContact))
                {
                    var mainViewModel = _serviceProvider.GetRequiredService<MainViewModel>();
                    mainViewModel.CurrentViewModel = _serviceProvider.GetRequiredService<ContactsViewModel>();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
