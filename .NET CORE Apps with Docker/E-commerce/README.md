

## Add logging packages. 

```
dotnet add package Serilog.AspNetCore --version 7.0.0
dotnet add package Serilog.Enrichers.Environment --version 2.3.0-dev-00792
dotnet add package Serilog.Sinks.Seq --version 5.2.3-dev-00262
```

Note:-
Serilog.Formatting.Compact


## Seq container
```
docker pull datalust/seq
```
### Run the container
```
docker run --name=seq -d -e ACCEPT_EULA=Y -p 5341:80 datalust/seq:latest
```


# Worker Service.
packages:
```
    <PackageReference Include="Serilog.Enrichers.Environment" Version="2.2.0" />
    <PackageReference Include="Serilog.Extensions.Hosting" Version="7.0.0" />
    <PackageReference Include="Serilog.Sinks.Console" Version="4.1.0" />
    <PackageReference Include="Serilog.Sinks.Seq" Version="5.2.2" />
```
