# WeddingPlanner
WeddingPlanner API and application.

## Prerequisities

* Visual Studio 2019
* SQL Server

## Getting Started

1. Clone repository. Open solution file in Visual Studio.
2. Restore nuget packages.
3. Build solution.
4. Setup SQL database - open Infrastructure project folder, open PowerShell and run command:
```
dotnet ef --startup-project ..\WeddingPlanner.Api\ database update
```
5. Run application.

## Built With

* .NET 5.0
* Swagger
* Automapper
* Autofac
* Entity Framework Core

## API

API Documentation was created using the [Swagger](https://aspnetcore.readthedocs.io/en/stable/tutorials/web-api-help-pages-using-swagger.html). It is available at url: _{api_url}/swagger_

Postman collection with API request examples is available in the repo root - [WeddingPlanner.postman_collection](WeddingPlanner.postman_collection.json).

### Authentication

API uses JWT for authentication.

Roles - TODO

#### Register

**Endpoint:**

```
{api_url}/api/authentication/register
```

Request body:

```json
{
  "username": "TestUser",
  "email": "user@example.com",
  "password": "TestUser123$"
}
```

Response body:
```json
{
  "result": true,
  "status": "Success",
  "message": "string",
  "item": {
    "username": "string",
    "email": "string"
  },
  "token": "string",
  "expiration": "2021-01-19T23:48:48.943Z"
}
```

#### Login

**Endpoint:**

```
{api_url}/api/authentication/login
```

Request body:

```json
{
  "username": "TestUser",
  "password": "TestUser123$"
}
```

Response body:
```json
{
  "result": true,
  "status": "Success",
  "message": "string",
  "item": {
    "username": "string",
    "email": "string"
  }
}
```

### Key Features

#### Guests

* Get all guests
* Create new guest
* Get guests by age param

#### Wedding Hall

* Get wedding hall by id
* Create new wedding hall
* Edit wedding hall

#### Outfits

* Add outfit
* Update outfit
* Get outfit by id

#### Wedding Services

TODO

## Implementation Details

TODO

### Caching

#### In-Memory Cache

[NOT IMPLEMENTED YET]

#### Distributed Cache

[NOT IMPLEMENTED YET]

### Logging

Logging  was implemented using the [log4net](https://logging.apache.org/log4net/) library. Log data is saved to the _main.log_ file (_.\WeddingPlanner.Api\bin\Debug\netcoreapp5\main.log_)

## Data storage

MS SQL Server - TODO

## Testing

**WeddingPlanner.Tests** - dedicated project for unit & integration tests.

Technologies used:

* xunit
* FluentAssertions
* Moq

## Author

* **Jakub Wajs** - [github](https://github.com/kubawajs)
