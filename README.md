# FSEngineerTest

Implementation of ASP.Net Core 3 Web API and Angular 10.2. I have implemented global DBCrudere Service, Angular Crudere Service and Angular Base Component.

# WEB API.

### Core

The project contains database models and entity framework base models.

### Infra

The project contains connection to external infrastructures like Database, Microsoft Dynamic Navision Business central etc. It also has database migrations.

### Service

The project defines interfaces, project utilities like extensions and helpers. It contains other services like Mapper Service, Identity Service and crudereservice. **CrudereService has database commonly used functions like getById, Create, Update and other CRUD operations**. They are added to the project through dependency injection.

### Web API

The project has project models and DTOs, controllers, pages, appsettings, program CS and startupCS. The project also contains the angular application. Under controller I have a **Base Controller** defined as **DBCrudere**. The controller defines all the logic within the controller and I just Inject it into all other controllers via Dependency Injection.

# Angular

- It has two global.

- ### CrudereService

  The utility service is used to communicate with the API endpoints and return an observable. It contains all the main functions needed when querying data from an API endpoint.

- ### Base Component.
  I am using it to hold most of the basic functionality in any component. It connects with CrudereService to call the API whenever needed and give the required response from the server. The component is extended in every other component in the system.

# API ENDPOINTS

I have created three APIS Endpoint.

- /jokes/  
  _loads all the jokes to the SPA_
- /jokes/category/  
  _loads all the categories_
- /api/people/  
  _loads all the people_
- /jokes/categoryId?=2
  _loads all the jokes in a particular category_

# Getting Started

- Clone the project from github
- Navigate to AngularUI with CMD or CMDER and run NPM install.
- Run the API from Visual Studio
- Run AngularUI using ng serve and open your project.

# Access

Provided in the email
Url: https://sovtechtaskspa.azurewebsites.net/
