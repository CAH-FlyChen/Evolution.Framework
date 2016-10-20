dotnet build
dotnet pack
c:\nuget\nuget push bin\debug\*.symbols.nupkg -Source nuget.org -configfile c:\nuget\nuget.config