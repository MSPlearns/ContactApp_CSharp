using Domain.Factories;
using Domain.Models;
using Dtos;
using Moq;

namespace Domain.Test.Factories;
public class ContactFactory_Tests
{
    private readonly ContactFactory _contactFactory;
    private readonly Mock<IUniqueIdentifierGenerator> _uniqueIdentifierGenerator;

    public ContactFactory_Tests()
    {
        _uniqueIdentifierGenerator = new Mock<IUniqueIdentifierGenerator>();
        _contactFactory = new ContactFactory(_uniqueIdentifierGenerator.Object);
    }

    [Theory]
    [InlineData("John", "Doe", "as@gr.se", "123456789", "Test Address", "12345", "City")]
    [InlineData("Jane", "Doe", "", "123456789", "", "", "")]
    [InlineData("John", "Doe", "as@gr.se", "", "", "", "")]
    [InlineData("Jane", "Doe", "as@gr.se", "123456789", "", "", "")]
    public void Create_ShouldReturnContact_WhenContactCreationFormIsValid(string firstName, string lastName, string email, string phoneNumber, string address, string postcode, string city)
    {
        //Arrange
        _uniqueIdentifierGenerator
            .Setup(uig => uig.Generate())
            .Returns("123l");

        ContactCreationForm form = new()
        {
            FirstName = firstName,
            LastName = lastName,
            Email = email,
            PhoneNumber = phoneNumber,
            Address = address,
            Postcode = postcode,
            City = city,
        };

        //Act
        Contact result = _contactFactory.Create(form);

        //Assert
        Assert.NotNull(result);
        Assert.IsType<Contact>(result);
    }

    [Theory]
    [InlineData("John", "Doe", "as@gr.se", "123456789", "Test Address", "12345", "City")]
    [InlineData("Jane", "Doe", "", "123456789", "", "", "")]
    [InlineData("John", "Doe", "as@gr.se", "", "", "", "")]
    [InlineData("Jane", "Doe", "as@gr.se", "123456789", "", "", "")]
    public void Create_ContacrFieldsShouldMatchFormFields_WhenContactCreationFormIsValid(string firstName, string lastName, string email, string phoneNumber, string address, string postcode, string city)
    {
        //Arrange
        _uniqueIdentifierGenerator
            .Setup(uig => uig.Generate())
            .Returns("123l");

        ContactCreationForm form = new()
        {
            FirstName = firstName,
            LastName = lastName,
            Email = email,
            PhoneNumber = phoneNumber,
            Address = address,
            Postcode = postcode,
            City = city,
        };

        //Act
        Contact result = _contactFactory.Create(form);

        //Assert
        Assert.Equal(form.FirstName, result.FirstName);
        Assert.Equal(form.LastName, result.LastName);
        Assert.Equal(form.Email, result.Email);
        Assert.Equal(form.PhoneNumber, result.PhoneNumber);
        Assert.Equal(form.Address, result.Address);
        Assert.Equal(form.Postcode, result.Postcode);
        Assert.Equal(form.City, result.City);
    }

    [Theory]
    //Mandatory fields missing
    [InlineData("", "Doe", "as@gr.se", "123456789", "Test Address", "12345", "City")]
    [InlineData("Jane", "", "", "123456789", "", "", "")]
    [InlineData("John", "Doe", "", "", "", "", "")]
    [InlineData("Jane", "Doe", "", "", "Test Address", "12345", "City")]
    //Invalid fields
    [InlineData("J", "Doe", "as@gr.se", "123456789", "Test Address", "12345", "City")]
    [InlineData("Jane", "D", "as@gr.se", "123456789", "Test Address", "12345", "City")]
    [InlineData("Jane", "Doe", "as", "123456789", "Test Address", "12345", "City")]
    [InlineData("Jane", "Doe", "as@gr.se", "123", "Test Address", "12345", "City")]
    [InlineData("Jane", "Doe", "as@gr.se", "123456789", "T", "12345", "City")]
    [InlineData("Jane", "Doe", "as@gr.se", "123456789", "Test Address", "12345666666", "City")]
    [InlineData("Jane", "Doe", "as@gr.se", "123456789", "Test Address", "12345", "C")]
    public void Create_ShouldReturnNull_WhenContactCreationFormIsNotValid(string firstName, string lastName, string email, string phoneNumber, string address, string postcode, string city)
    {
        // Arrange
        ContactCreationForm form = new()
        {
            FirstName = firstName,
            LastName = lastName,
            Email = email,
            PhoneNumber = phoneNumber,
            Address = address,
            Postcode = postcode,
            City = city,
        };

        // Act
        Contact result = _contactFactory.Create(form);
        // Assert
        Assert.Null(result);
    }

    [Fact]
    public void ValidateId_ShouldReturnTrue_WhenIdIsNotNullOrEmpty()
    {
        // Arrange
        string id = "test";
        // Act
        bool result = ContactFactory.ValidateId(id);
        // Assert
        Assert.True(result);
    }

    [Theory]
    [InlineData("")]
    [InlineData(null)]
    public void ValidateId_ShouldReturnFalse_WhenIdIsNullOrEmpty(string id)
    {
        // Arrange
        // Act
        bool result = ContactFactory.ValidateId(id);
        // Assert
        Assert.False(result);
    }
}