chcp 65001
rd /s /q bin\nupkg
md bin\nupkg\lib\netcoreapp1.0
copy /y bin\Debug\netstandard1.6\Evolution.Plugin.Core.* bin\nupkg\lib\netcoreapp1.0\
copy /y Evolution.Plugin.Core.nuspec bin\nupkg\
cd bin\nupkg
nuget pack Evolution.Plugin.Core.nuspec
