chcp 65001
rem md bin\nupkg\lib\netcoreapp1.0
rem copy /y bin\Debug\netstandard1.6\Evolution.Framework.dll bin\nupkg\lib\netcoreapp1.0\
rem copy /y bin\Debug\netstandard1.6\Evolution.Framework.pdb bin\nupkg\lib\netcoreapp1.0\
rem copy /y Evolution.Framework.nuspec bin\nupkg\
rem cd bin\nupkg
rem nuget pack Evolution.Framework.nuspec
