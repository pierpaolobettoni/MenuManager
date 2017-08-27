dotnet restore
dotnet build
dotnet publish -o ./publish
docker build --no-cache -t menumanager .


