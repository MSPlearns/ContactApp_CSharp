using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.DependencyInjection;

namespace Presentation.WinPF_App.ViewModels;
public partial class GetStartedViewModel : ObservableObject
{
    private readonly IServiceProvider _serviceProvider;
    public GetStartedViewModel(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    [RelayCommand]
    private void GoToNewContact()
    {
        var mainViewModel = _serviceProvider.GetRequiredService<MainViewModel>();
        mainViewModel.CurrentViewModel = _serviceProvider.GetRequiredService<NewContactViewModel>();
    }
}