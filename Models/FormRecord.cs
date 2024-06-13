using Newtonsoft.Json;

namespace FormAPI.Models
{
    public class FormRecord
    {
        public string FirstName { get; set; }
        public string SecondName { get; set; }
        public string LastName { get; set; }
        public DateTime Birthdate { get; set; }
        public string Gender { get; set; }
        public string LanguageCode { get; set; }
        public string Nationality { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public DateTime? Arrival { get; set; }
        public DateTime? Departure { get; set; }
        public string Address { get; set; }
        public string Zip { get; set; }
        public string City { get; set; }
        public string Country { get; set; }

        public string Kind { get; set; } // Represents the category or type of the form field
        public string FieldType { get; set; } // Specifies the type of data expected for the form field
        public Dictionary<string, string> Attributes { get; set; } // Additional attributes or metadata associated with the form field

        //public ForField zFormfield {get; set; } every field would belong to one formrecord
        // Method to convert FormRecord to JSON
        public string ToJson()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}
