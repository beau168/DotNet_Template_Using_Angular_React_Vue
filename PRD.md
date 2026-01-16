# Product Requirements Document (PRD)

## 1. Overview

### 1.1 Product Name

Angular-First .NET Full-Stack Platform (Azure-Optimized, BFF Architecture)

### 1.2 Purpose

Build a **production-ready, enterprise-grade starter platform** using the **latest .NET (LTS)** and **Angular (latest stable)**, optimized for **Microsoft Azure**, following **Clean Architecture** and the **Backend-for-Frontend (BFF) pattern**. The platform must support **separate repositories**, strong security, scalability, and allow **frontend swapping (Angular → React)** without backend changes.

### 1.3 Target Users

* Internal engineering teams
* Full-stack developers
* Cloud-native application teams
* Architects creating reusable application foundations

### 1.4 Goals

* Provide a reusable, opinionated but flexible starter architecture
* Enforce best practices for security, maintainability, and scalability
* Minimize coupling between frontend and backend
* Optimize for Azure deployment and operations

### 1.5 Non-Goals

* No business-specific domain logic
* No UI/UX branding beyond a basic responsive layout
* No mobile app (future extension)

---

## 2. Success Metrics

* Backend can serve multiple frontends without modification
* Angular frontend can be replaced with React using the same BFF contracts
* Services deploy successfully to Azure with minimal configuration
* New feature modules can be added with minimal architectural changes

---

## 3. High-Level Architecture

### 3.1 System Components

1. **Angular Frontend** (Primary UI)
2. **BFF API (.NET Web API)** – frontend-specific adapter
3. **Core Backend API (.NET Web API)** – domain and business logic
4. **Azure Infrastructure** – hosting, security, monitoring

### 3.2 Architecture Pattern

* Clean Architecture (Core API)
* Backend-for-Frontend (BFF)
* REST-based communication
* Environment-based configuration

### 3.3 Architecture Diagram (Logical)

```
[ Angular App ]
       |
       v
[ BFF API (.NET) ]
       |
       v
[ Core API (.NET) ]
       |
       v
[ Azure SQL ]
```

---

## 4. Functional Requirements

### 4.1 Core Backend API (.NET)

#### Description

Provides domain-driven, frontend-agnostic business logic and data access. Consumed only by the BFF.

#### Requirements

* Use **latest .NET LTS**
* Clean Architecture layers:

  * Domain
  * Application
  * Infrastructure
  * API
* RESTful endpoints
* SOLID principles enforced
* EF Core with Azure SQL
* DTO-based input/output
* Auto-mapping strategy
* Global exception handling
* API versioning
* Health checks
* Swagger / OpenAPI
* Authentication-ready (JWT, Azure AD compatible)

---

### 4.2 BFF API (.NET)

#### Description

Acts as a secure gateway and adapter between frontend and Core API.

#### Responsibilities

* Authentication and token handling
* API aggregation and orchestration
* Response shaping for Angular
* Caching (in-memory; Redis-ready)
* Rate limiting
* CORS enforcement (frontend-specific)

#### Requirements

* No business logic
* Calls Core API via HTTP client
* Angular-specific DTOs allowed
* Azure AD / Entra ID ready
* Secure cookie or token-based auth

---

### 4.3 Frontend – Angular (Primary)

#### Description

Primary user interface built with Angular and optimized for enterprise scalability.

#### Requirements

* Angular (latest stable)
* Standalone APIs
* Signals-first state management
* Modular folder structure
* Environment-based API configuration
* Auth via BFF
* HTTP interceptors
* Global error handling
* Responsive layout

#### Core Screens

* Login
* Dashboard
* Example CRUD feature

---

## 5. Frontend Swap Strategy (Angular → React)

### Principles

* Backend APIs remain unchanged
* BFF contracts remain stable
* Only frontend implementation changes

### What Stays the Same

* Core API
* BFF API
* Authentication model
* OpenAPI contracts

### What Changes

* Frontend repo
* UI state management
* Component implementation

---

## 6. Non-Functional Requirements

### 6.1 Security

* JWT authentication
* Azure AD ready
* Secure secrets via Azure Key Vault
* HTTPS enforced

### 6.2 Performance

* API response shaping via BFF
* Caching support
* Optimized Azure App Service hosting

### 6.3 Scalability

* Stateless services
* Horizontal scaling on Azure
* Database scalability via Azure SQL tiers

### 6.4 Maintainability

* Clear separation of concerns
* Consistent folder structure
* Strong typing and DTO boundaries

---

## 7. Azure Optimization Requirements

### Azure Services

* Azure App Service (Linux)
* Azure SQL Database
* Azure Key Vault
* Managed Identity
* Application Insights
* Azure API Management (optional)

### Environments

* Local
* Development
* QA
* Production

---

## 8. Repository Strategy (Separate Repos)

### 8.1 Core API Repo – `core-api`

```
/src
  /Api
  /Application
  /Domain
  /Infrastructure
/tests
/.github
README.md
```

### 8.2 BFF API Repo – `bff-api`

```
/src
  /Api
  /Adapters
  /Clients
/tests
/.github
README.md
```

### 8.3 Angular Frontend Repo – `angular-frontend`

```
/src
  /app
    /core
    /features
    /shared
/environments
/.github
README.md
```

---

## 9. Deployment & Operations

### Local Development

* Run services independently
* Environment-based configs
* Swagger for API validation

### CI/CD

* GitHub Actions or Azure DevOps
* Build, test, and deploy per repo

### Monitoring

* Application Insights
* Centralized logging

---

## 10. Risks & Mitigations

| Risk             | Mitigation            |
| ---------------- | --------------------- |
| Tight coupling   | Enforce BFF boundary  |
| Auth complexity  | Azure AD–ready design |
| Over-engineering | Minimal starter scope |

---

## 11. Future Enhancements

* React frontend repo
* Mobile app (BFF reuse)
* GraphQL BFF option
* Microservices decomposition
* Redis caching
* Infrastructure as Code (Bicep/Terraform)

---

## 12. Acceptance Criteria

* Platform deploys successfully to Azure
* Angular app communicates only with BFF
* BFF communicates only with Core API
* Frontend swap requires no backend changes
* Repos usable as GitHub starter templates

---

**End of PRD**
