chcp 65001
rd /s /q bin\nupkg
md bin\nupkg\lib\netcoreapp1.0
copy /y bin\Debug\netstandard1.6\Evolution.Data.dll bin\nupkg\lib\netcoreapp1.0\
copy /y bin\Debug\netstandard1.6\Evolution.Data.pdb bin\nupkg\lib\netcoreapp1.0\
copy /y Evolution.Data.nuspec bin\nupkg\
cd bin\nupkg
nuget pack Evolution.Data.nuspec
