# Task Management System
![alt text](https://github.com/andrewkamal/TaskManagementSystem/blob/master/TaskManagementSystem/wwwroot/images/TMS.jpg)

## Demo

Please refer to the Demo: [Link](https://drive.google.com/file/d/1xQE1i0sDifMSTsj1B-N7yncsbHqaurcQ/view?usp=drive_link)

## Description

This is an implementation of a Task Management System built with Entity Framework Core and the MVC model. It allows users to manage teams and tasks and generate reports. There are several features like user authentication, authorization, auditing, dynamic views, task assignment, error handling, and dynamic reporting. 

Back-end: ASP.NET Core as the back-end, SQL Server as the database,

Front-end: Built a responsive web interface using HTML5, CSS3, and Bootstrap for styling. JS and JQuery were used to help achieve some front-end features.

Auditing: NLog is used to Log almost everything important. The configuration file of Nlog is included in the project.

Utilized Entity Framework (EF) Core for data access using a Code-First approach to make use of Migrations and better flexibility.


## Roles and Accounts

There are three pre-made Roles: 
- Admin
- Lead
- User

Access the Accounts (Password for any account is: **Password1.** ): 
- Email: admin@gmail.com
- Email: lead@gmail.com
- Email: user@gmail.com
- Email: a@gmail.com
- Email: b@gmail.com

There is the option to manage all the roles, users, and teams as long as you are an **Admin**.


## Features

- **User Authentication**: Users can register, log in, and log out. Passwords are hashed and salted for security.
- **User Administration**: Administrators can create, edit, and delete users. They can also assign users to teams.
- **Error Handling**: Implemented error handling such as 404 errors or unauthorized access.
- **Task Management**: Users can create, edit, and delete tasks. Tasks can be assigned to teams.
- **Reporting**: Administrators and Team Leaders can generate reports on task completion rates, team performance, and individual user productivity.
- **Dynamic dashboard**: Customize the user dashboard based on their role. Regular users see a simplified task list, while Leads and Admins have access to advanced filtering and reporting options.
- **Audit trail**: Implemented an audit trail system to log all significant actions (task creation, assignment changes, status updates, etc) for compliance and accountability using Nlog.
- **Filtering Options**: Search options are implemented in Reports and the Management of Roles, Teams, and Users.

## Controllers

- User authentication (see [`AccountController.cs`](TaskManagementSystem/Controllers/AccountController.cs))
- User administration (see [`AdministrationController.cs`](TaskManagementSystem/Controllers/AdministrationController.cs))
- Home (see [`HomeController.cs`](TaskManagementSystem/Controllers/HomeController.cs))
- Error handling (see [`ErrorController.cs`](TaskManagementSystem/Controllers/ErrorController.cs))
- Task management (see [`TasksController.cs`](TaskManagementSystem/Controllers/TasksController.cs))
- Reporting (see [`ReportController.cs`](TaskManagementSystem/Controllers/ReportController.cs))


## Models

![alt text](https://github.com/andrewkamal/TaskManagementSystem/blob/master/TaskManagementSystem/wwwroot/images/ERD.png)

The system uses the following main models:

- **ApplicationUser**: Represents a user in the system. Inherits from `IdentityUser`. Has a many-to-many relationship with `Team` through the `UserTeam` join table.
- **Team**: Represents a team in the system. Has a many-to-many relationship with `ApplicationUser` through the `UserTeam` join table.
- **UserTeam**: Join the table for the many-to-many relationship between `ApplicationUser` and `Team`. Each `UserTeam` represents a member of a user in a team.
- **Task**: Represents a task in the system. Each task is assigned to an `ApplicationUser`.

## Getting Started

### Prerequisites

- .NET Core 3.1 or later
- SQL Server 2019 or later
- Visual Studio 2017 or later

### Installation

1. Clone the repository: `https://github.com/andrewkamal/TaskManagementSystem.git`
2. Navigate to the project directory: `cd TaskManagementSystem`
3. Restore the packages: `dotnet restore`
4. Build the project: `dotnet build`
5. Import the SQL file [`TMS.bak`](https://github.com/andrewkamal/TaskManagementSystem/blob/master/TMS.bak) into SQL Server to populate the database with tables and its data. In case the .bak file fails, the is the SQL Script file [`TMS.sql`](https://github.com/andrewkamal/TaskManagementSystem/blob/master/TMS.sql)

## Packages

These are the NuGet packages used in the project:

- **Microsoft.AspNetCore.Identity.EntityFrameworkCore**: Provides the Entity Framework Core data access layer for ASP.NET Core Identity.
- **Microsoft.EntityFrameworkCore**: The core package for Entity Framework Core.
- **Microsoft.EntityFrameworkCore.SqlServer**: The SQL Server database provider for Entity Framework Core.
- **Microsoft.EntityFrameworkCore.Tools**: Provides design-time tools for Entity Framework Core.
- **NLog.Extensions.Logging**: Integrates NLog with Microsoft.Extensions. Logging, which is used by ASP.NET Core.
- **NLog.Web.AspNetCore**: Integrates NLog with ASP.NET Core.

They are easily added to the project using the Nuget Package Manager. Nevertheless, these packages can be installed using the built-in cmd in Visual Studio using the following commands instead:

```bash
dotnet add package Microsoft.AspNetCore.Identity.EntityFrameworkCore
dotnet add package Microsoft.EntityFrameworkCore
dotnet add package Microsoft.EntityFrameworkCore.SqlServer
dotnet add package Microsoft.EntityFrameworkCore.Tools
dotnet add package NLog.Extensions.Logging
dotnet add package NLog.Web.AspNetCore
```

### Configuration

Edit the `appsettings.json` file to set your database connection string. For simplicity, only change the Server name to the name of your server and leave the other options the same.

![alt text](https://github.com/andrewkamal/TaskManagementSystem/blob/master/TaskManagementSystem/wwwroot/images/appsettings.png)

## Usage

To run the project, use the command `dotnet run` using the command line or run the HTTP/IIS Express option in Visual Studio.

## Ideas for Future Implementation
- Implement the User Claims Management
- Log the files on a server instead of locally for better reach and security
- Push the users' profile picture on a server or an S3 Bucket instead of locally
- Advanced Task Tracking (task dependencies). Gantt chart view to visualize task timelines and dependencies.
- Enhance the Reports to reach better analytics

## Contributing

In case you would like to contribute to the project you are more than welcome! Please open an issue if you have a bug to report or a feature to request. If you'd like to contribute to the code, please open a pull request.

## License

This project is licensed under the MIT License.

## Contact

If you have any questions, please email me at andrewk.kamal@aucegypt.edu
