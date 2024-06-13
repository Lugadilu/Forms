# Forms Project
			
## Setup Instructions
1. create a new FormApi  project and name it as FormAPI, ensure you set it up under ASP.NET core web API template
2. define database models, Form, FormFields and FormRecords, ensure the properties inside are per the openAPi
	also create DTOs to control the data returned in responses and used in requests
        create a MappingProfile class in the modelsfor mapping the models to Dtos and make them bi-directional
3. Set up AutoMapper in your project to map between your models and DTOs
4. Ensure the following dependencies are installed . 
	AutoMapper.Extensions.Microsoft.DependencyInjection - for mapping classes to DTos. automapper
	Dapper - if you would prefer manual instansation and doing direct SQL queries but it is highly recomended to use dependency injection
	Microsoft.EntityFrameworkCore - provides a way to interact with the database and migrations
	Microsoft.EntityFrameworkCore.Design for db migrations, updating db
	Microsoft.EntityFrameworkCore.InMemory - helps in testing without a need of a database, its like a virtual database
	Microsoft.EntityFrameworkCore.Tools - Allows developers to perform design-time tasks like creating migrations and updating the database schema from the command line or Package Manager Console.
	Microsoft.Extensions.DependencyInjection.Abstractions - for dependency injection
	Moq-enables testing like unit tests
	Newtonsoft.Json-for JSON serialization
	Npgsql.EntityFrameworkCore.PostgreSQL-our prefered database
	Swashbuckle.AspNetCore

5. create a connection to the db, create a folder calleed Context, create a class called ApplicationDbContext. Define our constructor in the ApplicationDbContext, and define our Db and have the models with tables	
6. in the startup.cs, under the IserviceCollection, register your database. For this project we used Npgsql so define it. The defaultConnection would be descrived below
	also configure these services services.
	AddScoped<IFormRepository, FormRepository>();
	services.AddAutoMapper(typeof(MappingProfile).Assembly);
	services.AddControllers();

	we would aslso desire to use ExceptionMiddleware to catch errors and excemptions in or before requests reach the controller, so you can also add the below in the IApplicationBuilder
	 app.UseMiddleware<ExceptionMiddleware>();
	
	app.UseRouting();
	app.UseEndpoints(endpoints =>
	{
    	   // Endpoint routing  // Map controllers using attribute routing
    	   endpoints.MapControllers();
	});

### Service Configuration
configure your appsetings.json to look something like this 
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=localhost;Database=mydatabase;User Id=myusername;Password=mypassword;"
  },
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft": "Warning",
      "Microsoft.Hosting.Lifetime": "Information"
    }
  },
  "AllowedHosts": "*"
}

### Create Repositories
   create IFormRepository. it is an interface class which represents operations which would be happening in tyhe database
   create FormRepository class. It inherits from IFormRepository and so it implements the interface. ensure parameter validation and error handlind ia cosidered to the latter
       ensure that whatever is in the interface is implemented to prevent errors

### create the FormsController
   inject the repositories and the I mapper in the constructor and create the endpoints. 

### create a Middleware folder and add a class called ExceptionMiddlewate
	this class cathes errors in the request pipeline before they7 reach the controller, in the controller or in the response

### Database Setup
 install pgsql
  in pgAdmin, navigate to the "Browser" panel on the left-hand side.
Expand the "Servers" group to find and select your PostgreSQL server.
Enter the required connection details such as Host, Port, Maintenance Database, Username, and Password. For the "Username," use "postgres." Then click "OK" or "Connect" to connect to the server.
 and create the database above, ie mydatabase


2. **Run Migrations**:
   in the Nugget Package Console and this is the command Add-Migration InitialMigration -c ApplicationDbContext -o Data/Mogrations
   update the database ie Update-Database

3. **Seeding if youlike:
   - Seed the database with initial data.
      in the ApplicationDbContext, add some seed data using this method modelBuilder.Entity<Form>().HasData({})

### Running Migrations

To apply database migrations, use the following command:
   modelBuilder.Entity<Form>().HasData(
in the package Manager Console, run the following commands
  Add-Migration SeedDatabase
  Update-Database


### Run the project, test it on swagger or postman

