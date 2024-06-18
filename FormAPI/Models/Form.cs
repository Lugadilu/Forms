using Newtonsoft.Json;

namespace FormAPI.Models
{
    public class Form
    {
        //[JsonIgnore]//to hide id in requests it uses newtonsoft.json
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        // Navigation property for the collection of FormFields associated with this form
        public ICollection<FormField> Fields { get; set; }
        // Navigation property for the collection of FormRecords associated with this form
        public ICollection<FormRecord> Records { get; set; }



    }
}
