# README #

dotnet ef migrations add InitialCreate --project CurrencyConverter.Infrastructure --startup-project CurrencyConverter.RestApi
dotnet ef database update --project CurrencyConverter.Infrastructure --startup-project CurrencyConverter.RestApi

