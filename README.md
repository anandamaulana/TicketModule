# 🎫 Ticket Module API

Ticket Module adalah REST API berbasis **ASP.NET Core Web API** yang digunakan untuk mengelola sistem tiket (helpdesk / issue tracking).  
Project ini menggunakan arsitektur berlapis dengan pemisahan Business Logic dan Data Access agar mudah dikembangkan dan di-maintain.

---

## 🚀 Features

- ✅ User Management
- ✅ Ticket Management
- ✅ Ticket Comments
- ✅ File Attachment Upload
- ✅ Pagination & Filtering
- ✅ Global Exception Handling
- ✅ Swagger API Documentation
- ✅ AutoMapper (DTO Mapping)
- ✅ Entity Framework Core Migration

---

## 🧱 Architecture

Project menggunakan konsep **Layered Architecture**:
Controller
↓
Service Layer (Business Logic)
↓
Repository Layer (Data Access)
↓
Entity Framework Core
↓
Database

---

## 📂 Project Structure

TicketModule
│
├── Controllers # API Endpoints
├── Contexts # DbContext EF Core
├── Models # Database Entities
├── ViewModels # Request & Response DTO
├── Services # Business Logic
├── Repositories # Data Access Layer
├── Middleware # Global Exception Handler
├── Extensions # Dependency Injection Config
├── Helpers # Utility & Pagination
├── Enums # Enum Definitions
├── Mappers # AutoMapper Profile
├── Migrations # EF Core Migration Files
└── uploads # Attachment Storage

---

## ⚙️ Tech Stack

- ASP.NET Core Web API
- Entity Framework Core
- AutoMapper
- Swagger / OpenAPI
- SQL Server
- Repository Pattern
- Dependency Injection

---

## 🛠️ Setup & Installation

### 1. Clone Repository

```bash
git clone https://github.com/your-username/TicketModule.git
cd TicketModule
