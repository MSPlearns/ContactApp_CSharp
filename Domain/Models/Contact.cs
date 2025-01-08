using System.ComponentModel.DataAnnotations;

namespace Domain.Models
{
    public class Contact
    {

        [Required]
        public string Id { get; set; } = null!;

        [Required(ErrorMessage = "*First name field is required")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "*First name must contain 2 to 50 characters")]
        [RegularExpression(@"^[a-zA-ZáéíóúÁÉÍÓÚñÑüÜäöüßøØåÅæÆ'’\s-]+$", ErrorMessage = "*First name cannot contain special characters")]
        public string FirstName { get; set; } = null!;

        [Required(ErrorMessage = "*Last name field is required")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "*Last name must contain 2 to 50 characters")]
        [RegularExpression(@"^[a-zA-ZáéíóúÁÉÍÓÚñÑüÜäöüßøØåÅæÆ'’\s-]+$", ErrorMessage = "*Last name cannot contain special characters")]
        public string LastName { get; set; } = null!;

        [RegularExpression(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,6}$", ErrorMessage = "*Email must be a valid email address")]
        public string? Email { get; set; } = null!;

        [RegularExpression(@"^\+?\d{9,15}$", ErrorMessage = "*Phone numbers must be 9 to 15 characters long and contain only numbers and an optional +")]
        public string? PhoneNumber { get; set; } = null!;

        [RegularExpression(@"^[A-Za-z0-9\s,.'\-#\/]{5,200}$", ErrorMessage = "*Address must be 5 to 200 characters long and contain only valid characters")] //letters, digits, spaces, commas, periods, hyphens, slashes, and the # symbol, length 5-200
        public string? Address { get; set; } = null!;

        [RegularExpression(@"^[A-Za-z0-9\s-]{5,10}$", ErrorMessage = "*Postcode must be 5 to 10 characters long and contain only valid characters")]
        public string? Postcode { get; set; } = null!;

        [RegularExpression(@"^[A-Za-zÀ-ÿ\s'\-]{2,100}$", ErrorMessage = "*City must be 2 to 100 characters long and contain only valid characters")] //letters, spaces, hyphens, and apostrophes, lenght 2-100
        public string? City { get; set; } = null!;


        public string FullName => $"{FirstName} {LastName}";

        public string DisplayPhoneOrEmail => !string.IsNullOrEmpty(PhoneNumber) ? PhoneNumber : Email;
    }
}
