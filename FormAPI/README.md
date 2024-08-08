# Forms Project

FormAPI is a RESTful API built with ASP.NET Core that allows users to create, manage, and retrieve forms and their associated records. 
The API supports operations such as creating forms, adding fields to forms, and storing form records.
A form is created together with its fields, the fields are then referenced in taking user data which is stored in FormField values as FormRecords 
This project is built using the Entity Framework Core with a PostgreSQL database


##### Features 
Forms Management: Create, update, and delete forms.
Form Fields: Add, retrieve and Delete form fields associated with forms.
Form Records: Create, retrieve nupdate and delete form records 
UUID Support: All forms, fields, and records are identified by UUIDs for enhanced security and scalability.



## prerequisites 
visual studio 2022 - https://visualstudio.microsoft.com/vs/
. net 8 installation - https://dotnet.microsoft.com/en-us/download/dotnet/8.0
postresql and pgAdmin - https://www.postgresql.org/download/


##### INSTALATIONS
1. CLONE THE REPOSITORY
    git clone https://github.com/Lugadilu/Forms/tree/main/FormAPI.git
2. 
cd FormAPI

2. Configure the Database:

Create a PostgreSQL database.
Update the connection string in appsettings.json to point to your PostgreSQL instance.

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




### in the longrun the project structure should look like 

	FormAPI/
├── Controllers/
│   ├── FormsController.cs
│   └── FormRecordsController.cs
├── Models/
│   ├── Form.cs
│   ├── FormField.cs
│   └── FormRecord.cs
├── DTOs/
│   ├── FormDto.cs
│   ├── FormFieldDto.cs
│   └── FormRecordDto.cs
├── Repositories/
│   └── FormRepository.cs
|___|___IFormRepository.cs
├── Mapping/
│   └── MappingProfile.cs
├── Context/
│   └── ApplicationDbContext.cs
├── Program.cs
├── Startup.cs
└── appsettings.json


####DATABASE COMMANDS

Run migrations in the package manager console to create the tables ie   Add-Migration InitialMigration
   update the database ie Update-Database


####RUNNING THE API

The API is available on http://localhost:7240

####Documentation and Testing
The project is already configured with swagger which can be used for both documentation and Testing, as described in the program.cs
You can use Postman to test the endpoints. A Postman collection is included in the repository:

Import the Postman Collection:

Open Postman.
Go to File > Import.
Select the FormTest.postman_collection.json file.
Run Tests with the FormAPI set as the base URL, http://localhost:7240. set thuis in the environment 

You can run individual requests or automate the tests using Postman CLI.

####Endpoints
FormRecords
GET /formrecords - Lists out all form records in the DB
GET /forms/{formId}/records - incorparates the form Id field to retrieve an individual formrecord from the List, associated with a specific form
POST /forms/{formId}/records - creates a new form record
GET /forms/{formId}/records/{recordId} - retrievs a formrecord with the form iid and record id 
PUT /forms/{formId}/records/{recordId} - updates an available form record in the Db
DELETE /forms/{formId}/records/{recordId} - deletes a form record


Forms
GET /Forms - Lists out all forms in the DB
POST /Forms - creates a new form, with its formfields
GET /Forms/{formId} - retrieves a specific form from the DB using the form Id
DELETE /Forms/{formId} - deletes a form
PUT /Forms/{formId} - updates a form
GET /Forms/fields - retrieves formfields


Contributing
Contributions are welcome! Please fork this repository and submit a pull request for review.

Steps:
Fork the repository.
Create a new branch (git checkout -b feature-branch).
Make your changes.
Commit your changes (git commit -m 'Add some feature').
Push to the branch (git push origin feature-branch).
Open a pull request.




DETAILED STEPS

			
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

