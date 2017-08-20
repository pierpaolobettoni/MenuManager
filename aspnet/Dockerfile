
FROM microsoft/dotnet:1.1.2-sdk

LABEL Name=menumanager
COPY . /app
WORKDIR /app
RUN ["dotnet", "restore"]
EXPOSE 5000
ENTRYPOINT ["dotnet", "run"]



