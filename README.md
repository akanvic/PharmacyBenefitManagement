PBM Eligibility API

WHAT THIS PROJECT IS
--------------------
This project is a production-ready ASP.NET Core Web API designed for a Pharmacy Benefit Manager (PBM) system.

Its core responsibility is simple and critical:
Given a member ID, return their eligibility status and a summary of recent claims securely, efficiently, and at scale.

The API is built with real healthcare constraints in mind:
- PHI/PII protection
- Auditability
- Predictable performance under heavy load
- Clean separation between business rules and infrastructure


ARCHITECTURE OVERVIEW
---------------------
The solution follows Clean Architecture principles to keep responsibilities clear and the system easy to maintain.

Project Structure:

PBM.EligibilityApi/
- Controllers   : HTTP endpoints (thin by design)
- Services      : Business rules and orchestration
- Repo          : EF Core context and repositories
- Domain        : Core domain entities
- DTOs          : API contracts (what is exposed externally)
- Exceptions    : Business and domain errors
- Middleware    : Logging, errors, and cross-cutting concerns

Why this structure?
- Controllers stay simple
- Business logic lives in one place
- Data access is replaceable
- The codebase remains testable as it grows


CORE DESIGN DECISIONS
--------------------

Repository Pattern
- Abstracts data access from business logic
- Makes unit testing easy
- Allows future data source changes

Service Layer
- Encapsulates all business rules
- Keeps controllers thin
- Encourages reuse and clarity

DTO Usage (Non-Negotiable)
- Prevents PHI exposure
- Avoids over-posting attacks
- Keeps API contracts stable
- Allows safe versioning


ENTITY FRAMEWORK CORE OPTIMIZATIONS
----------------------------------

AsNoTracking()
- Reduces memory usage
- Improves performance for read-only queries
- Ideal for eligibility lookups

Projection with Select()
- Fetches only required columns
- Reduces payload size
- Improves query performance

Explicit Loading with Include()
- Avoids N+1 query issues
- Ensures predictable database access
- Uses a single round-trip per request


HEALTHCARE-SPECIFIC CONSIDERATIONS
---------------------------------

PHI / PII Protection
- Every request is audited
- Only required fields are returned
- Masking hooks are ready for sensitive fields
- Designed with HIPAA-style compliance in mind

Data Integrity
- Claims are soft-deleted, never removed
- All entities track creation and modification timestamps
- Claims reference historical plan snapshots for accuracy


SCALABILITY AND PERFORMANCE
---------------------------

Database Indexing
Indexes support common access patterns like:
- Member eligibility checks
- Recent claim lookups
- Plan resolution

Query Strategy
- Pagination for claims
- Filtered includes
- Optimized projections

Caching Strategy
- Plan data is cacheable due to low change frequency
- Response caching planned for future phases


ERROR HANDLING
--------------

Global Exception Middleware
- Centralized error handling
- Consistent error responses
- No sensitive details exposed
- Structured logging for troubleshooting

Custom Business Exceptions
- Clear separation of business vs technical errors
- Predictable HTTP status codes


SECURITY CONSIDERATIONS
----------------------

Authentication and Authorization
- Designed for policy-based access control
- Ready for OAuth 2.0 / Azure AD B2C integration

Rate Limiting
- Planned to prevent abuse and data exposure

Input Validation
- Strict member ID validation
- Parameterized queries
- Request validation middleware


ASSUMPTIONS
-----------

Business Rules
- A member is eligible if active and within effective dates
- Recent claims are limited to the most recent 10
- Voided claims are excluded

Data Model
- Single primary member identifier
- Members can have multiple plans over time
- Claims are directly linked to members
- All actions capture audit context

Technical Expectations
- SQL Server 2019+ or PostgreSQL 12+
- 1000+ concurrent users
- Millions of members
- Sub-200ms response times


TRADEOFFS
---------

Denormalization
- Claims store plan snapshots for historical accuracy
- Increased storage is accepted for correctness

DTO-Only API
- More mapping code
- Stronger security and contract stability

Async Everywhere
- Slight complexity increase
- Essential for scalability


PRODUCTION ENHANCEMENTS (NEXT PHASE)
-----------------------------------

High Priority
- OAuth 2.0 / Azure AD B2C
- Rate limiting
- Redis caching
- Health checks
- Swagger documentation

Observability
- Application Insights
- Structured logging
- Metrics and alerting

Data Protection
- Encryption at rest and in transit
- Field-level encryption
- Long-term audit log retention


RUNNING THE APPLICATION
-----------------------

Requirements
- .NET 8 SDK
- SQL Server or PostgreSQL
- Visual Studio or VS Code

Setup Steps
1. Clone the repository
2. Update connection strings
3. Run migrations
4. Seed data
5. Start the application


API EXAMPLE
-----------

GET /api/members/{memberId}/eligibility-summary

Successful Response
- Returns eligibility status, active plan, and recent claims

Error Response
- Returns standardized error format with trace ID


METADATA
--------
Version: 1.0
Last Updated: 2026
Author: Akaninyene Uwah
