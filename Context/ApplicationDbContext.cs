using Microsoft.EntityFrameworkCore;
using FormAPI.Models;

namespace FormAPI.Context
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
            //can addd connection string but i choose to add it in appsettings.json
        }

        public DbSet<FormField> FormFields { get; set; }
        //public DbSet<FormRecord> FormRecords { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Ignore the Rules property of the FormField entity
            modelBuilder.Entity<FormField>().Ignore(f => f.Rules);

            base.OnModelCreating(modelBuilder);
        }
    }
}
