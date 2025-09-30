# Secure, Scalable RESTful API with .NET

This repository contains a sample project demonstrating a minimal e-commerce product catalog API built with .NET, following Clean Architecture principles, and integrated with a simple front-end client.

## Project Overview

The goal of this project is to showcase the design and implementation of a performant, secure RESTful API using C# and .NET, integrated with a simple front-end, and developed using Test-Driven Development (TDD) principles. It highlights best practices in asynchronous programming, dependency injection, and web security.

---

## Project Structure

The solution is organized into several projects following Clean Architecture principles:

-   `TallerChallenge.Api`: The .NET Web API project that exposes the RESTful endpoint. It handles HTTP requests, authentication, and responses.
-   `TallerChallenge.Application`: Contains the application logic, including query handlers (CQRS pattern) and data transfer objects (DTOs).
-   `TallerChallenge.Domain`: The core of the application, containing the `Product` domain entity.
-   `TallerChallenge.Repository`: The data access layer. For this project, it provides a hardcoded list of products.
-   `TallerChallenge.WebApp`: A simple front-end application built with vanilla HTML, CSS, and JavaScript to display the products.
-   `TallerChallenge.Tests`: A collection of xUnit tests covering the domain, application, repository, and API layers to ensure correctness and maintainability.

---

## Backend API

The backend is a .NET Web API that exposes a single endpoint to retrieve a list of products.

### API Documentation

The API is documented using Swagger (OpenAPI). When the API is running in a development environment, you can access the Swagger UI to view endpoints, test them, and see the API schema.

-   **Swagger UI:** `https://localhost:7219/swagger` (adjust the port if necessary)

This provides comprehensive details about the available endpoints, required headers, and response models.

### Implementation Details

-   **Asynchronous Processing:** The endpoint and underlying service layers use `async/await` for non-blocking I/O.
-   **Dependency Injection:** Services, such as the product repository, are registered in the DI container and injected into controllers and handlers.
-   **Security:** A custom `ApiKeyAuthAttribute` secures the endpoint. The required API key is hardcoded for this challenge as `12345`.
-   **CORS Policy:** For simplicity in this project, the CORS policy is configured to allow any origin to facilitate testing from a local `index.html` file. In a production environment, this would be restricted to known, trusted domains.

---

## Front-End

The front-end is a single-page application that fetches and displays product data from the API.

### Features

-   **Product Display:** Fetches products from the API on page load and displays them in a responsive table.
-   **Error Handling:** Displays a clear error message if the API call fails (e.g., due to an invalid API key or network issues).
-   **Technology:** Built with vanilla JavaScript (`fetch` API), HTML, and CSS, with no external frameworks.

### Configuration

The API URL and key must be configured in `TallerChallenge.WebApp/config.js`:

```javascript
const AppConfig = {
    apiKey: '12345',
    apiUrl: 'https://localhost:7219/api/products' // Adjust the port if necessary
};
```

> **Note:** Storing the API key in a client-side JavaScript file like `config.js` is insecure and not suitable for production. This approach makes the key publicly accessible. In a real-world application, authentication should be handled securely, for example by using a Backend-For-Frontend (BFF) to manage API keys, or implementing a token-based authentication system (e.g., OAuth 2.0 with Bearer tokens).

---

## Testing

The solution includes a comprehensive suite of unit tests written with xUnit to ensure the reliability of each layer.

-   **Domain Tests:** Validate the business rules of the `Product` entity.
-   **Application Tests:** Verify the logic within the `GetProductsQueryHandler`.
-   **Repository Tests:** Ensure the mock data repository functions as expected.
-   **API Tests:** Test the `ProductController` responses and the `ApiKeyAuthAttribute` security logic.

---

## How to Run the Project

### 1. Run the Backend API

You can run the API using your preferred .NET IDE (like Visual Studio, JetBrains Rider, or VS Code) or from the command line.

#### Using an IDE (e.g., Visual Studio)

1.  Open the solution file (`.sln`).
2.  Set `TallerChallenge.Api` as the startup project.
3.  Run the project (e.g., by pressing `F5` in Visual Studio). The API will be available at the URL specified in `launchSettings.json` (e.g., `https://localhost:7219`).

#### Using the .NET CLI

1.  Navigate to the `TallerChallenge.Api` directory in your terminal.
2.  Run the command: `dotnet run`.
3.  The API will be available at the URL specified in `launchSettings.json`.

### 2. Run the Front-End

1.  Ensure the `apiUrl` in `TallerChallenge.WebApp/config.js` matches the running API's URL.
2.  Open the `TallerChallenge.WebApp/index.html` file directly in a web browser.

### 3. Run the Tests

#### Using an IDE

1.  Open the **Test Explorer** in your IDE.
2.  Click **Run All Tests** to execute the entire test suite.

#### Using the .NET CLI

1.  Navigate to the root directory of the solution.
2.  Run the command: `dotnet test`.
