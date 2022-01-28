# FSEngineerTest
Implemetation of ASP.Net Core 3 Web API and Angular 10.2. I have implemented global DBCrudere Service, Angular Crudere Service and Angular Base Component.
# WEB API.
I Have core that store the DB model/entities
Infra has connection to the DB and the Services
I also have DBCrudere that have all the CRUD operations that exist in a DB. I am injecting the Crudere in the Controller using Dependency Injection.
# Angular
* It has two globals. A Crudere service for all the DB funtions and a Base Component that is extended in every component.
# API ENDPOINTS
I have created three APIS EndPoint.
* /jokes/   
_loads all the jokes to the SPA_
* /jokes/category/  
_loads all the categories_
* /api/people/     
_loads all the people_
* /jokes/categoryId?=2 
_loads all the jokes in a particular category_
# Getting Started
* Clone the project from github
* Navigate to AngularUI with CMD or CMDER and run NPM install.
* Run the API from Visual Studio
* Run AngularUI using ng serve and open your project.
# Access
Login using 
* _username_: Nickson@test.com
* _Password_: 123456
