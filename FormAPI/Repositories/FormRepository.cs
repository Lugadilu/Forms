using FormAPI.Models;
using System.Collections.Generic;
using Npgsql;
using Dapper;



namespace FormAPI.Repositories
{
    public class FormRepository : IFormRepository
    {
        private readonly string _connectionString;

        public FormRepository(string connectionString)
        {
            _connectionString = connectionString;
        }
        /*public List<FormField> GetFormFields()
        {

        }*/
        public async Task<IEnumerable<FormField>> GetAll()
        {
            using var connection = new NpgsqlConnection(_connectionString);
            await connection.OpenAsync();

            return await connection.QueryAsync<FormField>("SELECT * FROM FormFields");
        }
        public async Task<FormField> Create(FormField formField)
        {
            using var connection = new NpgsqlConnection(_connectionString);
            await connection.OpenAsync();

            const string query = @"
                INSERT INTO FormFields (Name, Type, DefaultValue) 
                VALUES (@Name, @Type, @DefaultValue) 
                RETURNING *";

            return await connection.QueryFirstOrDefaultAsync<FormField>(query, formField);
        }
        /* public void CreateFormField(FormField formField)
         {

         }*/
        /* public List<FormPage> GetFormPages()
         {

         }
         public void CreateFormPage(FormPage formPage)
         {

         }
         public List<FormRecord> GetFormRecords()
         {

         }
         public void CreateFormRecord(FormRecord formRecord)
         {

         }
         public List<FormCreate> GetFormCreates()
         {

         }
         public void CreateFormCreate(FormCreate formCreate)
         {

         }*/
    }
}
