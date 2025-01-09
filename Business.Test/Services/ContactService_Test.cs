using Business.Services;
using DataManagement.Services;
using Domain.Factories;
using Domain.Models;
using Dtos;
using Moq;
using System.Net;

namespace Business.Test.Services
{
    //TODO: Update with Delete and Update method tests
    public class ContactService_Test
    {
        private readonly Mock<IContactFactory> _contactFactoryMock;
        private readonly Mock<IDataService> _dataServiceMock;
        private readonly IContactService _contactService;

        public ContactService_Test()
        {
            _contactFactoryMock = new Mock<IContactFactory>();
            _dataServiceMock = new Mock<IDataService>();
            _contactService = new ContactService(_contactFactoryMock.Object, _dataServiceMock.Object);

        }

        [Fact]
        public void Add_ShouldAddContactToList_SaveToFile_AndReturnTrue()
        {
            // Arange
            var contactCreationForm = new ContactCreationForm();

            _dataServiceMock
                .Setup(fs => fs.SaveListToFile(It.IsAny<List<Contact>>()));

            _contactFactoryMock
                .Setup(cf => cf.Create(It.IsAny<ContactCreationForm>()))
                .Returns(new Contact());

            // Act
            var result = _contactService.Add(contactCreationForm);

            // Assert
            Assert.True(result);
            _contactFactoryMock.Verify(cf => cf.Create(It.IsAny<ContactCreationForm>()), Times.Once);
            _dataServiceMock.Verify(fs => fs.SaveListToFile(It.IsAny<List<Contact>>()), Times.Once);
        }

        [Fact]
        public void ShowAll_ShouldReturnAContactList()
        {
            // Arange
            var contactCreationForm = new ContactCreationForm();

            _dataServiceMock
                .Setup(fs => fs.LoadListFromFile<Contact>())
                .Returns(new List<Contact>());


            // Act
            var result = _contactService.GetAll();

            // Assert
            Assert.IsType<List<Contact>>(result);
            _dataServiceMock.Verify(fs => fs.LoadListFromFile<Contact>(),Times.Once);
        }
    }
}
