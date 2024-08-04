using FormAPI.Models;
using System.Text.Json.Serialization; // For System.Text.Json
namespace FormAPI.DTOs
{
    public class FormDto
    {
        // [JsonIgnore] 
        
        public Guid Id { get; set; }

        public string Name { get; set; }
         public string Description { get; set; }

        //public ICollection<PageDto> Pages { get; set; } = new List<PageDto>();
        public List<PageDto> Pages { get; set; } = new List<PageDto>();

        [JsonIgnore]
        public ICollection<FormRecordDto> FormRecords { get; set; } = new List<FormRecordDto>();

        [JsonPropertyName("type")]
        public string Type => "form";

        
    }
}
