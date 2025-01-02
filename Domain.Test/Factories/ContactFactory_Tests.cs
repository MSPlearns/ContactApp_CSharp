using Domain.Factories;
using Domain.Models;
using Dtos;

namespace Domain.Test.Factories
{
    public class ContactFactory_Tests
    {
        [Theory]
        [InlineData("John", "Doe", "as@gr.se", "123456789", "Test Address", "12345", "City")]
        [InlineData("Jane", "Doe", "", "123456789", "", "", "")]
        [InlineData("John", "Doe", "as@gr.se", "", "", "", "")]
        [InlineData("Jane", "Doe", "", "", "Test Address", "12345", "City")]
        public void Create_ShouldReturnContact_WhenContactCreationFormIsValid(string firstName, string lastName, string email, string phoneNumber, string address, string postcode, string city)
        {
            //Arrange
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
            Contact result = ContactFactory.Create(form);

            //Assert
            Assert.NotNull(result);
            Assert.IsType<Contact>(result);
            Assert.Equal(form.FirstName, result.FirstName);
            Assert.Equal(form.LastName, result.LastName);
            Assert.Equal(form.Email, result.Email);
            Assert.Equal(form.PhoneNumber, result.PhoneNumber);
            Assert.Equal(form.Address, result.Address);
            Assert.Equal(form.Postcode, result.Postcode);
            Assert.Equal(form.City, result.City);
        }



        [Theory]
        [InlineData("", "Doe", "as@gr.se", "123456789", "Test Address", "12345", "City")]
        [InlineData("Jane", "", "", "123456789", "", "", "")]
        //[InlineData("John", "Doe", "", "", "", "", "")]
        //I have to implement validation before this test can be implemented
        public void Create_ShouldReturnNull_WhenContactCreationFormIsMissingRequiredFields(string firstName, string lastName, string email, string phoneNumber, string address, string postcode, string city)
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
            }; // empty form

            // Act
            Contact result = ContactFactory.Create(form);

            // Assert
            Assert.Null(result);
        }
    }
}
