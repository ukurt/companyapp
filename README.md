# Company API

This project provides an API for managing company data, including CRUD operations and features like authentication and authorization.

## Authentication

The API uses JWT (JSON Web Token) for authentication and authorization. Below is the explanation of how the authentication works in this application:

### Authentication Flow

1. **Login**:
   - To access protected resources, a user must first authenticate using the login API endpoint. Upon successful authentication, the API will return a JWT token.
   - This token is then used for subsequent requests to protected endpoints.

2. **JWT Token**:
   - The JWT token contains user information and is signed with a secret key. It is sent with each request in the `Authorization` header in the format `Bearer <token>`.
   - The token is used by the API to verify the identity of the user and ensure that the user is authorized to access the requested resources.

3. **Protected Endpoints**:
   - Certain API endpoints are protected and require a valid JWT token for access. These endpoints include actions such as creating, updating, or deleting company data.

### JWT Authentication Implementation

- **Token Generation**: 
  - The JWT token is generated using user credentials (username and password) at the login endpoint.
  - Upon successful login, a token is created and returned.

- **Token Validation**:
  - Each request to a protected endpoint must include the JWT token in the `Authorization` header.
  - The API verifies the token on each request to ensure its validity.
  - If the token is invalid or expired, the API will return a 401 Unauthorized error.

### Endpoints

- **POST /api/auth/login**:
  - Used to authenticate the user and generate a JWT token.
  - Requires `username` and `password` in the request body.
  - Returns a JWT token.

- **GET /api/companies**:
  - Returns a list of all companies.
  - Requires a valid JWT token in the `Authorization` header.
  - Returns a 401 Unauthorized error if no valid token is provided.

- **POST /api/companies**:
  - Creates a new company.
  - Requires a valid JWT token in the `Authorization` header.
  - Returns a 401 Unauthorized error if no valid token is provided.

### Token Example

When sending requests to protected endpoints, include the `Authorization` header with the JWT token:

```http
Authorization: Bearer <your_jwt_token_here>


## Docker Setup

This project comes with a pre-configured Dockerfile to containerize the application for easy deployment. The Dockerfile is set up to build and run the API in a Docker container. Below are the instructions to build and run the Docker container.

### Dockerfile Explanation

The Dockerfile includes the following stages:

1. **Base Image**:
   - The base image used is `mcr.microsoft.com/dotnet/aspnet:8.0`, which is a lightweight image for running .NET applications.
   - The application will be run under the `app` user, and it exposes ports `8080` and `8081` for the application.

2. **Build Stage**:
   - The SDK image `mcr.microsoft.com/dotnet/sdk:8.0` is used to build the application. This stage involves:
     - Copying the necessary project files (`Company.Api.csproj`, `Company.Domain.csproj`, `Company.Infrastructure.csproj`) into the build context.
     - Restoring NuGet dependencies using `dotnet restore`.
     - Building the project using `dotnet build`.

3. **Publish Stage**:
   - The application is published to a folder using `dotnet publish`. This stage compiles the application into a self-contained deployment and prepares it to be run in the production environment.

4. **Final Stage**:
   - In the final stage, the published application is copied from the `publish` folder and placed into the `/app` directory.
   - The entry point for the container is set to run the `Company.Api.dll` application.

### Build and Run the Docker Container

To build and run the Docker container, follow these steps:

1. **Build the Docker Image**:
   In the root of your project (where the Dockerfile is located), run the following command to build the Docker image:

   ```bash
   docker build -t company-api .

1. **Run the Docker Container: Once the image is built, you can run it using the following command:**:

   ```bash
   docker run -d -p 8080:8080 -p 8081:8081 company-api


