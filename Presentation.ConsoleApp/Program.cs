using Business.Helpers;
using Business.Services;
using DataManagement.Services;
using Domain.Factories;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Presentation.ConsoleApp;

var host = Host.CreateDefaultBuilder()
    .ConfigureServices((context, services) =>
    {
       services.AddSingleton<IUniqueIdentifierGenerator, UniqueIdentifierGenerator>();
        services.AddSingleton<IContactFactory, ContactFactory>();
        services.AddSingleton<IDataService, DataService>();
        services.AddSingleton<IContactService, ContactService>();
        services.AddSingleton<ITextDisplayService, TextDisplayService>();
        services.AddTransient<IMenuService, MenuService>();
    })
    .Build();

using (var scope = host.Services.CreateScope())
{
    var menu = scope.ServiceProvider.GetRequiredService<IMenuService>();
    menu.Show();
};


//var IdGenerator = new UniqueIdentifierGenerator();
//var contactFactory = new ContactFactory(IdGenerator);
//var dataService = new DataService();
//var contactService = new ContactService(contactFactory, dataService);
//MenuService menu = new(contactService);
//menu.Show();
