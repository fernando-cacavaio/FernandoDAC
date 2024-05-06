# FernandoDAC

## Description
This project is a technical assessment demonstrating proficiency in .NET 8, ASP.NET Web API, C#, Microsoft SQL with AutoMapper and Dapper, MediatR Pattern, and Xunit/Moq/Shouldly for tests. This is the Backend project that contains CRUD for Patients.

## Features
- **.NET 8**: Leveraging the latest features and enhancements of .NET for optimal performance and maintainability.
- **ASP.NET Web API**: Building a robust API to facilitate seamless communication between clients and servers.
- **C#**: Implementing the backend logic with the expressive and powerful C# language.
- **SQL Express with AutoMapper and Dapper**: Utilizing Dapper for data access, enhanced by AutoMapper for streamlined object-to-object mapping.
- **MediatR Pattern**: Employing the MediatR pattern for clean and maintainable separation of concerns, enabling easy handling of commands and queries.
- **XUnit/Moq/Shouldly for Tests**: Ensuring the reliability and correctness of the codebase through comprehensive unit tests using XUnit for testing framework, Moq for mocking dependencies, and Shouldly for fluent assertions.
- **JWT (JSON Web Token)**: JWT is a compact, URL-safe means of representing claims to be transferred between two parties. It's commonly used for authentication and authorization in web applications.

## Installation
1. Clone the repository.
   ```sh
   git clone https://github.com/fernando-cacavaio/FernandoDAC.git
2. Open the solution file FernandoDAC.sln in Visual Studio or your preferred IDE.
3. Build the solution to restore dependencies.
4. Run the project using your IDE's running capabilities.
5. Use the scripts to create the database, check if you are using the correct name in appsettings.json.

## Usage
1. Once the project is running, access the API endpoints using your preferred API client (e.g., Postman) or swagger using the following url https://localhost:7071/swagger/index.html.
2. Explore the available endpoints and functionalities provided by the API.
3. Make requests to interact with the API and observe the responses.
4. Refer to the documentation or source code for detailed information on each endpoint and its expected behavior.

## Testing
1. Navigate to the Tests directory in the solution.
2. Open the test files containing the Xunit tests.
3. Run the tests using your IDE's testing capabilities or through the command line.
   ```sh
   dotnet test
4. Verify that all tests pass successfully to ensure the correctness and reliability of the codebase.
