
# .NET API Core Project | api-core
## Overview

This project is a RESTful Web API built using .NET Core that provides CRUD functionality for managing users, roles, and companies. It incorporates JWT authentication for secure access, role-based authorization to manage permissions, and uses Dapper for efficient data access. The API also implements throttling middleware to limit request rates and includes Swagger UI for easy testing and documentation. API versioning is supported to allow multiple versions of the API to coexist.

## Features

- **CRUD Operations**: Create, Read, Update, and Delete operations for users, roles, and companies.
- **JWT Authentication**: Secure access to the API using JSON Web Tokens.
- **Role-Based Authorization**: Different roles (Admin, User) with varying levels of access permissions.
- **Dapper Repository**: Lightweight and efficient data access layer using Dapper ORM.
- **Middleware Throttling**: Limits the number of requests from a single user to prevent abuse (e.g., max 10 requests per minute).
- **Swagger UI**: Interactive API documentation and testing interface.
- **API Versioning**: Allows the API to maintain multiple versions for backward compatibility.

## Technologies Used

- **.NET Core**: Backend framework for building the API.
- **JWT**: For authentication and authorization.
- **Dapper**: Micro ORM for data access.
- **Azure SQL Edge**: Database management system.
- **Docker**: Containerization for easy deployment and management of services.
- **Swagger**: API documentation and testing tool.
- **Middleware**: For request throttling.
- **API Versioning**: To manage and maintain different versions of the API.

## Getting Started

### Prerequisites

- [.NET SDK](https://dotnet.microsoft.com/download) (version 6.0 or later)
- [Docker](https://www.docker.com/get-started) (to run Azure SQL Edge)
- [Dapper NuGet Package](https://www.nuget.org/packages/Dapper)
- [Swashbuckle.AspNetCore NuGet Package](https://www.nuget.org/packages/Swashbuckle.AspNetCore/)
- [Microsoft.AspNetCore.Mvc.Versioning NuGet Package](https://www.nuget.org/packages/Microsoft.AspNetCore.Mvc.Versioning/)

### Installation

**Clone the Repository**:
   ```bash
   git clone https://github.com/marktorrena1/api-core.git
   cd yourrepository