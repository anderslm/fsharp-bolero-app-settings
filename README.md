# FSharp-Bolero-app-settings
This is an example of how to use appsettings.{env}.json in Bolero clients.

To run the example just type: `# dotnet run -p src/FSharp.Bolero.Server`

Try out the test environment: `# ASPNETCORE_ENVIRONMENT=Test dotnet run -p src/FSharp.Bolero.Server`

## How it works
In order for Blazor to know which appsettings file to use it requires a header, Blazor-Environment, in the request. This is accomplished in `Startup.fs`:
```
.Use(fun context next ->
        context.Response.Headers.Add("Blazor-Environment", env.EnvironmentName |> StringValues)
        next.Invoke())
```
