# Product Requirements Document (PRD)

## 1. Overview

### 1.1 Product Name

Enterprise Full-Stack Platform (Angular 21 | React 19 | Vue 3 | .NET 8)

### 1.2 Purpose

Build a **production-ready, enterprise-grade starter platform** using the **latest .NET API (Core & BFF)** and three interchangeable modern frontends (**Angular, React, Vue**). The platform follows **Clean Architecture** and the **Backend-for-Frontend (BFF) pattern**, managed within a **Monorepo** for unified development and testing.

### 1.3 Target Users

* Internal engineering teams
* Full-stack developers
* Cloud-native application teams
* Architects creating reusable application foundations

### 1.4 Goals

* Provide a reusable, opinionated but flexible starter architecture.
* Enforce best practices for security, maintainability, and scalability.
* Minimize coupling between frontends and backend via a unified BFF.
* Provide choice of frontend framework (Angular, React, or Vue) with zero backend changes.
* Maintain 100% test coverage across all layers.

### 1.5 Non-Goals

* No business-specific domain logic beyond identity/auth.
* No UI/UX branding beyond a premium, responsive glassmorphism design.
* No native mobile app (future extension via Maui/React Native).

---

## 2. Success Metrics

* Single backend (BFF) serves three different frontends (Angular, React, Vue) without modification.
* All frontends share the same security model (Secure Cookies/BFF).
* CI/CD pipelines can build and test the entire monorepo.
* All 30+ tests across the platform are passing (Green status).

---

## 3. High-Level Architecture

### 3.1 System Components (Monorepo)

1.  **Frontends (`/frontends`)**:
    *   **Angular Frontend** (v21.1.0 - Signals-based)
    *   **React Frontend** (v19.0.0 - Context-based)
    *   **Vue Frontend** (v3.5.0 - Pinia-based)
2.  **BFF API (`/bff-api`)** – .NET 8 frontend-specific orchestrator (Security & Aggregation).
3.  **Core Backend API (`/core-api`)** – .NET 8 domain and business logic (Persistence & CQRS).
4.  **Azure Infrastructure** – Hosting via App Services, Security via Entra ID/Key Vault.

### 3.2 Architecture Pattern

* **Clean Architecture** (Core API)
* **Backend-for-Frontend (BFF)** (Secure Cookie Auth)
* **CQRS with MediatR** (Internal logic)
* **Unified Design System** (Global CSS Variables)

### 3.3 Architecture Diagram (Logical)

```text
[ Angular App ]   [ React App ]   [ Vue App ]
       |                |               |
       +----------------+---------------+
                        |
                        v
               [ BFF API (.NET 8) ]
                        |
                        v
              [ Core API (.NET 8) ]
                        |
                        v
                [ Persistence/SQL ]
```

---

## 4. Functional Requirements

### 4.1 Core Backend API (.NET 8)

* **Clean Architecture Layers:** Domain (Entities), Application (Logic/CQRS), Infrastructure (Persistence), API (Delivery).
* **Security:** Password hashing via BCrypt.Net-Next.
* **Patterns:** CQRS with MediatR 14, Repository Pattern.
* **Documentation:** Swagger/OpenAPI.
* **Testing:** xUnit, Moq, FluentAssertions.

### 4.2 BFF API (.NET 8)

* **Auth Orchestration:** Secure Session Cookies (SameSite=Lax, HttpOnly).
* **Communication:** Typed HTTP Clients for Core API.
* **Cross-Cutting:** Unified error handling, CORS enforcement.
* **Testing:** xUnit, Integration tests for auth flows.

### 4.3 Frontends (Interchangeable)

* **Shared Design:** Premium Glassmorphism UI using global CSS tokens.
* **Angular 21:** Signals-first state, Standalone components, New Control Flow.
* **React 19:** Functional components, Context API, Lucide icons.
* **Vue 3:** Composition API, Pinia state, Lucide icons.
* **Common Features:** Login, Signup, Dashboard, Auth Interceptors.

---

## 5. Monorepo Strategy

### 5.1 Structure

```text
/angular-frontend    - Angular 21 project
/react-frontend      - React 19 project (Vite)
/vue-frontend        - Vue 3 project (Vite)
/bff-api             - .NET 8 BFF Service
/core-api            - .NET 8 Core Domain Service
/shared-assets       - Global CSS/Icons (conceptually shared)
PRD.md               - This document
TASKS.md             - Tracking
```

### 5.2 Benefits

* **Atomic Commits:** Update API and UI in a single PR.
* **Shared Logic:** Easier to maintain consistency across frontends.
* **Unified CI:** Single pipeline to verify the whole platform.

---

## 6. Non-Functional Requirements

### 6.1 Security

* **Secure Cookies:** No JWTs in LocalStorage (prevents XSS leaks).
* **BFF Pattern:** Hides internal API structure from the public web.
* **Encryption:** BCrypt for credentials.

### 6.2 Visual Excellence (Aesthetics)

* **Premium UI:** Glassmorphism, smooth gradients, and micro-animations.
* **Responsiveness:** Mobile-first, fluid layouts across all three frameworks.
* **Typography:** Modern sans-serif stacks (Inter/Outfit).

### 6.3 Performance

* **Vite:** High-speed development for React/Vue.
* **Angular Signals:** Fine-grained reactivity for performance.
* **Async/Await:** Non-blocking operations throughout the .NET stack.

---

## 7. Deployment & Operations

### CI/CD (GitHub Actions)
* Build and Test all 5 projects.
* Enforce "All Green" status before merge.

---

## 8. Current Project Status

| Project | Health | Framework |
| :--- | :--- | :--- |
| **Core API** | [dYY GREEN] | .NET 8 / xUnit |
| **BFF API** | [dYY GREEN] | .NET 8 / xUnit |
| **Angular** | [dYY GREEN] | v21 / Vitest |
| **React** | [dYY GREEN] | v19 / Vitest |
| **Vue** | [dYY GREEN] | v3 / Vitest |

---

## 9. Future Enhancements

* **Shared Component Library:** Create a headless UI library used by all three frontends.
* **Mobile Support:** Add a React Native or .NET Maui client.
* **Infrastructure as Code:** Bicep/Terraform for Azure provisioning.
* **Feature Flags:** Integration with Azure App Configuration.

---

**End of PRD**
