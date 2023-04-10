# Learn about building .NET container images:
# https://github.com/dotnet/dotnet-docker/blob/main/samples/README.md
FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /source

# copy csproj and restore as distinct layers
COPY AISmartHomeApp/*.csproj .
RUN dotnet restore --use-current-runtime

# copy everything else and build app
COPY AISmartHomeApp/. .
RUN dotnet publish --use-current-runtime -c Release --self-contained false -o /app


# final stage/image
FROM mcr.microsoft.com/dotnet/aspnet:7.0
WORKDIR /app
COPY --from=build /app .
ENTRYPOINT ["dotnet", "AISmartHomeApp.dll"]

# FROM mcr.microsoft.com/dotnet/sdk:7.0-jammy as build-env
# WORKDIR /AISmartHomeApp
# COPY . ./
# #COPY *.csproj .
# RUN dotnet restore AISmartHomeApp
# RUN dotnet publish -c Release -o /publish

# FROM mcr.microsoft.com/dotnet/aspnet:7.0-jammy as runtime
# WORKDIR /publish
# RUN apt-get update \
#     && apt-get install -y curl jq 

# COPY --from=build-env /publish .
# ENV ASPNETCORE_URLS=http://+:80
# HEALTHCHECK CMD curl --fail http://localhost:80/health || exit
# EXPOSE 80
# ENTRYPOINT ["dotnet", "AISmartHomeApp.dll"]