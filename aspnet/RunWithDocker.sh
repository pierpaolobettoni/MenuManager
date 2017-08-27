dotnet restore
dotnet publish -o ./publish
docker build --no-cache -t menumanager .


