using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace FormAPI.Models
{
    public class FormRecord
    {
        [JsonIgnore]//to hide id in requests it uses newtonsoft.json
        public int Id { get; set; }
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

        
        public int FormId { get; set; }  // Foreign key to the Form entity

       [JsonIgnore]
        public Form Form { get; set; } // Navigation property


        //public string Kind { get; set; } // Represents the category or type of the form field
        //public string FieldType { get; set; } // Specifies the type of data expected for the form field
        //public Dictionary<string, string> Attributes { get; set; } // Additional attributes or metadata associated with the form field

        //public int FormId { get; set; } // Foreign key to the Form entity
        //public Form Form { get; set; } // Navigation property for the associated Form

        //public int FormFieldId { get; set; } // Foreign key to the FormField entity
        // public FormField FormField { get; set; } // Navigation property for the associated FormFiel
        //public ICollection<FormField> FormFields { get; set; }


        //public ForField zFormfield {get; set; } every field would belong to one formrecord
        // Method to convert FormRecord to JSON
        //public string ToJson()
        //{
        //    return JsonConvert.SerializeObject(this);
        //}
    }
}
