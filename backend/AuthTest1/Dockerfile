#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["AuthTest1/AuthTest.API.csproj", "AuthTest1/"]
COPY ["AuthTest.Data/AuthTest.Data.csproj", "AuthTest.Data/"]
COPY ["AuthTest.Core/AuthTest.Core.csproj", "AuthTest.Core/"]
RUN dotnet restore "AuthTest1/AuthTest.API.csproj"
COPY . .
WORKDIR "/src/AuthTest1"
RUN dotnet build "AuthTest.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "AuthTest.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "AuthTest.API.dll"]