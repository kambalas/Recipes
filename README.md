# Recipe Sharing Platform

Welcome to our Recipe Sharing Platform! This application is built using .NET and is structured into three main projects: the backend API, the frontend Blazor Server-rendered web app, and a shared commons library. This architecture ensures clarity, scalability, and maintainability throughout the application.

## Table of Contents
- [Introduction](#introduction)
- [Implementation of Qualitative Requirements for the System](#implementation-of-qualitative-requirements-for-the-system)
  - [Concurrency](#concurrency)
  - [Security](#security)
  - [Data Access](#data-access)
  - [Data Consistency; Optimistic Locking](#data-consistency-optimistic-locking)
  - [Memory Management](#memory-management)
  - [Reactive Programming; Asynchronous/Non-Blocking Communication](#reactive-programming-asynchronousnon-blocking-communication)
  - [Cross-Cutting Functionality/Interceptors](#cross-cutting-functionalityinterceptors)
  - [Extensibility/Glass-Box Extensibility](#extensibilityglass-box-extensibility)

## Introduction
We've developed a well-structured recipe-sharing application using .NET, divided into three key projects: the backend API, the frontend Blazor Server-rendered web app, and a shared commons library. This division ensures clarity in responsibilities and smooth integration.

### Backend
In the backend, we utilized ASP.NET Core Web API along with Entity Framework Core to manage an SQL database. This setup is organized into layers:

- **Data Access Layer (DAL):** Handles database models and includes DbContext for managing connections, and repositories for CRUD operations.
- **Business Logic Layer (BLL):** Encapsulates services interacting with repositories.
- **Presentation Layer:** Exposes endpoints through API Controllers.

### Frontend
On the front end, we’ve implemented a Blazor Server-rendered web app. This choice provides interactive web functionality using C#. The front end also adopts a layered approach:

- Presentation elements like Blazored Pages represent different parts of the UI.
- Reusable components and services manage HTTP requests to the backend.

### Shared Commons Library
Shared resources in the commons library include:

- DTOs for efficient data transfer.
- Utility classes for validation and filtering.
- Enums defining shared values like recipe difficulty level or ingredient measurement.

This architecture fosters consistency, scalability, and maintainability across the application.

## Implementation of Qualitative Requirements for the System

### Concurrency
We store authentication JWT tokens in cookies, enabling users to seamlessly interact with the app across different tabs without needing to log in again. Since all our services are request-scoped, users experience smooth and concurrent usage of our web app.

**Example in our code:** `Program.cs`

### Security
In our application, we leverage Entity Framework Core (EF Core) extensively for database interactions. When querying or modifying data, we construct LINQ queries or method calls instead of composing raw SQL statements. EF Core then translates these queries into parameterized SQL queries behind the scenes.

**Example in our code:** `GenericRepository.cs`

### Data Access
As mentioned above, Entity Framework is utilized, where SQL statements are wrapped with LINQ. This ensures flexibility and code simplicity. In addition, all CRUD operations are request-scoped, meaning that database connections are opened and closed within the context of each request. This scoped approach ensures that database resources are properly managed and isolated for each request, minimizing the risk of connection pooling vulnerabilities and unauthorized access.

**Example in our code:** `Program.cs`

### Data Consistency; Optimistic Locking
We opted for Entity Framework with SQL due to its built-in optimistic locking feature, which adds safety when users access the web app concurrently. This functionality prevents data inconsistency by detecting conflicts and ensuring that updates are made to the latest version of the data, enhancing overall data integrity and reliability.

**Example in our code:** `Recipe.cs`

### Memory Management
We prioritize memory management by extensively using dependency injection (DI) throughout our project. With the scoped lifetime (`AddScoped`), we efficiently manage resources, ensuring services are created once per request in a web application and deleted after each request.

**Example in our code:** `Program.cs`

### Reactive Programming; Asynchronous/Non-Blocking Communication
We have implemented asynchronous programming using `async` and `await` to ensure non-blocking calls. This approach enables our web app to perform tasks such as data fetching, API calls, and other I/O operations asynchronously, improving responsiveness and user experience.

**Example in our code:** `UserService.cs`

### Cross-Cutting Functionality/Interceptors
We have implemented logging for all business logic actions using interceptors in our service classes. This logging captures essential details such as user information, method names, and class names, which are recorded in a log file. Configuration settings for enabling/disabling and customizing the log output are easily adjustable in the `appsettings.json` file, ensuring flexibility without requiring modifications to the monitored system code.

**Example in our code:**
- `appsettings.json`
- `AsyncLogger.cs`

### Extensibility/Glass-Box Extensibility
In our .NET app, we've applied the Decorator design pattern for dynamic algorithm switching and enhancement without altering existing code. We've wrapped the `UserRepository` class with the `UserRepositoryDecorator` class to add logging to all `UserRepository` methods. Using Scrutor nuget package for dependency injection and lifecycle management, our approach ensures flexibility and maintainability in our application architecture.

**Example in our code:** `Program.cs`

