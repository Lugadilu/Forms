using FormAPI.Models;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace FormAPI.DTOs
{
    public class PageDto
    {
        [JsonIgnore]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Each page must have a form ID.")]
        [JsonIgnore]
        public Guid FormId { get; set; }

        // [JsonIgnore]
        //public ICollection<FormFieldDto> Fields { get; set; } = new List<FormFieldDto>();
        [Required(ErrorMessage = "At least one field is required.")]
        public List<FormFieldDto> Fields { get; set; }
    }
}
