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
        private ObservableCollection<string> _validationErrors;

        public EditContactViewModel(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
            _validationErrors = [];
            
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
                        var mainViewModel = _serviceProvider.GetRequiredService<MainViewModel>();
                        mainViewModel.CurrentViewModel = _serviceProvider.GetRequiredService<ContactsViewModel>();
                    }
                }
                catch (Exception)
                {
                    throw;
                }
            }
            else
            {
                string errorMessage = string.Join(Environment.NewLine, ValidationErrors);
                System.Windows.MessageBox.Show(
                    $"Failed to save the contact:\n{errorMessage}",
                    "Validation Errors",
                    System.Windows.MessageBoxButton.OK,
                    System.Windows.MessageBoxImage.Warning
                );
            }

        }

        [RelayCommand]
        private void GoToContactDetail(Contact contact)
        {
            var contactDetailViewModel = _serviceProvider.GetRequiredService<ContactDetailViewModel>();
            contactDetailViewModel.SelectedContact = contact;

            var mainViewModel = _serviceProvider.GetRequiredService<MainViewModel>();
            mainViewModel.CurrentViewModel = contactDetailViewModel;
        }

        private bool ValidateForm(Contact editedContact)
        {
            ValidationErrors.Clear();
            bool sucessful = true;
            var context = new ValidationContext(new Contact());
            var validationResults = new List<ValidationResult>();

            foreach (var property in typeof(Contact).GetProperties())
            {
                context.MemberName = property.Name;
                sucessful = !Validator.TryValidateProperty(property.GetValue(editedContact), context, validationResults) ? false : sucessful;

            }

            if (!sucessful)
            {
                foreach (ValidationResult result in validationResults)
                {
                    ValidationErrors.Add(result!.ErrorMessage);
                }
            }

            if (string.IsNullOrEmpty(editedContact.Email) && string.IsNullOrEmpty(editedContact.PhoneNumber))
            {
                ValidationErrors.Add("*Either email or phone number must be provided");
                sucessful = false;
            }
            return sucessful;
        }

    }
}
