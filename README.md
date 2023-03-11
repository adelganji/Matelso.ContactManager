# Matelso.ContactManager

# Contacts CRUD Project

This is a CRUD project for managing contacts, built using CQRS and MediatR with FluentValidator for validation and Swagger for presentation.

## Getting Started

To get started, you must first create a PostgreSQL database and run the query in the file named "CREATE Contact Table.sql" found in the root of the project. This will create the necessary "Contact" table.

Once the table is created, you can run the project and access the Swagger UI by navigating to the following URL:

https://localhost:{port}/swagger-ui/


Replace {port} with the port number on which the project is running.

## Installation

1. Clone the repository.
2. Create the Contact table in PostgreSQL using the "CREATE Contact Table.sql" file located in the root of the project.
3. Update the connection string in the `appsettings.json` file to match your PostgreSQL database.
4. Run the application.


## Technologies Used

- .NET 6
- CQRS
- MediatR
- FluentValidator
- Swagger
- PostgreSQL

## Contributing

If you would like to contribute to this project, please fork the repository and submit a pull request. 
