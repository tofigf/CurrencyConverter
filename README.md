# README #
# Currency Converter Service

This project is a multi-access currency conversion service built with .NET 8.

## Why this project?

It was created as a test task to demonstrate clean architecture, gRPC usage, and working with external APIs and PostgreSQL in a real-world service setup.

## What's inside?

- **Core Logic** for converting currencies using [https://api.exchangerate.host](https://api.exchangerate.host)
- **PostgreSQL** database for caching rates and saving conversion history
- **gRPC Service** for fast and structured communication between services
- **REST API** as a simple HTTP interface
- **Console Client** to manually test currency conversion
- **Unit Tests** using xUnit and EF Core InMemory

## gRPC

The gRPC service defines a `Convert` method using Protobuf and allows clients to request conversions with high performance.

```proto
dotnet ef migrations add InitialCreate --project CurrencyConverter.Infrastructure --startup-project CurrencyConverter.RestApi
dotnet ef database update --project CurrencyConverter.Infrastructure --startup-project CurrencyConverter.RestApi

