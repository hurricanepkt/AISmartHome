# Learn about building .NET container images:
# https://github.com/dotnet/dotnet-docker/blob/main/samples/README.md
FROM mcr.microsoft.com/dotnet/sdk:8.0-alpine AS build
WORKDIR /source

# copy csproj and restore as distinct layers
COPY AISmartHomeApp/*.csproj .
RUN dotnet restore --use-current-runtime

# copy everything else and build app
COPY AISmartHomeApp/. .
RUN dotnet publish --use-current-runtime --no-self-contained  -o /app AISmartHomeApp.csproj


# final stage/image
FROM mcr.microsoft.com/dotnet/aspnet:8.0-alpine
WORKDIR /app 
RUN apk --no-cache add curl
COPY --from=build /app .
ENV ASPNETCORE_URLS=http://+:80
HEALTHCHECK CMD curl --fail http://localhost:80/health || exit
EXPOSE 80
ENTRYPOINT ["dotnet", "AISmartHomeApp.dll"]