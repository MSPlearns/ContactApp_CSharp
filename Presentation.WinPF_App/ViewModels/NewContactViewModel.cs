using Business.Services;
using Dtos;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.DependencyInjection;
using Domain.Models;
using System.ComponentModel.DataAnnotations;

namespace Presentation.WinPF_App.ViewModels
{
    public partial class NewContactViewModel : ObservableObject
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly IContactService _contactService;
        [ObservableProperty]
        private ContactCreationForm _ccForm = new();

        public NewContactViewModel(IServiceProvider serviceProvider, IContactService contactService)
        {
            _serviceProvider = serviceProvider;
            _contactService = contactService;
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
        }

        [RelayCommand]
        private void GoToContacts()
        {
            var mainViewModel = _serviceProvider.GetRequiredService<MainViewModel>();
            mainViewModel.CurrentViewModel = _serviceProvider.GetRequiredService<ContactsViewModel>();
        }

        private bool ValidateForm(ContactCreationForm form)
        {
            //TODO: Display errors to the user if validation fails
            var context = new ValidationContext(new Contact());
            var validationResults = new List<ValidationResult>();
            bool sucessful = true;
            foreach (var property in typeof(ContactCreationForm).GetProperties())
            {
                context.MemberName = property.Name;
                if (!Validator.TryValidateProperty(property.GetValue(form), context, validationResults))
                {
                    sucessful = false;
                }
            }
            return sucessful; 
        }

    }
}
