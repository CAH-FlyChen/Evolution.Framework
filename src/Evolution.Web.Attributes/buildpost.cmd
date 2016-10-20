chcp 65001
rd /s /q bin\nupkg
md bin\nupkg\lib\netcoreapp1.0
copy /y bin\Debug\netstandard1.6\Evolution.Web.Attributes.* bin\nupkg\lib\netcoreapp1.0\
copy /y Evolution.Web.Attributes.nuspec bin\nupkg\
cd bin\nupkg
nuget pack Evolution.Web.Attributes.nuspec
