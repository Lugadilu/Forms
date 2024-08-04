using System.Text.Json.Serialization;
namespace FormAPI.DTOs
{
    public class FormFieldDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
         //[JsonIgnore]
        
        
        public bool Required { get; set; }
        // public string Attributes { get; set; }
        public object Attributes { get; set; } = new { };
        public string Kind { get; set; }
        public string FieldType { get; set; }
        // public Dictionary<string, string> Rules { get; set; }
        public object Rules { get; set; } = new { };
       
        //public int FormId { get; set; }


    }
}
