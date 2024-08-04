using FormAPI.Models;
using Microsoft.EntityFrameworkCore;
using System;

namespace FormAPI.Context
{
    public class ApplicationDbContext : DbContext
    {
        // Added to address the timestamp issue
        static ApplicationDbContext()
        {
            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
        }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<FormField> formfields { get; set; }
        public DbSet<FormRecord> formrecords { get; set; }
        public DbSet<Form> forms { get; set; }
        public DbSet<Page> pages { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Form>()
            .HasMany(f => f.Pages)
            .WithOne(p => p.Form)
            .HasForeignKey(p => p.FormId);

            modelBuilder.Entity<Form>()
                .HasMany(f => f.FormRecords)
                .WithOne(fr => fr.Form)
                .HasForeignKey(fr => fr.FormId);

            modelBuilder.Entity<Page>()
                .HasMany(p => p.FormFields)
                .WithOne(ff => ff.Page)
                .HasForeignKey(ff => ff.PageId);

            base.OnModelCreating(modelBuilder);

            // Seeding data
            SeedData(modelBuilder);
        }
        private void SeedData(ModelBuilder modelBuilder)
        {
            // Create IDs for relationships
            var formId1 = Guid.NewGuid();
            var formId2 = Guid.NewGuid();
            var pageId1 = Guid.NewGuid();
            var pageId2 = Guid.NewGuid();
            var formFieldId1 = Guid.NewGuid();
            var formFieldId2 = Guid.NewGuid();
            var formRecordId1 = Guid.NewGuid();
            var formRecordId2 = Guid.NewGuid();

            // Seed Forms
            modelBuilder.Entity<Form>().HasData(
                new Form
                {
                    Id = formId1,
                    Name = "Customer Feedback Form",
                    Description = "A form to collect customer feedback.",
                },
                new Form
                {
                    Id = formId2,
                    Name = "Employee Survey",
                    Description = "A survey to collect employee opinions.",
                }
            );

            // Seed Pages
            modelBuilder.Entity<Page>().HasData(
                new Page
                {
                    Id = pageId1,
                    FormId = formId1,
                },
                new Page
                {
                    Id = pageId2,
                    FormId = formId2,
                }
            );

            // Seed FormFields
            modelBuilder.Entity<FormField>().HasData(
                new FormField
                {
                    Id = formFieldId1,
                    PageId = pageId1,
                    Name = "profileFirstName",
                    Required = false,
                    Attributes = "{}",
                    Kind = "profile",
                    FieldType = "firstName",
                    Rules = "{\"minLength\": 2, \"maxLength\": 128}",
                },
                new FormField
                {
                    Id = formFieldId2,
                    PageId = pageId2,
                    Name = "profileLastName",
                    Required = false,
                    Attributes = "{}",
                    Kind = "profile",
                    FieldType = "lastName",
                    Rules = "{\"minLength\": 2, \"maxLength\": 128}",
                }
            );

            // Seed FormRecords
            modelBuilder.Entity<FormRecord>().HasData(
                new FormRecord
                {
                    Id = formRecordId1,
                    FormId = formId1,
                    FormFieldValues = "{\"firstName\": \"Michelle\", \"lastName\": \"Smith\"}",
                    CreatedAt = DateTime.UtcNow,
                },
                new FormRecord
                {
                    Id = formRecordId2,
                    FormId = formId2,
                    FormFieldValues = "{\"firstName\": \"Yael\", \"lastName\": \"Doe\"}",
                    CreatedAt = DateTime.UtcNow,
                }
            );
        }
    }
}
















