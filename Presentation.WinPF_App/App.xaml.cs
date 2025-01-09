using Business.Helpers;
using Business.Services;
using DataManagement.Services;
using Domain.Factories;
using Domain.Models;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Presentation.WinPF_App.ViewModels;
using Presentation.WinPF_App.Views;
using System.Windows;

namespace Presentation.WinPF_App
{

    public partial class App : Application
    {
        private IHost _host;

    public App()
        {
            _host = Host.CreateDefaultBuilder()
                .ConfigureServices((services) =>
                {
                    services.AddSingleton<IUniqueIdentifierGenerator, UniqueIdentifierGenerator>();
                    services.AddSingleton<IContactFactory, ContactFactory>();
                    services.AddSingleton<IDataService, DataService>();
                    services.AddSingleton<IContactService, ContactService>();
                  
                    services.AddSingleton<MainViewModel>();
                    services.AddSingleton<MainWindow>();

                    services.AddTransient<ContactsViewModel>();
                    services.AddTransient<ContactsView>();

                    services.AddTransient<NewContactViewModel>();
                    services.AddTransient<NewContactView>();

                    services.AddTransient<ContactDetailViewModel>();
                    services.AddTransient<ContactDetailView>();

                    services.AddTransient<EditContactViewModel>();
                    services.AddTransient<EditContactView>();
                })
                .Build();
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            var mainWindow = _host.Services.GetRequiredService<MainWindow>();
            mainWindow.Show();
        }
    }
}
