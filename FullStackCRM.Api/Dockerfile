#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["FullStackCRM.Api/FullStackCRM.Api.csproj", "FullStackCRM.Api/"]
COPY ["FullStackCRM.Shared/FullStackCRM.Shared.csproj", "FullStackCRM.Shared/"]
COPY ["FullStackCRM.Infra.Data/FullStackCRM.Infra.Data.csproj", "FullStackCRM.Infra.Data/"]
COPY ["FullStackCRM.Domain/FullStackCRM.Domain.csproj", "FullStackCRM.Domain/"]
COPY ["FullStackCRM.Application/FullStackCRM.Application.csproj", "FullStackCRM.Application/"]
RUN dotnet restore "FullStackCRM.Api/FullStackCRM.Api.csproj"
COPY . .
WORKDIR "/src/FullStackCRM.Api"
RUN dotnet build "FullStackCRM.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "FullStackCRM.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "FullStackCRM.Api.dll"]
