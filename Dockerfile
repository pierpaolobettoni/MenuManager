
FROM microsoft/aspnetcore-build
LABEL Name=MenuManager Version=0.0.1
ARG source=.
WORKDIR /app
EXPOSE 80:5000
COPY $source .
RUN dotnet restore
ENTRYPOINT dotnet run

