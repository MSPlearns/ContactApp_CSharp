using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Domain.Models;
using Microsoft.Extensions.DependencyInjection;

namespace Presentation.WinPF_App.ViewModels
{
    public partial class EditContactViewModel : ObservableObject
    {
        private readonly IServiceProvider _serviceProvider;

        [ObservableProperty]
        private Contact _selectedContact = new();

        [ObservableProperty]
        private string _title = "Edit";

        public EditContactViewModel(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        [RelayCommand]
        private void SaveContact()
        {
            var mainViewModel = _serviceProvider.GetRequiredService<MainViewModel>();
            mainViewModel.CurrentViewModel = _serviceProvider.GetRequiredService<ContactsViewModel>();
        }

        [RelayCommand]
        private void GoToContactDetail(Contact contact)
        {
            var contactDetailViewModel = _serviceProvider.GetRequiredService<ContactDetailViewModel>();
            contactDetailViewModel.SelectedContact = contact;

            var mainViewModel = _serviceProvider.GetRequiredService<MainViewModel>();
            mainViewModel.CurrentViewModel = contactDetailViewModel;


        }

    }
}
