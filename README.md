# Company Management System 🏢

**ASP.NET Core MVC Learning Project**

A learning project built with **ASP.NET Core MVC** for managing employees and departments.

The project was created to practice the fundamentals of the MVC architecture and understand how different components of a traditional web application work together.

## 🎯 Project Overview

The application provides basic functionality for managing:

* Employees
* Departments

The main goal of the project is to understand and practice the core concepts of **ASP.NET Core MVC**.

## ✨ Features

* Employee management
* Department management
* CRUD operations
* MVC architecture
* Model binding
* Controllers and Actions
* Razor Views
* Entity Framework Core
* Database integration

## 🏗️ Architecture

The project follows the **Model-View-Controller (MVC)** architectural pattern.

```text
User Request
     │
     ▼
 Controller
     │
     ▼
   Model
     │
     ▼
  Database
     │
     ▼
    View
     │
     ▼
 HTML Response
```

### Model

Represents the application's data and business entities.

### View

Provides the user interface using Razor Views.

### Controller

Handles HTTP requests, coordinates application logic, and returns the appropriate response.

## 🛠️ Technologies

| Technology            | Purpose              |
| --------------------- | -------------------- |
| C#                    | Programming Language |
| ASP.NET Core MVC      | Web Framework        |
| Entity Framework Core | ORM                  |
| SQL Server            | Database             |
| Razor Views           | UI                   |
| HTML / CSS            | Frontend             |
| Bootstrap             | UI Styling           |

## 📚 Learning Objectives

This project was developed to practice:

* MVC architecture
* Routing
* Controllers and Actions
* Model Binding
* Validation
* Razor Views
* Dependency Injection
* Entity Framework Core
* CRUD operations
* Database relationships

## 🚀 Getting Started

### Prerequisites

* .NET SDK
* SQL Server
* Visual Studio

### Installation

Clone the repository:

```bash
git clone https://github.com/SeifSherif84/Company.git
```

Navigate to the project:

```bash
cd Company
```

Restore dependencies:

```bash
dotnet restore
```

Configure the database connection.

Apply migrations if available:

```bash
dotnet ef database update
```

Run the application:

```bash
dotnet run
```

## 📌 Project Status

Completed learning project.

## 👨‍💻 Author

**Seif Sherif**

.NET Backend Developer | C# | ASP.NET Core

GitHub: [@SeifSherif84](https://github.com/SeifSherif84)
