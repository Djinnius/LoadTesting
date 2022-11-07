# Generate tools manifest 

Create a '.config' folder in the root folder of the github repository containing a 'dotnet-tools.json' defining the tooling used within the solution. This configuration will be shared amongst all developers and minimise versioning issues.

From the Command Line Interface (CLI) in the solution root run:

`dotnet new tool-manifest`

## Install tools locally for solution
Below are the list of tools installed for this solution. To restore run:

`dotnet tool restore`

To confirm installed tools, run:

`dotnet tool list`

To update a tool, and modify the manifest, run:

`dotnet tool update nameOfTool`

Available tools can be viewed at: https://www.nuget.org/packages?packagetype=dotnettool

### Entity Framework Core:
EF Core is a modern object-database mapper for .NET. It supports LINQ queries, change tracking, updates, and schema migrations. EF Core works with SQL Server, Azure SQL Database, SQLite, Azure Cosmos DB, MySQL, PostgreSQL, and other databases through a provider plugin API.

- Git Repository: https://github.com/dotnet/efcore

To install Entity Framework Core, run:

`dotnet tool install dotnet-ef`

### Microsoft Tye:
Project Tye is an experimental developer tool that makes developing, testing, and deploying microservices and distributed applications easier.
- Blog: https://devblogs.microsoft.com/dotnet/introducing-project-tye/
- Git Repository: https://github.com/dotnet/tye
- Documentation: https://github.com/dotnet/tye/blob/main/docs/README.md

To install Microsoft Tye, run:

`dotnet tool install Microsoft.Tye`

**NOTE:** an error will display during prerelease and output latest version available, specify the latest version in the '--version' option to install, e.g.

`dotnet tool install Microsoft.Tye --version "0.10.0-alpha.21420.1"`

To install Mapster tools, run:

`dotnet tool install Mapster.Tool`
