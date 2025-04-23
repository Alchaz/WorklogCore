# Project Title and Description

Worklog-VUE 
This project is an exercise for demostrating skills in front end with Vue and backend with .net core.
This part of the project contains the software for backend it contains the apis for calling of the frontend.



## Tech Stack
```
FrontEnd, this part of the software uses the next tech stack:
-.Net Core 8.
-Entity Framework Core.
-IdentityModel.Tokens.Jwt.
-
```

### How to Run Locally
```
-`git clone https://github.com/Alchaz/WorklogCore`  
-Open and start with visual studio .sln file.


### Implemented Features
```
Each of the call to the controllers of the project will comprobate the token the head contains.
The login api call can be executed with not token required but user has to be valid.

```
### Project Structure
```

1. Data
Handles all database-related logic and repository patterns.

Context

WorklogContext.cs: Entity Framework DbContext for managing DB operations.

Repositories and Interfaces

IRepository.cs, IUserRepository.cs, IWorklogRepository.cs: Define data access contracts.

Repository.cs, UserRepository.cs, WorklogRepository.cs: Implement those contracts.

2. Entities
Defines domain models and shared types.

User.cs, Worklog.cs: Core entity classes.

Enums.cs: Common enums.

Authentication.cs: Likely contains auth-related models or helpers.

Class1.cs: Placeholder or unused class.

Dependencias: Possibly shared or external types (contents not shown).

3. Helpers
Contains utility classes or logic used across layers.

Authentication.cs: Possibly handles token creation or auth utilities.

Enums.cs: Central location for enums.

4. Services
Contains the application's business logic layer.

IUserService.cs, IWorklogService.cs: Service interfaces.

UserService.cs, WorklogService.cs: Implement business operations.

Dependencias: May include shared services or wrappers.

5. WorklogCore
Main Web API project (presentation layer).

Controllers

HomeController.cs, UserController.cs, WorklogController.cs: Define HTTP endpoints.

Models

UpdateWorkedHoursDto.cs: Data Transfer Object for update operations.

UserModel.cs: Likely a view model or API model.

appsettings.json: App config file.

Program.cs: Entry point of the application.

WeatherForecast.cs: Default scaffolded file (usually removable).

WorklogCore.http: REST client test file for API requests.
```





