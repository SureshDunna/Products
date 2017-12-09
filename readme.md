# Products
This api provides list of operations to query the data from mongo db.

- Retrieves All Products
- Retrieves All Products based on Price Min and Max
- Retrieves All Products which has Fantastic attribute
- Retrieves All Products based on Rating Min and Max

Api documentation or test harness can be accessed from
[Swagger doc and test harness](http://localhost:5001/swagger)

Api health check can be accessed from
[Products api health check](http://localhost:5001/healthcheck/ping)

## Pre-requisites
- Windows & Mac
    - Visual Studio Code with C# extensions
    - [.Net Core SDK version](https://www.microsoft.com/net/download/core ".Net Core SDK")

## Build
The project can be built by running the built-in build script `.\Build.ps1` from the Powershell command line and script executes all the tests in the solution.

## Docker
This api is configured to run under container and can be run by following commands.

```{r, engine='bash', count_lines}
$ docker build -t productsapp .
$ docker run -d -p 8080:80 --name products productsapp
```


