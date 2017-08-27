
FROM microsoft/dotnet:1.1.2-sdk

LABEL Name=menumanager

WORKDIR /app
COPY ./publish .

EXPOSE 5000
ENV ASPNETCORE_ENVIRONMENT Development
ENTRYPOINT ["dotnet", "clean_aspnet_mvc.dll"]



