using System;

namespace FormAPI.DTOs
{
    public class FormRecordDto
    {
        public Guid Id { get; set; }
        public Guid FormId { get; set; }
        // public string FormFieldValues { get; set; }
        public Dictionary<string, string> FormFieldValues { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}













/*

using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization; // For System.Text.Json
namespace FormAPI.DTOs
{
    public class FormRecordDto
    {
        
        public string FirstName { get; set; }
        
        public string SecondName { get; set; }
       
        public string? LastName { get; set; }
        
        public DateTime Birthdate { get; set; } = DateTime.MinValue;
        
        public string? Gender { get; set; }
       
        public string? LanguageCode { get; set; }
       
        public string? Nationality { get; set; }
      
        public string PhoneNumber { get; set; }
    
        public string Email { get; set; }
     
        public DateTime Arrival { get; set; } = DateTime.MinValue;
       
        public DateTime Departure { get; set; } = DateTime.MinValue;
       
        public string? Address { get; set; }
       
        public string? Zip { get; set; }
       
        public string? City { get; set; }
       
        public string? Country { get; set; }
        public string Rating { get; set; }
       
    }
}
*/