# eShop - Microservices Architecture with .NET 7 Web API and WebMVC Application
Welcome to the eShop project, a microservices architecture implemented using .NET 7 Web API. This project showcases the power and flexibility of microservices, with a WebMVC application under the Web folder that consumes the WebAppGateway.
  
## Technologies Used
- .NET 7 Web API: The foundation for building microservices.
- IdentityServer: Utilized in the Identity service for user authentication and authorization.
- YARP (Yet Another Reverse Proxy): Used in the WebAppGateway for reverse proxy functionality.
- Repository Pattern: Employed in all services for a consistent and modular approach to data access.
- Commands and Queries: Used for data operations, ensuring a separation of concerns.
- MSSQL: Database technology for most services.
- Entity Framework Core 7: ORM used with MSSQL for database operations.
- Redis: Used as a data store for the ShoppingCart service.
- RabbitMQ: Enables communication between services.
- MSTest: Used for unit testing.

## Service Details
### WebAppGateway
The main API gateway for the eShop project, responsible for routing and directing requests. All configuration, including authorization, is done in the application.json file.

### Catalog Service
Manages product information and provides data for the products in the eShop platform.

### Identity Service
Handles user authentication and authorization using IdentityServer, ensuring secure access to the eShop platform.

### Notification Services
 - Notification.Email: Sends email notifications to users.
 - Notification.SignalR: Manages real-time notifications using SignalR.

### Order Service
Manages the processing of orders, ensuring a smooth and reliable ordering experience for users.

### ShoppingCart Service
Holds the current state of the shopping cart, utilizing Redis as the data store for efficient and scalable cart management.

### WebMVC Application
A .NET 7 Web MVC application that consumes the WebAppGateway. Supports the following features:
 - Login/Logout: Users can log in and log out of the application.
 - Product Listing: Display a list of available products from the Catalog service.
 - Order Listing: Show a list of orders managed by the Order service.
 - Shopping Cart Management: View the current state of the shopping cart from the ShoppingCart service.
 - Adding Items to Cart: Users can add items to their shopping cart.
 - Checkout: Complete the purchase by checking out items from the shopping cart.

## Unit Testing
MSTest is used for unit testing to ensure the reliability and correctness of the codebase. Comprehensive unit tests are employed to validate the functionality of each service and promote robustness.

## Communication
All communication between services is facilitated through RabbitMQ, enabling seamless and asynchronous communication between different components of the eShop platform.

## Getting Started
To run the eShop project locally, follow these steps:

1. Clone the repository:
```
git clone https://github.com/kmehmedov/eShop.git
```
```
cd eShop
```
3. Build the project
```
docker compose build
```
5. Run the project
```
docker compose up
```
7. Navigate to
```
http://host.docker.internal:7212/
```
WINDOWS: Make sure you have the folling line in hosts file:
```
127.0.0.1 host.docker.internal
```

## Note
Please note that not all the services are fully implemented and unit tested.
