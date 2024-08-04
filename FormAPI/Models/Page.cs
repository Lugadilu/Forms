using System.ComponentModel.DataAnnotations.Schema;

namespace FormAPI.Models
{
    public class Page
    {

        
        public Guid Id { get; set; } = Guid.NewGuid();

        public Guid FormId { get; set; }

        public Form Form { get; set; }
       // public string Title { get; set; }
        public ICollection<FormField> FormFields { get; set; } = new List<FormField>();
    }
}
