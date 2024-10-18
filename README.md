This repo demonstrates a difference in how an Azure Functions app (dotnet 8, isolated worker) loads configuration from a json file.

My key observation is that configuration is not reloaded on change to a json file in a dotnet 8 isolated worker function app.

# To observe web api config reload behavior
1. From a terminal in the ConfigReload.API directory, run `dotnet run` to launch the api.
2. Execute a get request to [http://localhost:5283/configreload](http://localhost:5283/configreload). Observe the returned values.
3. Modify the `"Foo"` value in `appsettings.Development.json` and save the file.
4. Execute another get request to [http://localhost:5283/configreload](http://localhost:5283/configreload). Observe the response and notice that the values for the IOptionsMonitor and IConfiguration have changed.

# To observe Azure Functions config reload behavior
1. From a terminal in the ConfigReload.Function directory, run `func start` to launch the function app.
2. Execute a get request to [http://localhost:5283/configreload](http://localhost:7071/api/HttpFunction). Observe the returned values.
3. Modify the `"Foo"` value in `local.settings.json` and save the file.
4. Execute another get request to [http://localhost:5283/configreload](http://localhost:7071/api/HttpFunction). Observe the respnse and notice that the values for the IOptionsMonitor and IConfiguration have **not** changed.
