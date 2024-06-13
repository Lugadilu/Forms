using FormAPI.Models;
using FormAPI.Service;

namespace FormAPI.Main
{
    public class Program
    {
        public static void Main()
        {
            var formService = new FormService();

            // Assume formFields is a list of FormField objects retrieved from somewhere
            List<FormField> formFields = new List<FormField>();

            // Populate formFields with FormField metadata

            FormRecord formRecord = formService.CreateFormRecord(formFields);
            string jsonResponse = formRecord.ToJson();

            Console.WriteLine(jsonResponse);
        }
    }

}
