namespace FormAPI.Models
{
    public class Option
    {
        public int Id { get; set; }
        public string Value { get; set; }

        // Additional properties if needed

        // Navigation property to FormField
        public FormField FormField { get; set; }
        public string FormFieldId { get; set; }
    }
}
