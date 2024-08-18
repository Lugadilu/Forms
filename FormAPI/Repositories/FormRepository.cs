using FormAPI.Context;
using FormAPI.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FormAPI.Repositories
{
    public class FormRepository : IFormRepository
    {
        private readonly ApplicationDbContext _context;

        public FormRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Form>> GetAllFormsAsync()
        {
            return await _context.forms
                .Include(f => f.Pages)
                .ThenInclude(p => p.FormFields)
                .ToListAsync();
        }

        public async Task<Form> GetFormByIdAsync(Guid formId)
        {
            return await _context.forms
                .Include(f => f.Pages)
                .ThenInclude(p => p.FormFields)
                .FirstOrDefaultAsync(f => f.Id == formId);
        }

        public async Task AddFormAsync(Form form)
        {
            _context.forms.Add(form);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateFormAsync(Form form)
        {
            _context.Entry(form).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteFormAsync(Form form)
        {
            _context.forms.Remove(form);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<FormField>> GetAllFormFieldsAsync()
        {
            return await _context.formfields.ToListAsync();
        }

        public async Task<IEnumerable<FormRecord>> GetAllFormRecordsAsync()
        {
            return await _context.formrecords.ToListAsync();
        }
        
        public async Task<FormRecord> GetFormRecordByIdAsync(Guid formId, Guid recordId)
        {
            return await _context.formrecords
                .FirstOrDefaultAsync(fr => fr.FormId == formId && fr.Id == recordId);
        }
        
        
        public async Task<FormRecord> GetFormRecordByFormIdAsync(Guid formId)
        {
            return await _context.formrecords
                .FirstOrDefaultAsync(fr => fr.FormId == formId);
        }
        

        public async Task AddFormRecordAsync(FormRecord formRecord)
        {
            _context.formrecords.Add(formRecord);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateFormRecordAsync(FormRecord formRecord)
        {
            _context.Entry(formRecord).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteFormRecordAsync(FormRecord formRecord)
        {
            _context.formrecords.Remove(formRecord);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<FormField>> GetFormFieldsByFormIdAsync(Guid formId)
        {
            return await _context.formfields
                                 .Where(f => f.PageId == formId)
                                 .ToListAsync();
        }
        public async Task<Form> GetFormWithFieldsAsync(Guid formId)
        {
            return await _context.forms
                .Include(f => f.Pages)
                    .ThenInclude(p => p.FormFields)
                .FirstOrDefaultAsync(f => f.Id == formId);
        }
    }
}




/*
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FormAPI.Context;
using FormAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace FormAPI.Repositories
{
    public class FormRepository : IFormRepository
    {
        private readonly ApplicationDbContext _context;

        public FormRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Form>> GetAllFormsAsync()
        {
            try
            {
                return await _context.forms.Include(f => f.Pages)
                                           .ThenInclude(p => p.FormFields)
                                           .ToListAsync();
            }
            catch (Exception ex)
            {
                // Error handling
                throw; // Re-throw the exception
            }
        }

        public async Task<Form> GetFormByIdAsync(Guid id)
        {
            if (id == Guid.Empty)
            {
                throw new ArgumentException("Invalid form ID.", nameof(id));
            }

            try
            {
                return await _context.forms.Include(f => f.Pages)
                                           .ThenInclude(p => p.FormFields)
                                           .FirstOrDefaultAsync(f => f.Id == id);
            }
            catch (Exception ex)
            {
                // Error handling
                throw; // Re-throw the exception
            }
        }

        public async Task<Form> CreateFormAsync(Form form)
        {
            if (form == null)
            {
                throw new ArgumentNullException(nameof(form), "Form cannot be null.");
            }

            try
            {
                _context.forms.Add(form);
                await _context.SaveChangesAsync();
                return form;
            }
            catch (Exception ex)
            {
                // Error handling
                throw; // Re-throw the exception
            }
        }

        public async Task<Form> UpdateFormAsync(Form form)
        {
            if (form.Id == Guid.Empty)
            {
                throw new ArgumentException("Form Id must be provided.", nameof(form.Id));
            }

            try
            {
                _context.Entry(form).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                return form;
            }
            catch (Exception ex)
            {
                // Error handling
                throw;
            }
        }

        public async Task DeleteFormAsync(Guid id)
        {
            try
            {
                var form = await _context.forms.FindAsync(id);
                if (form != null)
                {
                    _context.forms.Remove(form);
                    await _context.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                // Error handling
                throw;
            }
        }

        public async Task<IEnumerable<FormField>> GetAllFormFieldsAsync()
        {
            try
            {
                return await _context.formfields.ToListAsync();
            }
            catch (Exception ex)
            {
                // Error handling
                throw; // Re-throw the exception
            }
        }

        public async Task<FormField> CreateFormFieldAsync(FormField formField)
        {
            if (formField == null)
            {
                throw new ArgumentNullException(nameof(formField), "Form field cannot be null.");
            }

            try
            {
                _context.formfields.Add(formField);
                await _context.SaveChangesAsync();
                return formField;
            }
            catch (Exception ex)
            {
                // Error handling
                throw;
            }
        }

        public async Task<bool> DeleteFormFieldAsync(Guid id)
        {
            try
            {
                var formField = await _context.formfields.FindAsync(id);
                if (formField != null)
                {
                    _context.formfields.Remove(formField);
                    await _context.SaveChangesAsync();
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                // Error handling
                throw;
            }
        }

        public async Task<IEnumerable<FormRecord>> GetAllFormRecordsAsync()
        {
            try
            {
                return await _context.formrecords.ToListAsync();
            }
            catch (Exception ex)
            {
                // Error handling
                throw; // Re-throw the exception
            }
        }

        public async Task<FormRecord> GetFormRecordByIdAsync(Guid id)
        {
            if (id == Guid.Empty)
            {
                throw new ArgumentException("Invalid form record ID.", nameof(id));
            }

            try
            {
                return await _context.formrecords.FindAsync(id);
            }
            catch (Exception ex)
            {
                // Error handling
                throw; // Re-throw the exception
            }
        }

        public async Task<FormRecord> CreateFormRecordAsync(FormRecord formRecord)
        {
            if (formRecord == null)
            {
                throw new ArgumentNullException(nameof(formRecord), "Form record cannot be null.");
            }

            try
            {
                _context.formrecords.Add(formRecord);
                await _context.SaveChangesAsync();
                return formRecord;
            }
            catch (Exception ex)
            {
                // Error handling
                throw; // Re-throw the exception
            }
        }

        public async Task<FormRecord> UpdateFormRecordAsync(FormRecord formRecord)
        {
            if (formRecord.Id == Guid.Empty)
            {
                throw new ArgumentException("Form record Id must be provided.", nameof(formRecord.Id));
            }

            try
            {
                _context.Entry(formRecord).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                return formRecord;
            }
            catch (Exception ex)
            {
                // Error handling
                throw; // Re-throw the exception
            }
        }

        public async Task<bool> DeleteFormRecordAsync(Guid id)
        {
            try
            {
                var formRecord = await _context.formrecords.FindAsync(id);
                if (formRecord != null)
                {
                    _context.formrecords.Remove(formRecord);
                    await _context.SaveChangesAsync();
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                // Error handling
                throw; // Re-throw the exception
            }
        }

        public async Task<IEnumerable<FormRecord>> GetFormRecordsByFormIdAsync(Guid formId)
        {
            if (formId == Guid.Empty)
            {
                throw new ArgumentException("Invalid form ID.", nameof(formId));
            }

            try
            {
                return await _context.formrecords.Where(fr => fr.FormId == formId).ToListAsync();
            }
            catch (Exception ex)
            {
                // Error handling
                throw;
            }
        }

        public async Task<FormRecord> CreateFormRecordForFormAsync(Guid formId, FormRecord formRecord)
        {
            if (formRecord == null)
            {
                throw new ArgumentNullException(nameof(formRecord), "Form record cannot be null.");
            }

            formRecord.FormId = formId;

            try
            {
                _context.formrecords.Add(formRecord);
                await _context.SaveChangesAsync();
                return formRecord;
            }
            catch (Exception ex)
            {
                // Error handling
                throw;
            }
        }

        public async Task<FormRecord> UpdateFormRecordForFormAsync(Guid formId, FormRecord formRecord)
        {
            if (formRecord == null)
            {
                throw new ArgumentNullException(nameof(formRecord), "Form record cannot be null.");
            }

            if (formRecord.Id == Guid.Empty)
            {
                throw new ArgumentException("Form record Id must be provided.", nameof(formRecord.Id));
            }

            formRecord.FormId = formId;

            try
            {
                _context.Entry(formRecord).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                return formRecord;
            }
            catch (Exception ex)
            {
                // Error handling
                throw;
            }
        }

        public async Task<bool> DeleteFormRecordForFormAsync(Guid formId, Guid recordId)
        {
            if (formId == Guid.Empty || recordId == Guid.Empty)
            {
                throw new ArgumentException("Invalid form or record ID.");
            }

            try
            {
                var formRecord = await _context.formrecords.FirstOrDefaultAsync(fr => fr.Id == recordId && fr.FormId == formId);
                if (formRecord != null)
                {
                    _context.formrecords.Remove(formRecord);
                    await _context.SaveChangesAsync();
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                // Error handling
                throw;
            }
        }
    }
}

*/








/*
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FormAPI.Context;
using FormAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace FormAPI.Repositories
{
    public class FormRepository : IFormRepository
    {
        private readonly ApplicationDbContext _context;

        public FormRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Form>> GetAllFormsAsync()
        {
            try
            {
                return await _context.forms.ToListAsync();
            }
            catch (Exception ex)
            {
                // Error handling
                // Example: Log.Error(ex, "Error occurred while retrieving all forms");
                throw; // Re-throw the exception
            }
        }

        public async Task<Form> GetFormByIdAsync(Guid id)
        {
            // Parameter validation
            if ( id == Guid.Empty)
            {
                throw new ArgumentException("Invalid form ID.", nameof(id));
            }

            try
            {
                return await _context.forms.FindAsync(id);
            }
            catch (Exception ex)
            {
                // Error handling
                // Example: Log.Error(ex, $"Error occurred while retrieving form with ID {id}");
                throw; // Re-throw the exception
            }
        }

        public async Task<Form> CreateFormAsync(Form form)
        {
            // Parameter validation
            if (form == null)
            {
                throw new ArgumentNullException(nameof(form), "Form cannot be null.");
            }

            try
            {
                _context.forms.Add(form);
                await _context.SaveChangesAsync();
                return form;
            }
            catch (Exception ex)
            {
                // Error handling
                // Example: Log.Error(ex, "Error occurred while creating form");
                throw; // Re-throw the exception
            }
        }

        public async Task<Form> UpdateFormAsync(Form form)
        {
            
            if (form.Id == Guid.Empty) // Check for empty Guid
            {
                throw new ArgumentException("Form Id must be provided.", nameof(form.Id));
            }

            try
            {
                _context.Entry(form).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                return form;
            }
            catch (Exception ex)
            {
                // Error handling
                // Example: Log.Error(ex, "Error occurred while updating form");
                throw;
            }
        }

       
        public async Task DeleteFormAsync(Guid id) // Changed parameter type to Guid
        {
            // No parameter validation needed for Guid (cannot be less than or equal to zero)
            try
            {
                var form = await _context.forms.FindAsync(id);
                if (form != null)
                {
                    _context.forms.Remove(form);
                    await _context.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                // Error handling
                // Example: Log.Error(ex, $"Error occurred while deleting form with ID {id}");
                throw;
            }
        }


        public async Task<IEnumerable<FormField>> GetAllFormFieldsAsync()
        {
            try
            {
                return await _context.formfields.ToListAsync();
            }
            catch (Exception ex)
            {
                // Error handling
                // Example: Log.Error(ex, "Error occurred while retrieving all form fields");
                throw; // Re-throw the exception
            }
        }

        
        public async Task<FormField> CreateFormFieldAsync(FormField formField)
        {
            // Parameter validation remains the same

            try
            {
                _context.formfields.Add(formField);
                await _context.SaveChangesAsync();
                return formField;
            }
            catch (Exception ex)
            {
                // Error handling
                // Example: Log.Error(ex, "Error occurred while creating form field");
                throw;
            }
        }


       
        public async Task<bool> DeleteFormFieldAsync(Guid id) // Changed parameter type to Guid
        {
            // No parameter validation needed for Guid (cannot be less than or equal to zero)
            try
            {
                var formField = await _context.formfields.FindAsync(id);
                if (formField != null)
                {
                    _context.formfields.Remove(formField);
                    await _context.SaveChangesAsync();
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                // Error handling
                // Example: Log.Error(ex, $"Error occurred while deleting form field with ID {id}");
                throw;
            }
        }


        public async Task<IEnumerable<FormRecord>> GetAllFormRecordsAsync()
        {
            try
            {
                return await _context.formrecords.ToListAsync();
            }
            catch (Exception ex)
            {
                // Error handling
                // Example: Log.Error(ex, "Error occurred while retrieving all form records");
                throw; // Re-throw the exception
            }
        }

        public async Task<FormRecord> GetFormRecordByIdAsync(Guid id)
        {
            // Parameter validation
            if (id == Guid.Empty)
            {
                throw new ArgumentException("Invalid form record ID.", nameof(id));
            }

            try
            {
                return await _context.formrecords.FindAsync(id);
            }
            catch (Exception ex)
            {
                // Error handling
                // Example: Log.Error(ex, $"Error occurred while retrieving form record with ID {id}");
                throw; // Re-throw the exception
            }
        }

        public async Task<FormRecord> CreateFormRecordAsync(FormRecord formRecord)
        {
           

            try
            {
                _context.formrecords.Add(formRecord);
                await _context.SaveChangesAsync();
                return formRecord;
            }
            catch (Exception ex)
            {
                // Error handling
                // Example: Log.Error(ex, "Error occurred while creating form record");
                throw; // Re-throw the exception
            }
        }

        public async Task<FormRecord> UpdateFormRecordAsync(FormRecord formRecord)
        {
           

            if (formRecord.Id == Guid.Empty)
            {
                throw new ArgumentException("Form record Id must be provided.", nameof(formRecord.Id));
            }

            try
            {
                _context.Entry(formRecord).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                return formRecord;
            }
            catch (Exception ex)
            {
                // Error handling
                // Example: Log.Error(ex, "Error occurred while updating form record");
                throw; // Re-throw the exception
            }
        }

        public async Task<bool> DeleteFormRecordAsync(Guid id)
        {
           

            try
            {
                var formRecord = await _context.formrecords.FindAsync(id);
                if (formRecord != null)
                {
                    _context.formrecords.Remove(formRecord);
                    await _context.SaveChangesAsync();
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                // Error handling
                // Example: Log.Error(ex, $"Error occurred while deleting form record with ID {id}");
                throw; // Re-throw the exception
            }
        }



        public async Task<IEnumerable<FormRecord>> GetFormRecordsByFormIdAsync(Guid formId) // Changed parameter type to Guid
        {
            if (formId == Guid.Empty) // Use direct comparison for Guid
            {
                throw new ArgumentException("Invalid form ID.", nameof(formId));
            }

            try
            {
                return await _context.formrecords.Where(fr => fr.FormId == formId).ToListAsync();
            }
            catch (Exception ex)
            {
                // Error handling
                // Example: Log.Error(ex, "Error occurred while retrieving form records by form ID");
                throw;
            }
        }


        public async Task<FormRecord> CreateFormRecordForFormAsync(Guid formId, FormRecord formRecord) // All parameters are now Guid
        {
            if (formRecord == null)
            {
                throw new ArgumentNullException(nameof(formRecord), "Form record cannot be null.");
            }

            formRecord.FormId = formId; // Set the formId

            try
            {
                _context.formrecords.Add(formRecord);
                await _context.SaveChangesAsync();
                return formRecord;
            }
            catch (Exception ex)
            {
                // Error handling
                // Example: Log.Error(ex, "Error occurred while creating form record for form");
                throw;
            }
        }


        public async Task<FormRecord> UpdateFormRecordForFormAsync(Guid formId, FormRecord formRecord) // Changed parameter type to Guid
        {
            if (formRecord == null)
            {
                throw new ArgumentNullException(nameof(formRecord), "Form record cannot be null.");
            }

            if (formRecord.Id == Guid.Empty) // Check for empty Guid
            {
                throw new ArgumentException("Form record Id must be provided.", nameof(formRecord.Id));
            }

            formRecord.FormId = formId; // Ensure the formId is set

            try
            {
                _context.Entry(formRecord).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                return formRecord;
            }
            catch (Exception ex)
            {
                // Error handling
                // Example: Log.Error(ex, "Error occurred while updating form record for form");
                throw;
            }
        }


        public async Task<bool> DeleteFormRecordForFormAsync(Guid formId, Guid recordId) // Changed parameter types to Guid
        {
            if (formId == Guid.Empty || recordId == Guid.Empty) // Check for empty Guids
            {
                throw new ArgumentException("Invalid form or record ID.");
            }

            try
            {
                var formRecord = await _context.formrecords.FirstOrDefaultAsync(fr => fr.Id == recordId && fr.FormId == formId);
                if (formRecord != null)
                {
                    _context.formrecords.Remove(formRecord);
                    await _context.SaveChangesAsync();
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                // Error handling
                // Example: Log.Error(ex, "Error occurred while deleting form record for form");
                throw;
            }
        }

    }
}

*/







//using FormAPI.Models;
//using System.Collections.Generic;
//using Npgsql;
//using Dapper;



//namespace FormAPI.Repositories
//{
//    public class FormRepository : IFormRepository
//    {
//        private readonly string _connectionString;

//        public FormRepository(string connectionString)
//        {
//            _connectionString = connectionString;
//        }


//        public async Task<IEnumerable<Form>> GetAllFormsAsync()
//        {
//            try
//            {
//                using var connection = new NpgsqlConnection(_connectionString);
//                await connection.OpenAsync();

//                const string query = "SELECT * FROM forms";
//                return await connection.QueryAsync<Form>(query);
//            }
//            catch (Exception ex)
//            {
//                // Error handling
//                // Example: Log.Error(ex, "Error occurred while retrieving all forms");
//                throw; // Re-throw the exception
//            }
//        }

//        public async Task<Form> GetFormByIdAsync(int id)
//        {
//            // Parameter validation
//            if (id <= 0)
//            {
//                throw new ArgumentException("Invalid form ID.", nameof(id));
//            }

//            try
//            {
//                using var connection = new NpgsqlConnection(_connectionString);
//                await connection.OpenAsync();

//                const string query = "SELECT * FROM forms WHERE \"Id\" = @Id";
//                return await connection.QueryFirstOrDefaultAsync<Form>(query, new { Id = id });
//            }
//            catch (Exception ex)
//            {
//                // Error handling
//                // Example: Log.Error(ex, $"Error occurred while retrieving form with ID {id}");
//                throw; // Re-throw the exception
//            }
//        }

//        public async Task<Form> CreateFormAsync(Form form)
//        {
//            // Parameter validation
//            if (form == null)
//            {
//                throw new ArgumentNullException(nameof(form), "Form cannot be null.");
//            }

//            try
//            {
//                using var connection = new NpgsqlConnection(_connectionString);
//                await connection.OpenAsync();

//                const string query = @"
//            INSERT INTO forms (""Name"", ""Description"") 
//            VALUES (@Name, @Description) 
//            RETURNING *";

//                return await connection.QueryFirstOrDefaultAsync<Form>(query, form);
//            }
//            catch (Exception ex)
//            {
//                // Error handling
//                // Example: Log.Error(ex, "Error occurred while creating form");
//                throw; // Re-throw the exception
//            }
//        }


//        public async Task<Form> UpdateFormAsync(Form form)
//        {
//            // Parameter validation
//            if (form == null)
//            {
//                throw new ArgumentNullException(nameof(form), "Form cannot be null.");
//            }

//            if (form.Id == 0)
//            {
//                throw new ArgumentException("Form Id must be provided.", nameof(form.Id));
//            }

//            try
//            {
//                using var connection = new NpgsqlConnection(_connectionString);
//                await connection.OpenAsync();

//                const string query = @"
//            UPDATE forms 
//            SET ""Name"" = @Name, 
//                ""Description"" = @Description
//            WHERE ""Id"" = @Id
//            RETURNING *";

//                return await connection.QueryFirstOrDefaultAsync<Form>(query, form);
//            }
//            catch (Exception ex)
//            {
//                // Error handling
//                // Example: Log.Error(ex, "Error occurred while updating form");
//                throw; // Re-throw the exception
//            }
//        }


//        public async Task DeleteFormAsync(int id)
//        {
//            // Parameter validation
//            if (id <= 0)
//            {
//                throw new ArgumentException("Invalid form ID.", nameof(id));
//            }

//            try
//            {
//                using var connection = new NpgsqlConnection(_connectionString);
//                await connection.OpenAsync();

//                const string query = "DELETE FROM forms WHERE \"Id\" = @Id";
//                await connection.ExecuteAsync(query, new { Id = id });
//            }
//            catch (Exception ex)
//            {
//                // Error handling
//                // Example: Log.Error(ex, $"Error occurred while deleting form with ID {id}");
//                throw; // Re-throw the exception
//            }
//        }

//        public async Task<IEnumerable<FormField>> GetAllFormFieldsAsync()
//        {
//            try
//            {
//                using var connection = new NpgsqlConnection(_connectionString);
//                await connection.OpenAsync();

//                const string query = "SELECT * FROM formfields";
//                return await connection.QueryAsync<FormField>(query);
//            }
//            catch (Exception ex)
//            {
//                // Error handling
//                // Example: Log.Error(ex, "Error occurred while retrieving all form fields");
//                throw; // Re-throw the exception
//            }
//        }


//        public async Task<FormField> CreateFormFieldAsync(FormField formField)
//        {
//            // Parameter validation
//            if (formField == null)
//            {
//                throw new ArgumentNullException(nameof(formField), "Form field cannot be null.");
//            }

//            try
//            {
//                using var connection = new NpgsqlConnection(_connectionString);
//                await connection.OpenAsync();

//                const string query = @"
//            INSERT INTO formfields (""Name"", ""Required"", ""Attributes"", ""Kind"", ""FieldType"",""Rules"") 
//            VALUES (@Name, @Required, @Attributes, @Kind, @FieldType, @Rules) 
//            RETURNING *";

//                return await connection.QueryFirstOrDefaultAsync<FormField>(query, formField);
//            }
//            catch (Exception ex)
//            {
//                // Error handling
//                // Example: Log.Error(ex, "Error occurred while creating form field");
//                throw; // Rethrow the exception
//            }
//        }

//        public async Task<bool> DeleteFormFieldAsync(int id)
//        {
//            // Parameter validation
//            if (id <= 0)
//            {
//                throw new ArgumentException("Invalid form field ID.", nameof(id));
//            }

//            try
//            {
//                using var connection = new NpgsqlConnection(_connectionString);
//                await connection.OpenAsync();

//                const string query = "DELETE FROM formfields WHERE \"Id\" = @Id";
//                var rowsAffected = await connection.ExecuteAsync(query, new { Id = id });

//                return rowsAffected > 0;
//            }
//            catch (Exception ex)
//            {
//                // Error handling
//                // Example: Log.Error(ex, $"Error occurred while deleting form field with ID {id}");
//                throw; // Re-throw the exception
//            }
//        }

//        public async Task<IEnumerable<FormRecord>> GetAllFormRecordsAsync()
//        {
//            try
//            {
//                using var connection = new NpgsqlConnection(_connectionString);
//                await connection.OpenAsync();

//                const string query = "SELECT * FROM formrecords";
//                return await connection.QueryAsync<FormRecord>(query);
//            }
//            catch (Exception ex)
//            {
//                // Error handling
//                // Example: Log.Error(ex, "Error occurred while retrieving all form records");
//                throw; // Re-throw the exception
//            }
//        }

//        public async Task<FormRecord> GetFormRecordByIdAsync(int id)
//        {
//            // Parameter validation
//            if (id <= 0)
//            {
//                throw new ArgumentException("Invalid form record ID.", nameof(id));
//            }

//            try
//            {
//                using var connection = new NpgsqlConnection(_connectionString);
//                await connection.OpenAsync();

//                const string query = "SELECT * FROM formrecords WHERE \"Id\" = @Id";
//                return await connection.QueryFirstOrDefaultAsync<FormRecord>(query, new { Id = id });
//            }
//            catch (Exception ex)
//            {
//                // Error handling
//                // Example: Log.Error(ex, $"Error occurred while retrieving form record with ID {id}");
//                throw; // Re-throw the exception
//            }
//        }

//        public async Task<FormRecord> CreateFormRecordAsync(FormRecord formRecord)
//        {
//            // Parameter validation
//            if (formRecord == null)
//            {
//                throw new ArgumentNullException(nameof(formRecord), "Form record cannot be null.");
//            }

//            try
//            {
//                using var connection = new NpgsqlConnection(_connectionString);
//                await connection.OpenAsync();

//                const string query = @"
//            INSERT INTO formrecords (""FirstName"", ""SecondName"", ""LastName"", ""Birthdate"", ""Gender"", ""LanguageCode"", ""Nationality"", ""PhoneNumber"", ""Email"", ""Arrival"", ""Departure"", ""Address"", ""Zip"", ""City"", ""Country"", ""Kind"", ""FieldType"", ""Attributes"") 
//            VALUES (@FirstName, @SecondName, @LastName, @Birthdate, @Gender, @LanguageCode, @Nationality, @PhoneNumber, @Email, @Arrival, @Departure, @Address, @Zip, @City, @Country, @Kind, @FieldType, @Attributes) 
//            RETURNING *";

//                // Wrap the formRecord object in the necessary field
//                var parameters = new
//                {
//                    FirstName = formRecord.FirstName,
//                    SecondName = formRecord.SecondName,
//                    LastName = formRecord.LastName,
//                    Birthdate = formRecord.Birthdate,
//                    Gender = formRecord.Gender,
//                    LanguageCode = formRecord.LanguageCode,
//                    Nationality = formRecord.Nationality,
//                    PhoneNumber = formRecord.PhoneNumber,
//                    Email = formRecord.Email,
//                    Arrival = formRecord.Arrival,
//                    Departure = formRecord.Departure,
//                    Address = formRecord.Address,
//                    Zip = formRecord.Zip,
//                    City = formRecord.City,
//                    Country = formRecord.Country,
//                    Kind = formRecord.Kind,
//                    FieldType = formRecord.FieldType,
//                    Attributes = formRecord.Attributes
//                };

//                return await connection.QueryFirstOrDefaultAsync<FormRecord>(query, parameters);
//            }
//            catch (Exception ex)
//            {
//                // Error handling
//                // Example: Log.Error(ex, "Error occurred while creating form record");
//                throw; // Re-throw the exception
//            }
//        }


//        public async Task<FormRecord> UpdateFormRecordAsync(FormRecord formRecord)
//        {
//            // Parameter validation
//            if (formRecord == null)
//            {
//                throw new ArgumentNullException(nameof(formRecord), "Form record cannot be null.");
//            }

//            if (formRecord.Id == 0)
//            {
//                throw new ArgumentException("Form record Id must be provided.", nameof(formRecord.Id));
//            }

//            try
//            {
//                using var connection = new NpgsqlConnection(_connectionString);
//                await connection.OpenAsync();

//                const string query = @"
//            UPDATE formrecords 
//            SET ""FirstName"" = @FirstName, 
//                ""SecondName"" = @SecondName,
//                ""LastName"" = @LastName,
//                ""Birthdate"" = @Birthdate,
//                ""Gender"" = @Gender,
//                ""LanguageCode"" = @LanguageCode,
//                ""Nationality"" = @Nationality,
//                ""PhoneNumber"" = @PhoneNumber,
//                ""Email"" = @Email,
//                ""Arrival"" = @Arrival,
//                ""Departure"" = @Departure,
//                ""Address"" = @Address,
//                ""Zip"" = @Zip,
//                ""City"" = @City,
//                ""Country"" = @Country,
//                ""Kind"" = @Kind,
//                ""FieldType"" = @FieldType,
//                ""Attributes"" = @Attributes
//            WHERE ""Id"" = @Id
//            RETURNING *";

//                return await connection.QueryFirstOrDefaultAsync<FormRecord>(query, formRecord);
//            }
//            catch (Exception ex)
//            {
//                // Error handling
//                // Example: Log.Error(ex, "Error occurred while updating form record");
//                throw; // Re-throw the exception
//            }
//        }


//        public async Task<bool> DeleteFormRecordAsync(int id)
//        {
//            // Parameter validation
//            if (id <= 0)
//            {
//                throw new ArgumentException("Invalid form record ID.", nameof(id));
//            }

//            try
//            {
//                using var connection = new NpgsqlConnection(_connectionString);
//                await connection.OpenAsync();

//                const string query = @"DELETE FROM formrecords WHERE ""Id"" = @Id";

//                var rowsAffected = await connection.ExecuteAsync(query, new { Id = id });

//                return rowsAffected > 0;
//            }
//            catch (Exception ex)
//            {
//                // Error handling
//                // Example: Log.Error(ex, $"Error occurred while deleting form record with ID {id}");
//                throw; // Re-throw the exception
//            }
//        }
//    }
//}





































//using FormAPI.Models;
//using System.Collections.Generic;
//using Npgsql;
//using Dapper;



//namespace FormAPI.Repositories
//{
//    public class FormRepository : IFormRepository
//    {
//        private readonly string _connectionString;

//        public FormRepository(string connectionString)
//        {
//            _connectionString = connectionString;
//        }


//        public async Task<IEnumerable<Form>> GetAllFormsAsync()
//        {
//            using var connection = new NpgsqlConnection(_connectionString);
//            await connection.OpenAsync();

//            return await connection.QueryAsync<Form>("SELECT * FROM forms");
//        }
//        public async Task<Form> GetFormByIdAsync(int id)
//        {
//            using var connection = new NpgsqlConnection(_connectionString);
//            await connection.OpenAsync();

//            const string query = @"
//        SELECT * 
//        FROM forms 
//        WHERE ""Id"" = @Id";

//            return await connection.QueryFirstOrDefaultAsync<Form>(query, new { Id = id });
//        }


//        public async Task<Form> CreateFormAsync(Form form)
//        {
//            using var connection = new NpgsqlConnection(_connectionString);
//            await connection.OpenAsync();

//            const string query = @"
//                INSERT INTO forms (""Name"", ""Description"") 
//                VALUES (@Name, @Description) 
//                RETURNING *";

//            return await connection.QueryFirstOrDefaultAsync<Form>(query, form);
//        }

//        public async Task<Form> UpdateFormAsync(Form form)
//        {
//            using var connection = new NpgsqlConnection(_connectionString);
//            await connection.OpenAsync();

//            const string query = @"
//                UPDATE forms 
//                SET ""Name"" = @Name, 
//                    ""Description"" = @Description
//                WHERE ""Id"" = @Id
//                RETURNING *";

//            return await connection.QueryFirstOrDefaultAsync<Form>(query, form);
//        }

//        public async Task<bool> DeleteFormAsync(int id)
//        {
//            using var connection = new NpgsqlConnection(_connectionString);
//            await connection.OpenAsync();

//            const string query = @"DELETE FROM forms WHERE Id = @Id";

//            var rowsAffected = await connection.ExecuteAsync(query, new { Id = id });

//            return rowsAffected > 0;
//        }

//        public async Task<IEnumerable<FormField>> GetAllFormFields()
//        {
//            using var connection = new NpgsqlConnection(_connectionString);
//            await connection.OpenAsync();

//            return await connection.QueryAsync<FormField>("SELECT * FROM formfields");
//        }

//        public async Task<FormField> CreateFormField(FormField formField)
//        {
//            using var connection = new NpgsqlConnection(_connectionString);
//            await connection.OpenAsync();

//            const string query = @"
//                INSERT INTO formfields (Name, Id, Required, Attributes, Kind, FieldType, Rules) 
//                VALUES (@Name, @Id, @Required, @Attributes, @Kind, @FieldType, @Rules) 
//                RETURNING *";

//            return await connection.QueryFirstOrDefaultAsync<FormField>(query, formField);


//        }
//        public async Task<IEnumerable<FormRecord>> GetAllFormRecordsAsync()
//        {
//            using var connection = new NpgsqlConnection(_connectionString);
//            await connection.OpenAsync();

//            return await connection.QueryAsync<FormRecord>("SELECT * FROM formrecords");
//        }
//        public async Task<FormRecord> GetByIdAsync(int id)
//        {
//            using var connection = new NpgsqlConnection(_connectionString);
//            await connection.OpenAsync();

//            return await connection.QueryFirstOrDefaultAsync<FormRecord>(
//                "SELECT * FROM formrecords WHERE \"Id\" = @Id", new { Id = id });
//        }
//        public async Task<FormRecord> CreateFormRecordAsync(FormRecord formRecord)
//        {
//            using var connection = new NpgsqlConnection(_connectionString);
//            await connection.OpenAsync();

//            const string query = @"
//             INSERT INTO formrecords (""FirstName"", ""SecondName"", ""LastName"", ""Birthdate"", ""Gender"", ""LanguageCode"", ""Nationality"", ""PhoneNumber"", ""Email"", ""Arrival"", ""Departure"", ""Address"", ""Zip"", ""City"", ""Country"", ""Kind"", ""FieldType"", ""Attributes"") 
//             VALUES (@FirstName, @SecondName, @LastName, @Birthdate, @Gender, @LanguageCode, @Nationality, @PhoneNumber, @Email, @Arrival, @Departure, @Address, @Zip, @City, @Country, @Kind, @FieldType, @Attributes) 
//             RETURNING *";



//            // Wrap the formRecord object in the necessary field
//            var parameters = new
//            {
//                formRecord = formRecord
//            };

//            return await connection.QueryFirstOrDefaultAsync<FormRecord>(query, formRecord);
//        }

//        public async Task<FormRecord> UpdateFormRecordAsync(FormRecord formRecord)
//        {
//            using var connection = new NpgsqlConnection(_connectionString);
//            await connection.OpenAsync();

//            const string query = @"
//        UPDATE formrecords 
//        SET ""FirstName"" = @FirstName, 
//            ""SecondName"" = @SecondName,
//            ""LastName"" = @LastName,
//            ""Birthdate"" = @Birthdate,
//            ""Gender"" = @Gender,
//            ""LanguageCode"" = @LanguageCode,
//            ""Nationality"" = @Nationality,
//            ""PhoneNumber"" = @PhoneNumber,
//            ""Email"" = @Email,
//            ""Arrival"" = @Arrival,
//            ""Departure"" = @Departure,
//            ""Address"" = @Address,
//            ""Zip"" = @Zip,
//            ""City"" = @City,
//            ""Country"" = @Country,
//            ""Kind"" = @Kind,
//            ""FieldType"" = @FieldType,
//            ""Attributes"" = @Attributes
//        WHERE ""Id"" = @Id
//        RETURNING *";

//            return await connection.QueryFirstOrDefaultAsync<FormRecord>(query, formRecord);
//        }

//        public async Task<bool> DeleteFormRecordAsync(int id)
//        {
//            using var connection = new NpgsqlConnection(_connectionString);
//            await connection.OpenAsync();

//            const string query = @"DELETE FROM formrecords WHERE ""Id"" = @Id";

//            var rowsAffected = await connection.ExecuteAsync(query, new { Id = id });

//            return rowsAffected > 0;
//        }

//    }
//}
