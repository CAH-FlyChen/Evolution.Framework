chcp 65001
rd /s /q bin
rem rd /s /q bin\nupkg
rem md bin\nupkg\lib\netcoreapp1.0
rem copy /y bin\Debug\netstandard1.6\Evolution.IInfrastructure.dll bin\nupkg\lib\netcoreapp1.0\
rem copy /y bin\Debug\netstandard1.6\Evolution.IInfrastructure.pdb bin\nupkg\lib\netcoreapp1.0\
rem copy /y Evolution.IInfrastructure.nuspec bin\nupkg\
rem cd bin\nupkg
rem nuget pack Evolution.IInfrastructure.nuspec
