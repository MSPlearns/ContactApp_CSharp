using System.ComponentModel.DataAnnotations;

namespace Domain.Models
{
    public class Contact
    {
 
        [Required]
        public string Id { get; set; } = null!;
        
        [Required]
        [StringLength(50, MinimumLength = 2)]
        public string FirstName { get; set; } = null!;

        [Required]
        [StringLength(50, MinimumLength = 2)] 
        public string LastName { get; set; } = null!;

        [RegularExpression(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,6}$")]
        public string? Email { get; set; } = null!;

        [RegularExpression(@"^\+?\d{10,15}$")]
        public string? PhoneNumber { get; set; } = null!;

        [RegularExpression(@"^[A-Za-z0-9\s,.'\-#\/]{5,200}$")] //letters, digits, spaces, commas, periods, hyphens, slashes, and the # symbol, length 5-200
        public string? Address { get; set; } = null!;

        [RegularExpression(@"^[A-Za-z0-9\s-]{5,10}$")]
        public string? Postcode { get; set; } = null!;

        [RegularExpression(@"^[A-Za-zÀ-ÿ\s'\-]{2,100}$")] //letters, spaces, hyphens, and apostrophes, lenght 2-100
        public string? City { get; set; } = null!;


        //public Contact(string id, string firstName, string lastName, string email, string phoneNumber, string address, string postcode, string city)
        //{
        //    Id = id;
        //    FirstName = firstName;
        //    LastName = lastName;
        //    Email = email;
        //    PhoneNumber = phoneNumber;
        //    Address = address;
        //    Postcode = postcode;
        //    City = city;
        //}
    }
}
