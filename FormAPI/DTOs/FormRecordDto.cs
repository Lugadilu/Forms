using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization; // For System.Text.Json
namespace FormAPI.DTOs
{
    public class FormRecordDto
    {
        //[JsonIgnore] // Use this line
       // public int? Id { get; set; } // Use nullable type if you want to set it to null later
        //[JsonIgnore] // Use this line
        //public int FormId { get; set; }

        [Required]
        public string FirstName { get; set; }
        [Required]
        public string SecondName { get; set; }
        [Required]
        public string? LastName { get; set; }
        [Required]
        public DateTime Birthdate { get; set; } = DateTime.MinValue;
        [Required]
        public string? Gender { get; set; }
        [Required]
        public string? LanguageCode { get; set; }
       
        public string? Nationality { get; set; }
        [Required]
        public string PhoneNumber { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public DateTime Arrival { get; set; } = DateTime.MinValue;
        [Required]
        public DateTime Departure { get; set; } = DateTime.MinValue;
        [Required]
        public string? Address { get; set; }
        [Required]
        public string? Zip { get; set; }
        [Required]
        public string? City { get; set; }
        [Required]
        public string? Country { get; set; }
        //public string Kind { get; set; } // Represents the category or type of the form field
        //public string FieldType { get; set; } // Specifies the type of data expected for the form field
        //public Dictionary<string, string> Attributes { get; set; }

       // public int FormId { get; set; }
    }
}
