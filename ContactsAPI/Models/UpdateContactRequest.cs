using System.ComponentModel.DataAnnotations;

namespace ContactsAPI.Models
{
    public class UpdateContactRequest
    {
        [System.ComponentModel.DataAnnotations.Required]
        public string? Name { get; set; }
        [Required]
        [DataType(DataType.EmailAddress)]
        [EmailAddress(ErrorMessage = "Invalid email address")]
        public string? Email { get; set; }
        [Required]
        [DataType(DataType.PhoneNumber)]
        [Phone(ErrorMessage = "Invalid phone number")]
        public string? Phone { get; set; }
        [System.ComponentModel.DataAnnotations.Required]
        public string? Address { get; set; }
    }
}
