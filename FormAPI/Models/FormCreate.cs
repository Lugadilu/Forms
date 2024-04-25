namespace FormAPI.Models
{
    public class FormCreate
    {
        public string Name { get; set; }
        public string Description { get; set; }
        List<FormPage> Pages { get; set; }

    }
}
