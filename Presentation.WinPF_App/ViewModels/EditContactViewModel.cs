using Business.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Domain.Models;
using Dtos;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;

namespace Presentation.WinPF_App.ViewModels
{
    public partial class EditContactViewModel : ObservableObject
    {
        private readonly IServiceProvider _serviceProvider;

        [ObservableProperty]
        private Contact _selectedContact = new();


        [ObservableProperty]
        private string _errorMessage;

        public EditContactViewModel(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
            _errorMessage = "";

        }

        [RelayCommand]
        private void SaveContact()
        {
            if (ValidateForm(SelectedContact))
            {
                try
                {
                    var contactService = _serviceProvider.GetRequiredService<IContactService>();
                    if (contactService.Update(SelectedContact))
                    {
                        GoToContactDetail();
                    }
                }
                catch (Exception ex)
                {
                    System.Windows.MessageBox.Show(
                    $"Something went wrong:\n{ex.Message}",
                    "Unknown error",
                    System.Windows.MessageBoxButton.OK,
                    System.Windows.MessageBoxImage.Warning
                    );
                }
            }
        }

        [RelayCommand]
        private void GoToContactDetail()
        {
            var contactDetailViewModel = _serviceProvider.GetRequiredService<ContactDetailViewModel>();
            contactDetailViewModel.SelectedContact = SelectedContact;

            var mainViewModel = _serviceProvider.GetRequiredService<MainViewModel>();
            mainViewModel.CurrentViewModel = contactDetailViewModel;
        }

        private bool ValidateForm(Contact editedContact)
        {
            bool allPropertiesValid = true;
            var context = new ValidationContext(new Contact());
            var validationResults = new List<ValidationResult>();
            var validationErrors = new List<string>();

            foreach (var property in typeof(Contact).GetProperties())
            {
                context.MemberName = property.Name;
                if (!Validator.TryValidateProperty(property.GetValue(editedContact), context, validationResults))
                {
                    allPropertiesValid = false;
                }
            }

            if (!allPropertiesValid)
            {
                foreach (ValidationResult result in validationResults)
                {
                    validationErrors.Add(result!.ErrorMessage!);
                }
            }

            if (string.IsNullOrEmpty(editedContact.Email) && string.IsNullOrEmpty(editedContact.PhoneNumber))
            {
                validationErrors.Add("*Either email or phone number must be provided");
                allPropertiesValid = false;
            }
            ErrorMessage = string.Join(Environment.NewLine, validationErrors);
            return allPropertiesValid;
        }

    }
}
