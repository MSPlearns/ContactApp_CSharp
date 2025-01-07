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
                    services.AddSingleton<MainViewModel>();
                    services.AddSingleton<MainWindow>();

                    services.AddTransient<ContactsViewModel>();
                    services.AddTransient<ContactsView>();

                    services.AddTransient<NewContactViewModel>();
                    services.AddTransient<NewContactView>();
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
