using Business.Services;
using Dtos;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.DependencyInjection;
using Domain.Models;
using System.ComponentModel.DataAnnotations;
using System.Collections.ObjectModel;

namespace Presentation.WinPF_App.ViewModels
{
    public partial class NewContactViewModel : ObservableObject
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly IContactService _contactService;
        [ObservableProperty]
        private ContactCreationForm _ccForm = new();

        [ObservableProperty]
        private ObservableCollection<string> _validationErrors;



        public NewContactViewModel(IServiceProvider serviceProvider, IContactService contactService)
        {
            _serviceProvider = serviceProvider;
            _contactService = contactService;
            _validationErrors = new();

        }

        [RelayCommand]
        private void SaveContact()
        {
            if (ValidateForm(CcForm))
            {
                var result = _contactService.Add(CcForm);

                if (result)
                {
                    var mainViewModel = _serviceProvider.GetRequiredService<MainViewModel>();
                    mainViewModel.CurrentViewModel = _serviceProvider.GetRequiredService<ContactsViewModel>();
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
        private void GoToContacts()
        {
            var mainViewModel = _serviceProvider.GetRequiredService<MainViewModel>();
            mainViewModel.CurrentViewModel = _serviceProvider.GetRequiredService<ContactsViewModel>();
        }

        private bool ValidateForm(ContactCreationForm form)
        {
            ValidationErrors.Clear();
            bool sucessful = true;
            var context = new ValidationContext(new Contact());
            var validationResults = new List<ValidationResult>();

            foreach (var property in typeof(ContactCreationForm).GetProperties())
            {
                context.MemberName = property.Name;
                sucessful = !Validator.TryValidateProperty(property.GetValue(form), context, validationResults) ? false : sucessful;

            }

            if (!sucessful)
            {
                foreach (ValidationResult result in validationResults)
                {
                    ValidationErrors.Add(result!.ErrorMessage);
                }
            }

            if (string.IsNullOrEmpty(form.Email) && string.IsNullOrEmpty(form.PhoneNumber))
            {
                ValidationErrors.Add("*Either email or phone number must be provided");
                sucessful = false;
            }
            return sucessful;
        }
    }
}
