# Task List

- [x] Install Prerequisites
    - [x] Install .NET SDK 8
    - [x] Install Angular CLI / Vite
- [x] Initialize Project Structure (Monorepo)
    - [x] Create folder structure (`core-api`, `bff-api`, `angular-frontend`, `react-frontend`, `vue-frontend`)
    - [x] Create README.md, PRD.md, TASKS.md
- [x] Implement Core API
    - [x] Create .NET Web API solution/project in `core-api`
    - [x] Setup Clean Architecture layers (Domain, Application, Infrastructure, Api)
    - [x] Implement CQRS with MediatR
    - [x] Implement User Domain & Persistence (BCrypt hashing)
    - [x] Add Unit Tests (xUnit)
- [x] Implement BFF API
    - [x] Create .NET Web API solution/project in `bff-api`
    - [x] Configure as Gateway/Adapter
    - [x] Implement Secure Cookie Auth (Login, Signup, Logout, Me)
    - [x] Add Unit Tests (xUnit)
- [x] Implement Angular Frontend (v21)
    - [x] Initialize Angular App
    - [x] Implement Premium Glassmorphism UI
    - [x] Add Auth Service (Signals) & Components
    - [x] Add Unit Tests (Vitest)
- [x] Implement React Frontend (v19)
    - [x] Initialize React App (Vite)
    - [x] Implement Premium Glassmorphism UI (matching Angular)
    - [x] Add Auth Management (Context API)
    - [x] Add Unit Tests (Vitest)
- [x] Implement Vue Frontend (v3)
    - [x] Initialize Vue App (Vite)
    - [x] Implement Premium Glassmorphism UI (matching others)
    - [x] Add Auth Management (Pinia)
    - [x] Add Unit Tests (Vitest)
- [x] Dev Preview & Verification
    - [x] Verify all 3 frontends connect to BFF
    - [x] Verify Secure Cookie persistence across frameworks
    - [x] Run all 30+ tests (All Green)
- [x] GitHub Integration
    - [x] Initialize Git Repository
    - [x] Resolve remote conflicts (README/LICENSE)
    - [x] Push entire Monorepo to GitHub (`origin/main`)
    - [x] Verify visibility of all components on GitHub

## Next Steps

- [ ] Implement shared CSS component library (to reduce duplication)
- [ ] Add GitHub Actions workflows for automated testing
- [ ] Setup Azure Deployment (App Service/SQL)