/*
using FormAPI.Models;
using Microsoft.EntityFrameworkCore;
using System;

namespace FormAPI.Context
{
    public class ApplicationDbContext : DbContext
    {
        // Added to address the timestamp issue
        static ApplicationDbContext()
        {
            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
        }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<FormField> formfields { get; set; }
        public DbSet<FormRecord> formrecords { get; set; }
        public DbSet<Form> forms { get; set; }
        public DbSet<Page> pages { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Form>()
            .HasMany(f => f.Pages)
            .WithOne(p => p.Form)
            .HasForeignKey(p => p.FormId);

            modelBuilder.Entity<Form>()
                .HasMany(f => f.FormRecords)
                .WithOne(fr => fr.Form)
                .HasForeignKey(fr => fr.FormId);

            modelBuilder.Entity<Page>()
                .HasMany(p => p.FormFields)
                .WithOne(ff => ff.Page)
                .HasForeignKey(ff => ff.PageId);

            base.OnModelCreating(modelBuilder);

            // Seeding data
            SeedData(modelBuilder);
        }
        private void SeedData(ModelBuilder modelBuilder)
        {
            // Create IDs for relationships
            var formId1 = Guid.NewGuid();
            var formId2 = Guid.NewGuid();
            var pageId1 = Guid.NewGuid();
            var pageId2 = Guid.NewGuid();
            var formFieldId1 = Guid.NewGuid();
            var formFieldId2 = Guid.NewGuid();
            var formRecordId1 = Guid.NewGuid();
            var formRecordId2 = Guid.NewGuid();

            // Seed Forms
            modelBuilder.Entity<Form>().HasData(
                new Form
                {
                    Id = formId1,
                    Name = "Customer Feedback Form",
                    Description = "A form to collect customer feedback.",
                },
                new Form
                {
                    Id = formId2,
                    Name = "Employee Survey",
                    Description = "A survey to collect employee opinions.",
                }
            );

            // Seed Pages
            modelBuilder.Entity<Page>().HasData(
                new Page
                {
                    Id = pageId1,
                    FormId = formId1,
                },
                new Page
                {
                    Id = pageId2,
                    FormId = formId2,
                }
            );

            // Seed FormFields
            modelBuilder.Entity<FormField>().HasData(
                new FormField
                {
                    Id = formFieldId1,
                    PageId = pageId1,
                    Name = "profileFirstName",
                    Required = false,
                    Attributes = "{}",
                    Kind = "profile",
                    FieldType = "firstName",
                    Rules = "{\"minLength\": 2, \"maxLength\": 128}",
                },
                new FormField
                {
                    Id = formFieldId2,
                    PageId = pageId2,
                    Name = "profileLastName",
                    Required = false,
                    Attributes = "{}",
                    Kind = "profile",
                    FieldType = "lastName",
                    Rules = "{\"minLength\": 2, \"maxLength\": 128}",
                }
            );

            // Seed FormRecords
            modelBuilder.Entity<FormRecord>().HasData(
                new FormRecord
                {
                    Id = formRecordId1,
                    FormId = formId1,
                    FirstName = "Michelle",
                    SecondName = "Valeria",
                    LastName = "Smith",
                    Birthdate = new DateTime(1980, 1, 1),
                    Gender = "Female",
                    LanguageCode = "en",
                    Nationality = "American",
                    PhoneNumber = "1234567890",
                    Email = "michelle@example.com",
                    Arrival = DateTime.UtcNow,
                    Departure = DateTime.UtcNow.AddDays(7),
                    Address = "123 Main St",
                    Zip = "12345",
                    City = "New York",
                    Country = "USA",
                    Rating = "10/10"
                },
                new FormRecord
                {
                    Id = formRecordId2,
                    FormId = formId2,
                    FirstName = "Yael",
                    SecondName = "Thomas",
                    LastName = "Doe",
                    Birthdate = new DateTime(1990, 2, 2),
                    Gender = "Male",
                    LanguageCode = "en",
                    Nationality = "Canadian",
                    PhoneNumber = "0987654321",
                    Email = "yael@example.com",
                    Arrival = DateTime.UtcNow,
                    Departure = DateTime.UtcNow.AddDays(7),
                    Address = "456 Elm St",
                    Zip = "54321",
                    City = "Toronto",
                    Country = "Canada",
                    Rating = "10/10"
                }
            );
        }
    }
}

*/









/*
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
        public DbSet<Form> forms { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configure Form entity
            modelBuilder.Entity<Form>()
                .HasKey(f => f.Id);

            // Configure FormRecord entity
            modelBuilder.Entity<FormRecord>()
                .HasKey(fr => fr.Id);

            // Configure the relationship between Form and FormRecord
            modelBuilder.Entity<FormRecord>()
                .HasOne(fr => fr.Form)
                .WithMany(f => f.Records)
                .HasForeignKey(fr => fr.FormId)
                .OnDelete(DeleteBehavior.Cascade); // Adjust delete behavior as needed

            // Configure FormField entity
            modelBuilder.Entity<FormField>()
                .HasKey(ff => ff.Id);
            // Configure the relationship between Form and FormField
            modelBuilder.Entity<FormField>()
                .HasOne(ff => ff.Form)
                .WithMany(f => f.Fields)
                .HasForeignKey(ff => ff.FormId)
                .OnDelete(DeleteBehavior.Cascade); // Adjust delete behavior as needed

            //modelBuilder.Entity<FormRecord>()
            //    .HasOne(fr => fr.FormField)
            //    .WithMany(ff => ff.Records)
            //    .HasForeignKey(fr => fr.FormFieldId)
            //    .IsRequired();

            //base.OnModelCreating(modelBuilder);

            //using Fluent API to specify the column names in your DbContext

            //modelBuilder.Entity<FormRecord>()
            //.Property(f => f.FirstName)
            //.HasColumnName("FirstName");


            base.OnModelCreating(modelBuilder);

            //seed data

            modelBuilder.Entity<Form>().HasData(
                new Form
                {
                    Id = 1, // specify the id for the seed form (if needed)
                    Name = "questionnaire",
                    Description = "ask some personal questions for statistics",
                }

            );

            modelBuilder.Entity<FormField>().HasData(
                new FormField
                {
                    Id = -1, // Example negative value for seed data
                    Name = "first name",
                    Required = true,
                    Kind = "profile",
                    FieldType = "text",
                    Attributes = "new attribute",
                    Rules = "string",
                    FormId = 1
                }
                // Add more seed data FormFields as needed
            );






            modelBuilder.Entity<FormRecord>().HasData(

                new FormRecord
                {
                    Id = -1,
                    FirstName = "Jane",
                    SecondName = "Doe",
                    LastName = "Doe",
                    Birthdate = new DateTime(1995, 1, 1),
                    Gender = "Female",
                    PhoneNumber = "0714665512",
                    Email = "jane@example.com",
                    Address = "123 Main St",
                    Zip = "12345",
                    City = "Philadephia",
                    Country = "USA",
                    Arrival = DateTime.UtcNow.Date, // Example: Today's date
                    Departure = DateTime.UtcNow.Date.AddDays(9), // Example: 10 days from today
                    FormId = 1
                    //FieldType = "text",
                    //Kind = "profile",
                    //Attributes = new Dictionary<string, string>
                    //{
                    //    { "Attribute1", "Value1" },
                    //    { "Attribute2", "Value2" }
                    //    // Add more attributes as needed
                    //}
                }
                // Add more FormRecords as needed
            );

        }
    }
}
*/