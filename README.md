# TodoApp - Backend - ASP.NET Web API

## Steps to run the Web API:

0. Change the connection string named `WebApiDatabase` in the `TodoApp.DbInit.Console` -> `appsettings.json`

1. Run the `TodoApp.DbInit.Console` console app, which creates the db (if it does not exist), and runs the db migration scripts

2. Run the `TodoApp.WebApi` app


## Steps to run the functional tests:

0. Change the connection string named `WebApiDatabase` in the `TodoApp.DbInit.Console` -> `appsettings.json` to your test database

1. Run the `TodoApp.DbInit.Console` console app, which creates the db (if it does not exist), and runs the db migration scripts

2. Run the tests using the Test Explorer in Visual Studio



Resources:

["Create a web API with ASP.NET Core"](https://docs.microsoft.com/en-us/aspnet/core/tutorials/first-web-api?view=aspnetcore-6.0&tabs=visual-studio) from ASP.NET Docs

[".NET 6.0 - User Registration and Login Tutorial with Example API"](https://jasonwatmore.com/post/2022/01/07/net-6-user-registration-and-login-tutorial-with-example-api) by Jason Watmore

["How to test your C# Web API"](https://timdeschryver.dev/blog/how-to-test-your-csharp-web-api) by Tim Deschryver


## On Testing

["Functional Test == Acceptance Test == End-to-End Test"](https://www.obeythetestinggoat.com/book/chapter_02_unittest.html)

> What I call functional tests, some people prefer to call acceptance tests, or end-to-end tests. The main point is that these kinds of tests look at how the whole application functions, from the outside. Another term is black box test, because the test doesn’t know anything about the internals of the system under test.

["integration (or functional) tests"](https://timdeschryver.dev/blog/why-writing-integration-tests-on-a-csharp-api-is-a-productivity-booster) by Tim Deschryver

["one good integration test is worth 1,000 unit tests"](https://khalidabuhakmeh.com/secrets-of-a-dotnet-professional) by Khalid Abuhakmeh

JsonException: The JSON value could not be converted to Enum - https://makolyte.com/jsonexception-the-json-value-could-not-be-converted-to-enum/


## If you want to add migration using Package Manager Console in Visual Studio 2022

```
add-migration AddTodoItemsTable -context DataContext -outputdir Migrations
```
