chcp 65001
rd /s /q bin\nupkg
md bin\nupkg\lib\netcoreapp1.0
copy /y bin\Debug\netstandard1.6\Evolution.RepositoryBase.* bin\nupkg\lib\netcoreapp1.0\
copy /y Evolution.RepositoryBase.nuspec bin\nupkg\
cd bin\nupkg
nuget pack Evolution.RepositoryBase.nuspec
