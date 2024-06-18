using System.Collections.Generic;
using Newtonsoft.Json;

namespace FormAPI.Models
{
    public class FormField //provides metadata about formfild.describes structure $xtics of each formfield
    {
        public string Name { get; set; }

        [JsonIgnore]//to hide id in requests it uses newtonsoft.json
        public int Id { get; set; }

       // [JsonIgnore]
        //public int FormId { get; set; }  // Foreign key to the Form entity

        public bool Required { get; set; } // Indicates whether the form field is required for submission.

        public string Attributes { get; set; }// Additional attributes or metadata associated with the form field.
        //public object Attributes { get; set; } = new { };
        public string Kind { get; set; }// Represents the category or type of the form field (e.g., "profile", "address", "html").
        public string FieldType { get; set; } //Specifies the type of data expected for the form field (e.g., "text", "email", "checkbox").
                                              //public Dictionary<string, string> Rules { get; set; }//Defines validation rules or constraints specific to the form field (e.g., minimum/maximum length, regex pattern
        public string Rules { get; set; } //= new { };

        [JsonIgnore]
        public int FormId { get; set; }  // foreign key to the form entity

        [JsonIgnore]
        public Form Form { get; set; }  // navigation property
    }
}
