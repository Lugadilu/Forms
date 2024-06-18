using FormAPI.Models;
using System.Text.Json.Serialization; // For System.Text.Json
namespace FormAPI.DTOs
{
    public class FormDto
    {
       // [JsonIgnore] // Use this line
       // public int? Id { get; set; }
        public string Name { get; set; }
         public string Description { get; set; }
       // public string Description { get; set; } = string.Empty;
       // public ICollection<FormField> Fields { get; set; }
        //public ICollection<FormRecordDto> Records { get; set; }
    }
}
