# dotnet-csc
`dotnet-csc` is a simple .NET tool wrapper over the `csc` C# compiler provided by all .NET installations, included with the Roslyn compiler platform.

This tool is available under the NuGet package name `csc`. In order to install it, use the following command:
```
dotnet tool install -g csc
```

## Use-cases
This tool is mainly intended when compiling C# code from a build system other than MSBuild, or for manually compiling files to IL assemblies.
