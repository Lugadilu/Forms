﻿using FormAPI.Models;
using Microsoft.AspNetCore.Http;

namespace FormAPI.Repositories
{
    public interface IFormRepository
    {
        //List<FormField> GetFormFields();
        Task<IEnumerable<FormField>> GetAll();

        //void CreateFormField(FormField formField);



        Task<FormField> Create(FormField formField);

        //List<FormPage> GetFormPages();
        //void CreateFormPage(FormPage formPage);

        //List<FormRecord> GetFormRecords();
        //void CreateFormRecord(FormRecord formRecord);

        //List<FormCreate> GetFormCreates();
        //void CreateFormCreate(FormCreate formCreate);
    }
}



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
