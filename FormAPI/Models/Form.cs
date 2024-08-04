using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;

namespace FormAPI.Models
{
    public class Form
    {
        //[JsonIgnore]//to hide id in requests it uses newtonsoft.json
        // public int Id { get; set; }
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Name { get; set; }
        public string Description { get; set; }
        // Navigation property for the collection of FormFields associated with this form
        //public ICollection<FormField> Fields { get; set; }
        // Navigation property for the collection of FormRecords associated with this form
       //public ICollection<FormRecord> Records { get; set; }
        public ICollection<Page> Pages { get; set; } = new List<Page>();
        public ICollection<FormRecord> FormRecords { get; set; } = new List<FormRecord>();



    }
}
