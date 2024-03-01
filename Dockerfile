# https://hub.docker.com/_/microsoft-dotnet
FROM mcr.microsoft.com/dotnet/sdk:8.0.101 AS build
WORKDIR /source

# copy csproj and restore as distinct layers
COPY *.sln .
COPY AISmartHomeApp/*.csproj ./AISmartHomeApp/
RUN dotnet restore

# copy everything else and build app
COPY AISmartHomeApp/. ./AISmartHomeApp/
WORKDIR /source/AISmartHomeApp
RUN dotnet publish -c release -o /app --no-restore

# final stage/image
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /app ./
ENV ASPNETCORE_URLS=http://+:80
HEALTHCHECK CMD curl --fail http://localhost:80/health || exit
EXPOSE 80
ENTRYPOINT ["dotnet", "AISmartHomeApp.dll"]