using Business.Services;
using DataManagement.Services;
using Domain.Factories;
using Domain.Models;
using Dtos;
using Moq;

namespace Business.Test.Services;
public class ContactService_Test
{
    private readonly Mock<IContactFactory> _contactFactoryMock;
    private readonly Mock<IDataService> _dataServiceMock;
    private readonly ContactService _contactService;

    public ContactService_Test()
    {
        _contactFactoryMock = new Mock<IContactFactory>();
        _dataServiceMock = new Mock<IDataService>();

        _dataServiceMock
            .Setup(fs => fs.LoadListFromFile<Contact>())
            .Returns([]);

        _dataServiceMock
            .Setup(fs => fs.SaveListToFile(It.IsAny<List<Contact>>()));

        _contactFactoryMock
            .Setup(cf => cf.Create(It.IsAny<ContactCreationForm>()))
            .Returns(new Contact());

        _contactService = new ContactService(_contactFactoryMock.Object, _dataServiceMock.Object);
    }

    [Fact]
    public void Add_ShouldAddContactToListAndReturnTrue()
    {
        // Arange
        var contactCreationForm = new ContactCreationForm();
        // Act
        var result = _contactService.Add(contactCreationForm);
        // Assert
        Assert.True(result);
        Assert.NotEmpty(_contactService.GetAll());
        _contactFactoryMock.Verify(cf => cf.Create(It.IsAny<ContactCreationForm>()), Times.Once);
        _dataServiceMock.Verify(fs => fs.SaveListToFile(It.IsAny<List<Contact>>()), Times.Once);
    }

    [Fact]
    public void Delete_ShouldReturnFalse_WhenContactDoesntExists()
    {
        // Arange
        Contact testContact = new Contact { Id = "testID" };
        // Act
        var result = _contactService.Delete(testContact);
        // Assert
        Assert.False(result);
    }

    [Fact]
    public void Delete_ShouldRemoveGivenContactFromListAndReturnTrue_WhenContactExists()
    {
        // Arange
        var contact = new Contact { Id = "testID" };

        _dataServiceMock
            .Setup(fs => fs.LoadListFromFile<Contact>())
            .Returns(new List<Contact> { contact });

        ContactService testContactService = new ContactService(_contactFactoryMock.Object, _dataServiceMock.Object);

        // Act
        var result = testContactService.Delete(contact);

        // Assert
        Assert.Empty(_contactService.GetAll());
        Assert.True(result);
        _dataServiceMock.Verify(fs => fs.LoadListFromFile<Contact>(), Times.Exactly(3));
    }

    [Fact]
    public void DoesExist_ShouldReturnFalse_WhenContactDoesntExist()
    {
        // Arange
        var contact = new Contact { Id = "testID" };
        // Act
        var result = _contactService.DoesExist(contact);
        // Assert
        Assert.False(result);
    }

    [Fact]
    public void DoesExist_ShouldReturnTrue_WhenContactExists()
    {
        // Arange
        var contact = new Contact { Id = "testID" };

        _dataServiceMock
            .Setup(fs => fs.LoadListFromFile<Contact>())
            .Returns(new List<Contact> { contact });

        ContactService testContactService = new ContactService(_contactFactoryMock.Object, _dataServiceMock.Object);

        // Act
        var result = testContactService.DoesExist(contact);
        // Assert
        Assert.True(result);
    }

    [Fact]
    public void GetAll_ShouldReturnAContactList()
    {
        // Arange
        var contactCreationForm = new ContactCreationForm();
        // Act
        var result = _contactService.GetAll();
        // Assert
        Assert.IsType<List<Contact>>(result);
        _dataServiceMock.Verify(fs => fs.LoadListFromFile<Contact>(), Times.Exactly(2));
    }

    [Fact]
    public void GetContactById_ShouldReturnAContactWithMatchingId_WhenContactExists()
    {
        // Arange
        var contact = new Contact { Id = "testID" };

        _dataServiceMock
            .Setup(fs => fs.LoadListFromFile<Contact>())
            .Returns(new List<Contact> { contact });

        ContactService testContactService = new ContactService(_contactFactoryMock.Object, _dataServiceMock.Object);

        //Act
        var result = testContactService.GetContactById("testID");

        //Assert
        Assert.Equal(contact.Id, result!.Id);
    }

    [Fact]
    public void GetContactById_ShouldReturnNull_WhenContactDoesntExists()
    {
        // Arange
        var contact = new Contact { Id = "testID" };
        //Act
        var result = _contactService.GetContactById("testID");
        //Assert
        Assert.Null(result);
    }

    [Fact]
    public void IsEmpty_ShouldReturnFalse_WhenContactListIsNotEmpty()
    {
        // Arange

        _dataServiceMock
            .Setup(fs => fs.LoadListFromFile<Contact>())
            .Returns(new List<Contact> { new Contact() });

        IContactService testContactService = new ContactService(_contactFactoryMock.Object, _dataServiceMock.Object);

        // Act
        var result = testContactService.IsEmpty();

        // Assert
        Assert.False(result);
    }

    [Fact]
    public void IsEmpty_ShouldReturnTrue_WhenContactListIsEmpty()
    {
        // Arange
        // Act
        var result = _contactService.IsEmpty();
        // Assert
        Assert.True(result);
    }

    [Fact]
    public void Update_ShouldReturnFalse_WhenContactDoesntExist()
    {
        // Arange
        var contact = new Contact { Id = "testID" };
        // Act
        var result = _contactService.Update(contact);
        // Assert
        Assert.False(result);
    }

    [Fact]
    public void Update_ShouldUpdateContactAndReturnTrue_WhenContactExists()
    {
        // Arange
        var contact = new Contact { Id = "testID", FirstName = "Original" };

        _dataServiceMock
            .Setup(fs => fs.LoadListFromFile<Contact>())
            .Returns(new List<Contact> { contact });
        var updatedContact = new Contact { Id = "testID", FirstName = "Updated" };

        ContactService testContactService = new ContactService(_contactFactoryMock.Object, _dataServiceMock.Object);

        // Act
        var result = testContactService.Update(updatedContact);

        // Assert
        Assert.True(result);
        Assert.Equal(updatedContact.FirstName, testContactService.GetContactById("testID")!.FirstName);
    }
}