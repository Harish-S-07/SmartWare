# SmartWare - Inventory Management System

SmartWare is a microservices-based inventory management system built with .NET Core using Clean Architecture. It demonstrates modern practices such as polyglot persistence, event-driven communication with Kafka, CQRS with MediatR, and dockerization.

**Project Structure**

- **AuthService** – Authentication and user management

- **ProductService** – Product catalog management with PostgreSQL and Kafka producer

- **InventoryService** – Inventory tracking with MongoDB and Kafka consumer

- **smartware-frontend** – React-based simple login page

- **docker-compose.yml** – Multi-service container setup with SQL Server, PostgreSQL, MongoDB, Kafka, Redis

**Stakeholders and Roles**

- Admin – Full control over users, products, and inventory
- Inventory Manager – Manages stock updates and product availability
- User – Can log in and access assigned product-related operations

**Implemented Features**

**Authentication**

- JWT-based login and registration
- Redis integration for token blacklist
- Role-based access using ASP.NET Core authorization

**Product Service**

- Full CRUD operations for products
- Kafka producer integration for publishing product events
- PostgreSQL database integration
- CQRS pattern with MediatR for handling commands and queries

**Inventory Service**

- Kafka consumer to listen for product events
- MongoDB database for inventory persistence
- Stock level management and updates

**Frontend**

- Basic React-based login page with JWT authentication

**Database Integration**

- AuthService – SQL Server
- ProductService – PostgreSQL
- InventoryService – MongoDB

**Testing**

- Swagger UI for API documentation and testing
- xUnit test projects for backend services

**Security**

- Secure 32+ character secret key for JWT
- Token validation middleware in each service
- Role-based protection for APIs

**Getting Started**

**Prerequisites**

- .NET 8 SDK
- SQL Server, PostgreSQL, MongoDB
- Docker and Docker Compose
- Node.js for frontend

**Setup Instructions**

- Clone the repository
- Start services with Docker Compose: `docker-compose up --build`
- Run the frontend:

`cd smartware-frontend`

`npm install`

`npm start`

**Tech Stack**

- Backend: ASP.NET Core (.NET 8), Clean Architecture, CQRS, MediatR
- Databases: SQL Server, PostgreSQL, MongoDB
- Messaging: Apache Kafka
- Authentication: JWT, Redis
- Frontend: React
- DevOps: Docker, Docker Compose, GitHub Actions, Azure (planned)
- Testing: Swagger, xUnit

**Project Status**

- Authentication, Product, and Inventory services are functional
- Kafka integration between Product and Inventory services is complete
- Basic React login page implemented
- AuthService deployed to Azure
- Future scope: Extend frontend for product and inventory management

**Author**

**Harish Shanmugavelan**  

.NET Backend Developer (1.5+ years experience)  

**Email:** iharish7810@gmail.com  
**GitHub:** [https://github.com/Harish-S-07](https://github.com/Harish-S-07)  
**LinkedIn:** [https://www.linkedin.com/in/harishshanmugavelan](https://www.linkedin.com/in/harishshanmugavelan)