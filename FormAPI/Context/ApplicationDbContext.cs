using Microsoft.EntityFrameworkCore;
using FormAPI.Models;

namespace FormAPI.Context
{
    public class ApplicationDbContext : DbContext
    {
        //i added this after database failed to update with error timestamp with time zone literal cannot be generated
        static ApplicationDbContext()
        {
            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
        }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
            //can addd connection string but i choose to add it in appsettings.json
        }
        //making the tables to lowercases solves some error
        public DbSet<FormField>formfields { get; set; }
        public DbSet<FormRecord> formrecords { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {


            //base.OnModelCreating(modelBuilder);

            //seed data
            modelBuilder.Entity<FormField>().HasData(
                new FormField
                {
                    Id = 1,
                    Name = "Field 1",
                    Required = true,
                    Kind = "profile",
                    FieldType = "text",
                    Attributes = new Dictionary<string, string>
                    {
                        { "Attribute1", "Value1" },
                        { "Attribute2", "Value2" }
                        // Add more attributes as needed
                    },
                    Rules = new Dictionary<string, string>
                    {
                        { "MinLength", "5" },
                        { "MaxLength", "20" },
                        { "RegexPattern", "^\\w+$" }
                        // Add more rules as needed
                    }
                },
                new FormField
                {
                    Id = 2,
                    Name = "Field 2",
                    Required = false,
                    Kind = "address",
                    FieldType = "text",
                    Attributes = new Dictionary<string, string>
                    {
                        { "Attribute1", "Value1" },
                        { "Attribute2", "Value2" }
                        // Add more attributes as needed
                    },
                    Rules = new Dictionary<string, string>
                    {
                        { "MinLength", "5" },
                        { "MaxLength", "20" },
                        { "RegexPattern", "^\\w+$" }
                        // Add more rules as needed
                    }
                },
                new FormField
                {
                    Id = 3,
                    Name = "Field 3",
                    Required = false,
                    Kind = "registration",
                    FieldType = "text",
                    Attributes = new Dictionary<string, string>
                    {
                        { "Attribute1", "Value1" },
                        { "Attribute2", "Value2" }
                        // Add more attributes as needed
                    },
                    Rules = new Dictionary<string, string>
                    {
                        { "MinLength", "5" },
                        { "MaxLength", "20" },
                        { "RegexPattern", "^\\w+$" }
                        // Add more rules as needed
                    }
                }
                // Add more FormFields as needed
            );





            modelBuilder.Entity<FormRecord>().HasData(
                new FormRecord
                {
                    Id = 1,
                    FirstName = "John",
                    SecondName = "Doe",
                    LastName = "Smith",
                    Birthdate = new DateTime(1990, 1, 1),
                    Gender = "Male",
                    PhoneNumber = 1234567890,
                    Email = "john@example.com",
                    Address = "123 Main St",
                    Zip = "12345",
                    City = "New oke",
                    Country = "USA",
                    Arrival = DateTime.UtcNow.Date, // Example: Today's date
                    Departure = DateTime.UtcNow.Date.AddDays(9), // Example: 10 days from today
                    FieldType = "text",
                    Kind = "profile",
                    Attributes = new Dictionary<string, string>
                    {
                        { "Attribute1", "Value1" },
                        { "Attribute2", "Value2" }
                        // Add more attributes as needed
                    }
                },
                new FormRecord
                {
                    Id = 2,
                    FirstName = "Jane",
                    SecondName = "Doe",
                    LastName = "Doe",
                    Birthdate = new DateTime(1995, 1, 1),
                    Gender = "Female",
                    PhoneNumber = 0776543210,
                    Email = "jane@example.com",
                    Address = "123 Main St",
                    Zip = "12345",
                    City = "Philadephia",
                    Country = "USA",
                    Arrival = DateTime.UtcNow.Date, // Example: Today's date
                    Departure = DateTime.UtcNow.Date.AddDays(9), // Example: 10 days from today
                    FieldType = "text",
                    Kind = "profile",
                    Attributes = new Dictionary<string, string>
                    {
                        { "Attribute1", "Value1" },
                        { "Attribute2", "Value2" }
                        // Add more attributes as needed
                    }
                }
                // Add more FormRecords as needed
            );

        }
    }
}
