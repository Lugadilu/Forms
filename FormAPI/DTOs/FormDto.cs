using FormAPI.Models;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization; // For System.Text.Json
namespace FormAPI.DTOs
{
    public class FormDto
    {
        // [JsonIgnore] 
        
        public Guid Id { get; set; }

        [Required(ErrorMessage = "The Name field is required.")]
        [StringLength(100, ErrorMessage = "The Name field must be a maximum of 100 characters.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "The Description field is required.")]
        public string Description { get; set; }

        //public ICollection<PageDto> Pages { get; set; } = new List<PageDto>();
        [Required(ErrorMessage = "At least one page is required.")]
        public List<PageDto> Pages { get; set; } = new List<PageDto>();

        [JsonIgnore]
        public ICollection<FormRecordDto> FormRecords { get; set; } = new List<FormRecordDto>();

        [JsonPropertyName("type")]
        public string Type => "form";

        
    }
}
