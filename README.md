# 🚀 Enterprise Full-Stack Platform
> **.NET 8** | **Angular 21** | **React 19** | **Vue 3**

This is a production-ready, enterprise-grade starter platform featuring a **Backend-for-Frontend (BFF)** architecture with three interchangeable modern frontend implementations. The system is built on .NET 8, following Clean Architecture principles and optimized for scalable, secure deployment.

---

## 🛠 Technology Stack

### 🌍 Global
- **Framework Host:** .NET 8.0 SDK
- **Runtime:** Node.js v20+ / npm v10+
- **Languages:** C# 12, TypeScript 5.9, HTML5, CSS3

### ⚙️ Backend (Core API & BFF API)
- **Framework:** ASP.NET Core 8.0
- **Architectural Patterns:** Clean Architecture, CQRS (MediatR 14.0.0), BFF Pattern
- **Security:** BCrypt.Net-Next (Password Hashing), Session Cookies (SameSite=Lax), CORS
- **Documentation:** Swagger/OpenAPI (Swashbuckle 6.6.2+)
- **Testing:** xUnit, Moq, FluentAssertions

### 🎨 Frontends (Modular & Interchangeable)
- **Angular:** v21.1.0 (Signals, Standalone Components, New Control Flow)
- **React:** v19.0.0 (Hooks, Context API, Lucide Icons)
- **Vue:** v3.5.0 (Pinia, Composition API, Lucide Icons)
- **Design System:** Shared Premium CSS variables with Glassmorphism
- **Testing:** Vitest, React Testing Library, Vue Test Utils

---

## 📂 Project Components

### 1. Core API (`/core-api`)
The foundational domain service. Handles all business rules and data persistence.
- **Key Features:** MediatR CQRS, Persistence isolation, Password Hashing.

### 2. BFF API (`/bff-api`)
The Backend-for-Frontend orchestrator.
- **Key Features:** Secure Cookie Management, OAuth Scaffolding, Unified Error Handling.

### 3. Frontends
Three modern implementations sharing the same API contract and logic.
- **Angular:** High performance with reactive signals.
- **React:** Functional approach with Context-based state.
- **Vue:** Scalable state with Pinia and Composition API.

---

## 🚀 Getting Started

### 1. Start Core API
```bash
cd core-api
dotnet run --project src/Api/CoreApi.Api.csproj
# Running at http://localhost:5279
```

### 2. Start BFF API
```bash
cd bff-api
dotnet run --project src/Api/BffApi.Api.csproj
# Running at http://localhost:5084
```

### 3. Start a Frontend
| Framework | Command | URL |
| :--- | :--- | :--- |
| **Angular** | `cd angular-frontend && npm install && ng serve` | `http://localhost:4200` |
| **React** | `cd react-frontend && npm install && npm run dev` | `http://localhost:5173` |
| **Vue** | `cd vue-frontend && npm install && npm run dev` | `http://localhost:5174` |

---

## 🏗 Architecture
The Frontends communicate **exclusively** with the BFF API. The BFF handles all security-sensitive operations and encapsulates the complexity of the internal Core API services. This ensures:
- **Security:** No sensitive tokens reach the browser.
- **Simplicity:** Frontends interact with a tailor-made API.
- **Independence:** Core services can evolve without breaking the UI.

---

## 📊 Project Status (Monorepo)
> All tests across the entire monorepo are currently **GREEN**.

```text
+-----------------------+-------------------+----------------------------+
| Project               | Test Status       | Framework / Runner         |
+-----------------------+-------------------+----------------------------+
| Core API              | [🟢 PASSED (7)]   | .NET 8 / xUnit             |
| BFF API               | [🟢 PASSED (3)]   | .NET 8 / xUnit             |
| Angular Frontend      | [🟢 PASSED (15)]  | Angular 21 / Vitest        |
| React Frontend        | [🟢 PASSED (3)]   | React 19 / Vitest          |
| Vue Frontend          | [🟢 PASSED (2)]   | Vue 3 / Vitest             |
+-----------------------+-------------------+----------------------------+
| OVERALL HEALTH        | [🟢 GREEN]        | All 30 Tests Passing       |
+-----------------------+-------------------+----------------------------+
```
