using FormAPI.Models;
using Newtonsoft.Json;

namespace FormAPI.DTOs
{
    public class PageDto
    {
       // [JsonIgnore]
        public Guid Id { get; set; }
       
       // public Guid FormId { get; set; }

       // [JsonIgnore]
        //public ICollection<FormFieldDto> Fields { get; set; } = new List<FormFieldDto>();
        public List<FormFieldDto> Fields { get; set; }
    }
}
