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
