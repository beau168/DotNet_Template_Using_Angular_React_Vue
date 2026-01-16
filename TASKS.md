# Task List

- [x] Install Prerequisites
    - [x] Install .NET SDK 8
    - [x] Install Angular CLI
- [x] Initialize Project Structure
    - [x] Create folder structure (`core-api`, `bff-api`, `angular-frontend`)
    - [x] Create README.txt
- [x] Implement Core API
    - [x] Create .NET Web API solution/project in `core-api`
    - [x] Setup Clean Architecture layers (Domain, Application, Infrastructure, Api)
    - [x] Add basic Health Check and Swagger
- [x] Implement BFF API
    - [x] Create .NET Web API solution/project in `bff-api`
    - [x] Configure as Gateway/Adapter
    - [x] Add basic Health Check and Swagger
- [x] Implement Angular Frontend
    - [x] Initialize Angular App in `angular-frontend`
    - [x] Setup folder structure (core, features, shared)
    - [x] Create basic Login and Dashboard components
- [x] Verification (Initial Setup)
    - [x] Verify Core API runs
    - [x] Verify BFF API runs
    - [x] Verify Angular App runs

## Authentication Feature (New)

- [x] Core API: User Domain & Persistence
    - [x] Create `User` and `RefreshToken` entities in Domain
    - [x] Implement `IUserRepository` (In-Memory/SQLite)
    - [x] Implement Password Hashing Service
    - [x] Add `CreateUser` and `ValidateUser` features (Commands/Queries)
- [x] BFF API: Auth Orchestration
    - [x] Implement Auth Controller (Login, Signup, Logout, Me)
    - [x] Setup Session Management (Cookie/JWT)
    - [x] Implement OAuth Handler (Mock/Real strategy)
    - [x] Connect BFF to Core API User endpoints
- [x] Angular Frontend: Auth UI
    - [x] Implement `AuthService` (Signals)
    - [x] Update `LoginComponent` with Form & OAuth buttons
    - [x] Implement `SignupComponent`
    - [x] Add Auth Interceptor
- [x] Verification (Auth)
    - [x] Verify Local Signup & Login flow
    - [x] Verify OAuth Redirect flow
    - [x] Verify Session Persistence
