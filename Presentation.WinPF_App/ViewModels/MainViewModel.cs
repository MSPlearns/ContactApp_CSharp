﻿using Business.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using Microsoft.Extensions.DependencyInjection;

namespace Presentation.WinPF_App.ViewModels;
//TODO: Improve navigation by saving last view and returning to it
//Create a lastView property
//Create a method that takes in a view(new),
//saves the current view to lastView,
//changes the current view to the new view

//TODO: avoid repeating code (commands to navigation buttons, delete) in each viewmodel... Or sharing commands between views?
public partial class MainViewModel : ObservableObject
{
    private readonly IServiceProvider _serviceProvider;

    [ObservableProperty]
    private ObservableObject _currentViewModel = null!;

    public MainViewModel(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
        var contactService = _serviceProvider.GetRequiredService<IContactService>(); //Maybe this shouldnt be done like this but with a injected property?
        if (contactService.IsEmpty())
        {
            _currentViewModel = _serviceProvider.GetRequiredService<GetStartedViewModel>();
        }
        else
        {
            _currentViewModel = _serviceProvider.GetRequiredService<ContactsViewModel>();
        }   
    }
}