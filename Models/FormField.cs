using System.Collections.Generic;

namespace FormAPI.Models
{
    public class FormField //provides metadata about formfild.describes structure $xtics of each formfield
    {
        public string Name { get; set; }
        public string Id { get; set; }
        public bool Required { get; set; } // Indicates whether the form field is required for submission.
        public Dictionary<string, string> Attributes { get; set; }// Additional attributes or metadata associated with the form field.
        public string Kind { get; set; }// Represents the category or type of the form field (e.g., "profile", "address", "html").
        public string FieldType { get; set; } //Specifies the type of data expected for the form field (e.g., "text", "email", "checkbox").
        public Dictionary<string, string> Rules { get; set; }//Defines validation rules or constraints specific to the form field (e.g., minimum/maximum length, regex pattern

       
        /*// Method to validate field value
        public bool ValidateFieldValue(object value)
        {
            if (value == null)
            {
                return !Required; // field not required, so its valid if empty
            }
            //perform typpe-specific validation based on FieldType
            switch (FieldType)
            {
                case "text":
                    // Example: validate minimum and maximum length
                    int minLength = Rules.ContainsKey("minLength") ? Convert.ToInt32(Rules["minLength"]) : 0;
                    int maxLength = Rules.ContainsKey("maxLength") ? Convert.ToInt32(Rules["maxLength"]) : int.MaxValue;
                    string textValue = value.ToString();
                    return textValue.Length >= minLength && textValue.Length <= maxLength;

                case "email":
                    // Example: validate email format
                    string emailValue = value.ToString();
                    return IsValidEmail(emailValue);

                // Add more cases for other field types as needed

                default:
                    // No specific validation for other field types
                    return true;
            }
        }
*/
        // Method to validate email format
        private bool IsValidEmail(string email)
        {
            // Example: use a regular expression to validate email format
            // This is a simplified regex for demonstration purposes
            return !string.IsNullOrEmpty(email) &&
                   System.Text.RegularExpressions.Regex.IsMatch(email,
                   @"^(.+)@(.+)$");
        }
    }
}
