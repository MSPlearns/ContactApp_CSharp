using Business.Services;
using Dtos;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.DependencyInjection;
using Domain.Models;
using System.ComponentModel.DataAnnotations;

namespace Presentation.WinPF_App.ViewModels;
//TODO: Some sort of shared validation class that could be used in all contact forms? With overloaded methods for different types of context?
public partial class NewContactViewModel : ObservableObject
{
    private readonly IServiceProvider _serviceProvider;
    private readonly IContactService _contactService;

    [ObservableProperty]
    private ContactCreationForm _ccForm = new();

    [ObservableProperty]
    private string _errorMessage;

    [ObservableProperty]
    private string _headline = "ADD NEW CONTACT";

    public NewContactViewModel(IServiceProvider serviceProvider, IContactService contactService)
    {
        _serviceProvider = serviceProvider;
        _contactService = contactService;
        _errorMessage = "";
    }

    [RelayCommand]
    private void SaveContact()
    {
        if (ValidateForm(CcForm))
        {
            try
            {
                var result = _contactService.Add(CcForm);

                if (result)
                {
                    GoToContacts();
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
    private void GoToContacts()
    {
        var mainViewModel = _serviceProvider.GetRequiredService<MainViewModel>();
        mainViewModel.CurrentViewModel = _serviceProvider.GetRequiredService<ContactsViewModel>();
    }

    private bool ValidateForm(ContactCreationForm form)
    {
        bool sucessful = true;
        var context = new ValidationContext(new Contact());
        var validationResults = new List<ValidationResult>();
        var validationErrors = new List<string>();

        foreach (var property in typeof(ContactCreationForm).GetProperties())
        {
            context.MemberName = property.Name;
            if (!Validator.TryValidateProperty(property.GetValue(form), context, validationResults))
            {
                sucessful = false;
            }
        }

        if (!sucessful)
        {
            foreach (ValidationResult result in validationResults)
            {
                validationErrors.Add(result!.ErrorMessage!);
            }
        }

        if (string.IsNullOrEmpty(form.Email) && string.IsNullOrEmpty(form.PhoneNumber))
        {
            validationErrors.Add("*Either email or phone number must be provided");
            sucessful = false;
        }

        ErrorMessage = string.Join(Environment.NewLine, validationErrors);
        return sucessful;
    }
}