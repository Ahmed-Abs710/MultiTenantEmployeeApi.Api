# MultiTenantEmployeeApi

## Overview

MultiTenantEmployeeApi is a Multi-Tenant Employee Management API built using ASP.NET Core 8.

The project demonstrates:

* Clean Architecture
* CQRS with MediatR
* Multi-Tenancy
* PostgreSQL
* Entity Framework Core
* Docker & Docker Compose
* Unit Testing
* Integration Testing
* Repository Pattern
* Unit Of Work Pattern
* Global Error Handling
* Response Wrapping

---

# Architecture

The solution is divided into multiple layers:

## 1. API Layer

Contains:

* Controllers
* Middlewares
* Filters
* Dependency Injection
* Swagger

### Important Components

#### Tenant Middleware

Responsible for reading the tenant id from request headers:

```http
X-Tenant-Id
```

Then storing it in the current tenant service.

---

#### ErrorHandlerMiddleware

Handles global exceptions and returns unified API responses.

---

#### ResponseWrapperFilter

Wraps all API responses in a unified structure:

```json
{
  "success": true,
  "data": {},
  "error": null
}
```

---

## 2. Application Layer

Contains:

* CQRS Commands & Queries
* DTOs
* Handlers
* Interfaces
* Validation
* Pagination

### CQRS

The project uses:

* Commands for write operations
* Queries for read operations

Using MediatR.

---

## 3. Domain Layer

Contains:

* Entities
* Value Objects
* Enums
* Domain Exceptions

### Entities

* Employee
* Tenant

### Value Objects

* Money

---

## 4. Infrastructure Layer

Contains:

* EF Core
* PostgreSQL
* Repositories
* Unit Of Work
* DbContext
* Migrations

---

# Multi-Tenancy Implementation

The application supports Database Shared Multi-Tenancy.

Each request must contain:

```http
X-Tenant-Id
```

Example:

```http
X-Tenant-Id: 11111111-1111-1111-1111-111111111111
```

Tenant filtering is automatically applied when querying data.

---

# Technologies Used

* ASP.NET Core 8
* Entity Framework Core
* PostgreSQL
* MediatR
* FluentValidation
* Docker
* Docker Compose
* xUnit
* FluentAssertions
* Testcontainers

---

# Database

## Main Tables

### Employees

| Column            | Type   |
| ----------------- | ------ |
| ID                | Guid   |
| TenantId          | Guid   |
| FirstName         | string |
| LastName          | string |
| Email             | string |
| Department        | string |
| Status            | enum   |
| SalaryAmountMinor | long   |
| CurrencyCode      | string |

---

### Tenants

| Column | Type   |
| ------ | ------ |
| ID     | Guid   |
| Name   | string |

---

# API Endpoints

## Create Employee

```http
POST /api/v1/employees
```

### Request Body

```json
{
  "firstName": "Ahmed",
  "lastName": "Eid",
  "email": "ahmed@test.com",
  "department": "IT",
  "salaryAmountMinor": 100000,
  "currencyCode": "USD"
}
```

---

## Get Employees

```http
GET /api/v1/employees?pageNumber=1&pageSize=10
```

---

## Get Employee By Id

```http
GET /api/v1/employees/{id}
```

---

## Update Employee

```http
PUT /api/v1/employees/{id}
```

---

## Delete Employee

```http
DELETE /api/v1/employees/{id}
```

---

# Pagination

The API supports pagination using:

```http
?pageNumber=1&pageSize=10
```

Returned inside:

```json
{
  "items": [],
  "totalCount": 0,
  "pageNumber": 1,
  "pageSize": 10
}
```

---

# Docker Setup

## Run Using Docker Compose

```bash
docker compose up --build
```

---

## Stop Containers

```bash
docker compose down
```

---

## Remove Database Volume

```bash
docker compose down -v
```

Useful when resetting PostgreSQL data.

---

# Docker Services

## PostgreSQL

* Image: postgres:16
* Port: 5432

---

## API

* ASP.NET Core 8 API
* Port: 8080

---

# Running Migrations

Migrations are automatically applied on application startup using:

```csharp
app.MigrateDatabase();
```

Implemented in:

```csharp
MigrationExtensions
```

---

# Testing

The project contains:

* Unit Tests
* Integration Tests

---

# Integration Testing

Integration tests use:

* WebApplicationFactory
* Testcontainers
* PostgreSQL Container

Example:

```csharp
_postgres = new PostgreSqlBuilder()
    .WithImage("postgres:16")
    .WithDatabase("testdb")
    .WithUsername("postgres")
    .WithPassword("postgres")
    .Build();
```

---

## Running Tests

```bash
dotnet test
```

---

# Important Problems Solved During Development

## 1. testhost.deps.json Error

Solved by:

* Referencing the API project from the test project
* Using WebApplicationFactory correctly

---

## 2. PostgreSQL Foreign Key Errors

Solved by:

* Creating tenants before employees
* Saving tenants first using:

```csharp
await db.SaveChangesAsync();
```

---

## 3. NullReferenceException During Tests

Solved by:

* Adding Salary value object during test seeding

---

## 4. Docker Migration Duplicate Table Errors

Solved by:

```bash
docker compose down -v
```

Because PostgreSQL volume persisted old database data.

---

# Design Patterns Used

## Repository Pattern

Used to abstract data access logic.

---

## Unit Of Work Pattern

Used to manage transactions and repositories.

---

## CQRS Pattern

Separates read and write operations.

---

# Project Features

* Multi-Tenant Architecture
* Clean Architecture
* Pagination
* Global Error Handling
* Response Wrapping
* Docker Support
* PostgreSQL Integration
* Integration Testing
* Unit Testing
* CQRS
* MediatR
* Repository Pattern
* Unit Of Work Pattern

---

# How To Run The Project Locally

## 1. Clone Repository

```bash
git clone <repo-url>
```

---

## 2. Run Docker

```bash
docker compose up --build
```

---

## 3. Open Swagger

```text
http://localhost:8080/swagger
```

---

# Example Request Using Postman

## Headers

```http
X-Tenant-Id: 11111111-1111-1111-1111-111111111111
```

---

## Create Employee

```http
POST http://localhost:8080/api/v1/employees
```

Body:

```json
{
  "firstName": "Ahmed",
  "lastName": "Eid",
  "email": "ahmed@test.com",
  "department": "IT",
  "salaryAmountMinor": 100000,
  "currencyCode": "USD"
}
```

---

# Future Improvements

* Authentication & Authorization
* Refresh Tokens
* Role-Based Access Control
* Audit Logging
* Distributed Caching
* Redis
* API Versioning Enhancements
* CI/CD Pipeline
* Kubernetes Deployment

---

# Author

Ahmed Eid

Mid-Level .NET Full Stack Developer
