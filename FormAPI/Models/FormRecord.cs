
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FormAPI.Models
{
    public class FormRecord
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        [JsonIgnore]
        public Guid FormId { get; set; }
        [JsonIgnore]
        public Form Form { get; set; } // Navigation property

        // JSON string to store form field values
        public string FormFieldValues { get; set; } = "{}"; //stores a JSON string
        // Dictionary to store form field values
        //public Dictionary<string, string> FormFieldValues { get; set; } = new Dictionary<string, string>();

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}










/*

using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FormAPI.Models
{
    public class FormRecord
    {
        // [JsonIgnore]//to hide id in requests it uses newtonsoft.json
        // public int Id { get; set; }
        public Guid Id { get; set; } = Guid.NewGuid();


        [Required(ErrorMessage = "First name is required")]
        public string FirstName { get; set; }
        public FormRecord()
        {
            FirstName = "";
            SecondName = "";
            
        }

        [Required(ErrorMessage = "Second name is required")]

        public string SecondName { get; set; }


        [Required(ErrorMessage = "Last name is required")]
        public string? LastName { get; set; }


        //[DataType(DataType.Date)]
        //[DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        //public DateTime Birthdate { get; set; }

        //public DateTime? Birthdate { get; set; } = null; // Assigns null as the default value
        public DateTime Birthdate { get; set; } = DateTime.MinValue; // Default birthdate to minimum DateTime value in UTC
        [Required(ErrorMessage = "Gender is required")]
        public string? Gender { get; set; }
        public string? LanguageCode { get; set; }
        public string? Nationality { get; set; }
        [RegularExpression(@"^\d{10}$", ErrorMessage = "Phone number must be 10 digits")]
        public string PhoneNumber { get; set; }
        [EmailAddress(ErrorMessage = "Invalid email address")]
        public string Email { get; set; }

        //public DateTime Arrival { get; set; } = default; // Assigns default DateTime value
        //public DateTime Departure { get; set; } = default; // Assigns default DateTime value
        public DateTime Arrival { get; set; } = DateTime.MinValue; // Default arrival to minimum DateTime value in UTC
        public DateTime Departure { get; set; } = DateTime.MinValue;// ToUniversalTime(); // Default departure to minimum DateTime value in UTC

        public string? Address { get; set; }
        public string? Zip { get; set; }
        public string? City { get; set; }
        public string? Country { get; set; }
        [Required]
        public string Rating { get; set; }


        // public int FormId { get; set; }  // Foreign key to the Form entity

        public Guid FormId { get; set; }  // Changed to Guid

        [JsonIgnore]
        public Form Form { get; set; } // Navigation property

    }
}
*/