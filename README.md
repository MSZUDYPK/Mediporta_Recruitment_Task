# Mediporta Recruitment Task

REST API developed in .NET 8 and C#, based on the list of tags provided by the [StackOverflow API](https://api.stackexchange.com/docs).

## Installation

To install and run the application, use Docker Compose:

```bash
docker compose up
```

## Configuration

The application's behavior can be adjusted by modifying the `appsettings.json` file. One of the configurable settings is the `DataFetching` method.

The `DataFetching` setting controls whether the application fetches data from the StackExchange API on application start. By default, this setting is set to `true`, meaning the application will fetch data on start. If you don't want the application to fetch data when it starts, change this setting to `false`.

Here's an example of how to change this setting:

```json
"DataFetching": {
  "OnApplicationStart": false
}
```

Remember to save the `appsettings.json` file after making any changes. The new settings will take effect the next time the application is started.

## Usage

Once the application is running, you can interact with the API using Swagger UI. Swagger provides a web-based user interface that allows you to visualize and interact with the API’s resources.  To access Swagger UI, navigate to http://127.0.0.1:8080/swagger in your web browser.

Alternatively, you can also interact with the API using an [MediportaApp.http](./MediportaApp.http) file. This is a script file that can be executed from within JetBrains Rider (or Visual Studio Code with the REST Client extension installed). The .http file contains a series of HTTP requests that can be sent to the API. This can be a convenient way to test the API without needing to manually enter each request in a tool like Postman or Swagger.

### Interacting with the API Endpoints

The API provides several endpoints that you can interact with. Here are some examples of how to use them:

#### Fetching the Last Population

You can fetch the last population data by sending a GET request to the `/api/population` endpoint. This endpoint supports pagination and sorting.

- `page`: This parameter determines which page of the population data you want to fetch.
- `pageSize`: This parameter determines how many items are included on each page.
- `sort`: This parameter determines the attribute by which the results are sorted. The possible values are `name` and `share`.
- `order`: This parameter determines the order in which the results are sorted. The possible values are `asc` for ascending order and `desc` for descending order.

Here's an example of a request that fetches the first page of the population data, with a page size of 10, sorted by name in descending order:

```http
GET /api/population?page=1&pageSize=10&sort=name&order=desc HTTP/1.1
```

In this example, we're fetching the first page of the population data, with a page size of 10. The results are sorted by name in descending order.

#### Pulling Tags

You can pull 1000 tags by sending a POST request to the `/api/population` endpoint with empty body or empty JSON array.

```http
POST /api/population HTTP/1.1
Content-Type: application/json
```

#### Pulling Missing Tags

You can pull missing tags (or data) by sending a POST request to the `/api/population` endpoint. The body of the request should be a JSON array containing the additional names of the tags you want to pull. Here's an example:

```http
POST /api/population HTTP/1.1
Content-Type: application/json

[".net8", "mini"]
```

In this example, we're requesting to pull the tags ".net8", "mini".

**Additional tag names are merged with the previous population tag names to fetch up-to-date data and create a new population**

## Important Note

Please be aware that the application does not currently handle the throttling limits imposed by the StackExchange API. If the application exceeds these limits (more than 30 requests per second from a single IP), the API may respond with an undefined response, which could cause the application to return an Internal Server Error (HTTP 500).

## Running Tests

### Unit Tests

To run the unit tests, navigate to the tests directory and run the dotnet test command with the specific unit test project:

```bash
cd tests
```

```bash
dotnet test Mediporta.Tests.Unit
```

### Integration Tests

Similarly, to run the integration tests, navigate to the tests directory and run the dotnet test command with the specific integration test project:

```bash
cd tests
```

These tests require Docker to be installed and running on your machine. This is because the tests use Testcontainers to create isolated environments for each test.

**Please ensure Docker is running before executing the integration tests.**

```bash
dotnet test Mediporta.Tests.Integration
```