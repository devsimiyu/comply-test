# Comply Test Project
A web application that allows users to view a list of items and add new items. The application is developed using Angular and .NET.

## Directory Structure

This project contains the following folders:
- `ComplyTest.Core`: Contains domain enties, repositories and services
- `ComplyTest.Data`: Data layer that host entity database migrations, EF Core persistence context and implementations of the domain repositories
- `ComplyTest.Service`: Service layer contains implementations of the domain services
- `ComplyTest.Api`: Entrypoint that bootstraps and runs the backend .NET Core Web Api
- `ComplyTest.Core`: Contains Api integration tests
- `ComplyTest.Ui`: Frontend application in Angular

## Application Setup

- `Database`: The application uses SQLite database, and EF Core migrations tool for applying domain entity changes to the database.
- `Backend`: The Api is written using `.NET Core 8.0`
- `Frontend`: The front is built using `Angular v17`

## Database
The EF Core tools is needed to create a database and run migrations. Runs this command to install the EF core tools `dotnet tool install --global dotnet-ef`. 
Once the EF Core tools is installed, proceed to apply any pending migrations by executing this command `dotnet ef database --project ComplyTest.Data update -- --Database "Data Source=..\\ComplyTest.db"`

## API
To run the API, execute this command: `dotnet run --project ComplyTest.Api --launch-profile http`

## UI
The frontend can be started by this command: `npm start`

## Integration Tests
The API integration tests can be executed by this command `dotnet test ComplyTest.IntegrationTests`
