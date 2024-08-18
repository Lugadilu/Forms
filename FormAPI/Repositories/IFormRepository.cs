using FormAPI.Models;

namespace FormAPI.Repositories
{
    public interface IFormRepository
    {
        Task<IEnumerable<Form>> GetAllFormsAsync();
        Task<Form> GetFormByIdAsync(Guid formId);
        Task AddFormAsync(Form form);
        Task UpdateFormAsync(Form form);
        Task DeleteFormAsync(Form form);

        Task<IEnumerable<FormField>> GetAllFormFieldsAsync();

        Task<IEnumerable<FormRecord>> GetAllFormRecordsAsync();
        Task<FormRecord> GetFormRecordByIdAsync(Guid formId, Guid recordId);

        

        Task<FormRecord> GetFormRecordByFormIdAsync(Guid formId);


        Task AddFormRecordAsync(FormRecord formRecord);
        Task UpdateFormRecordAsync(FormRecord formRecord);
        Task DeleteFormRecordAsync(FormRecord formRecord);
        Task<IEnumerable<FormField>> GetFormFieldsByFormIdAsync(Guid formId);
        Task<Form> GetFormWithFieldsAsync(Guid formId);
        /*
        Task<IEnumerable<Form>> GetAllFormsAsync();
        Task<Form> GetFormByIdAsync(Guid id);
        Task<Form> CreateFormAsync(Form form);
        Task<Form> UpdateFormAsync(Form form);
        Task DeleteFormAsync(Guid id);

        Task<IEnumerable<FormField>> GetAllFormFieldsAsync();
        Task<FormField> CreateFormFieldAsync(FormField formField);
        Task<bool> DeleteFormFieldAsync(Guid id);

        Task<IEnumerable<FormRecord>> GetAllFormRecordsAsync();
        Task<FormRecord> GetFormRecordByIdAsync(Guid id);
        Task<FormRecord> CreateFormRecordAsync(FormRecord formRecord);
        Task<FormRecord> UpdateFormRecordAsync(FormRecord formRecord);
        Task<bool> DeleteFormRecordAsync(Guid id);

        Task<IEnumerable<FormRecord>> GetFormRecordsByFormIdAsync(Guid formId);
        Task<FormRecord> CreateFormRecordForFormAsync(Guid formId, FormRecord formRecord);
        Task<FormRecord> UpdateFormRecordForFormAsync(Guid formId, FormRecord formRecord);
        Task<bool> DeleteFormRecordForFormAsync(Guid formId, Guid recordId);
        
        */

    }
}








/*using FormAPI.Models;
using Microsoft.AspNetCore.Http;

namespace FormAPI.Repositories
{
    public interface IFormRepository
    {
        
        Task<IEnumerable<Form>> GetAllFormsAsync();
        Task<Form> GetFormByIdAsync(int id);
        Task<Form> CreateFormAsync(Form form);
        Task<Form> UpdateFormAsync(Form form);
        Task DeleteFormAsync(int id);

        
        Task<IEnumerable<FormField>> GetAllFormFieldsAsync();
        Task<FormField> CreateFormFieldAsync(FormField formField);
        Task<bool> DeleteFormFieldAsync(int id);

       
        Task<IEnumerable<FormRecord>> GetAllFormRecordsAsync();
        Task<FormRecord> GetFormRecordByIdAsync(int id);
        Task<FormRecord> CreateFormRecordAsync(FormRecord formRecord);
        Task<FormRecord> UpdateFormRecordAsync(FormRecord formRecord);
        Task<bool> DeleteFormRecordAsync(int id);




        Task<IEnumerable<FormRecord>> GetFormRecordsByFormIdAsync(int formId);
        Task<FormRecord> CreateFormRecordForFormAsync(int formId, FormRecord formRecord);
        Task<FormRecord> UpdateFormRecordForFormAsync(int formId, FormRecord formRecord);
        Task<bool> DeleteFormRecordForFormAsync(int formId, int recordId);
        */


        //Task<IEnumerable<FormField>> GetAllFormFieldsAsync();

        ////void CreateFormField(FormField formField);



        //Task<FormField> CreateFormFieldAsync(FormField formField);

        //Task<IEnumerable<FormRecord>> GetAllFormRecordsAsync();

        //Task<FormRecord> GetFormRecordByIdAsync(int id);
        //Task<FormRecord> CreateFormRecordAsync(FormRecord formRecord);

   // }
//}



//using System.Collections.Generic;
//using System.Threading.Tasks;
//using FormAPI.Models;

//namespace FormAPI.Services
//{
//    public interface IFormService
//    {
//        Task<List<FormField>> GetFormFieldsAsync();
//        Task CreateFormFieldAsync(FormField formField);

//        Task<List<FormPage>> GetFormPagesAsync();
//        Task CreateFormPageAsync(FormPage formPage);

//        Task<List<FormRecord>> GetFormRecordsAsync();
//        Task CreateFormRecordAsync(FormRecord formRecord);

//        Task<List<FormCreate>> GetFormCreatesAsync();
//        Task CreateFormCreateAsync(FormCreate formCreate);
//    }

//    public class FormService : IFormService
//    {
//        public async Task<List<FormField>> GetFormFieldsAsync()
//        {
//            // Implement asynchronous logic to retrieve form fields from database or other source
//            // Example: await _dbContext.FormFields.ToListAsync();
//            return new List<FormField>();
//        }

//        public async Task CreateFormFieldAsync(FormField formField)
//        {
//            // Implement asynchronous logic to create form field in database or other source
//            // Example: _dbContext.FormFields.AddAsync(formField); await _dbContext.SaveChangesAsync();
//        }

//        // Implement similar methods for other operations (GetFormPagesAsync, CreateFormPageAsync, etc.)
//    }
//}
