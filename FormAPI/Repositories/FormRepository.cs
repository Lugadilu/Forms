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

            return await connection.QueryAsync<FormField>("SELECT * FROM formfields");
        }

        public async Task<FormField> CreateFormField(FormField formField)
        {
            using var connection = new NpgsqlConnection(_connectionString);
            await connection.OpenAsync();

            const string query = @"
                INSERT INTO formfields (Name, Id, Required, Attributes, Kind, FieldType, Rules) 
                VALUES (@Name, @Id, @Required, @Attributes, @Kind, @FieldType, @Rules) 
                RETURNING *";

            return await connection.QueryFirstOrDefaultAsync<FormField>(query, formField);


        }
        public async Task<IEnumerable<FormRecord>> GetAllFormRecordsAsync()
        {
            using var connection = new NpgsqlConnection(_connectionString);
            await connection.OpenAsync();

            return await connection.QueryAsync<FormRecord>("SELECT * FROM formrecords");
        }
        public async Task<FormRecord> GetByIdAsync(int id)
        {
            using var connection = new NpgsqlConnection(_connectionString);
            await connection.OpenAsync();

            return await connection.QueryFirstOrDefaultAsync<FormRecord>(
                "SELECT * FROM formrecords WHERE \"Id\" = @Id", new { Id = id });
        }
        public async Task<FormRecord> CreateFormRecordAsync(FormRecord formRecord)
        {
            using var connection = new NpgsqlConnection(_connectionString);
            await connection.OpenAsync();

            const string query = @"
             INSERT INTO formrecords (""FirstName"", ""SecondName"", ""LastName"", ""Birthdate"", ""Gender"", ""LanguageCode"", ""Nationality"", ""PhoneNumber"", ""Email"", ""Arrival"", ""Departure"", ""Address"", ""Zip"", ""City"", ""Country"", ""Kind"", ""FieldType"", ""Attributes"") 
             VALUES (@FirstName, @SecondName, @LastName, @Birthdate, @Gender, @LanguageCode, @Nationality, @PhoneNumber, @Email, @Arrival, @Departure, @Address, @Zip, @City, @Country, @Kind, @FieldType, @Attributes) 
             RETURNING *";



            // Wrap the formRecord object in the necessary field
            var parameters = new
            {
                formRecord = formRecord
            };

            return await connection.QueryFirstOrDefaultAsync<FormRecord>(query, formRecord);
        }

        public async Task<FormRecord> UpdateFormRecordAsync(FormRecord formRecord)
        {
            using var connection = new NpgsqlConnection(_connectionString);
            await connection.OpenAsync();

            const string query = @"
        UPDATE formrecords 
        SET ""FirstName"" = @FirstName, 
            ""SecondName"" = @SecondName,
            ""LastName"" = @LastName,
            ""Birthdate"" = @Birthdate,
            ""Gender"" = @Gender,
            ""LanguageCode"" = @LanguageCode,
            ""Nationality"" = @Nationality,
            ""PhoneNumber"" = @PhoneNumber,
            ""Email"" = @Email,
            ""Arrival"" = @Arrival,
            ""Departure"" = @Departure,
            ""Address"" = @Address,
            ""Zip"" = @Zip,
            ""City"" = @City,
            ""Country"" = @Country,
            ""Kind"" = @Kind,
            ""FieldType"" = @FieldType,
            ""Attributes"" = @Attributes
        WHERE ""Id"" = @Id
        RETURNING *";

            return await connection.QueryFirstOrDefaultAsync<FormRecord>(query, formRecord);
        }

        public async Task<bool> DeleteFormRecordAsync(int id)
        {
            using var connection = new NpgsqlConnection(_connectionString);
            await connection.OpenAsync();

            const string query = @"DELETE FROM formrecords WHERE ""Id"" = @Id";

            var rowsAffected = await connection.ExecuteAsync(query, new { Id = id });

            return rowsAffected > 0;
        }

    }
}
