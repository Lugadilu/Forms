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
        public async Task<IEnumerable<FormField>> GetAllFormFields()
        {
            using var connection = new NpgsqlConnection(_connectionString);
            await connection.OpenAsync();

            return await connection.QueryAsync<FormField>("SELECT * FROM FormFields");
        }

        public async Task<FormField> CreateFormField(FormField formField)
        {
            using var connection = new NpgsqlConnection(_connectionString);
            await connection.OpenAsync();

            const string query = @"
        INSERT INTO FormFields (Name, Id, Required, Attributes, Kind, FieldType, Rules) 
        VALUES (@Name, @Id, @Required, @Attributes, @Kind, @FieldType, @Rules) 
        RETURNING *";

            return await connection.QueryFirstOrDefaultAsync<FormField>(query, formField);


        }
        public async Task<IEnumerable<FormRecord>> GetAllFormRecordsAsync()
        {
            using var connection = new NpgsqlConnection(_connectionString);
            await connection.OpenAsync();

            return await connection.QueryAsync<FormRecord>("SELECT * FROM FormRecords");
        }
        public async Task<FormRecord> GetByIdAsync(int id)
        {
            using var connection = new NpgsqlConnection(_connectionString);
            await connection.OpenAsync();

            return await connection.QueryFirstOrDefaultAsync<FormRecord>("SELECT * FROM FormRecords WHERE Id = @Id", new { Id = id });
        }
        public async Task<FormRecord> CreateFormRecordAsync(FormRecord formRecord)
        {
            using var connection = new NpgsqlConnection(_connectionString);
            await connection.OpenAsync();

            const string query = @"
                INSERT INTO FormRecords (FirstName, SecondName, LastName, Birthdate, Gender, LanguageCode, Nationality, PhoneNumber, Email, Arrival, Departure, Address, Zip, City, Country, Kind, FieldType, Attributes) 
                VALUES (@FirstName, @SecondName, @LastName, @Birthdate, @Gender, @LanguageCode, @Nationality, @PhoneNumber, @Email, @Arrival, @Departure, @Address, @Zip, @City, @Country, @Kind, @FieldType, @Attributes) 
                RETURNING *";

            return await connection.QueryFirstOrDefaultAsync<FormRecord>(query, formRecord);
        }

       
    }
}
