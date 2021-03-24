#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["Zero.Core.WebApi/Zero.Core.WebApi.csproj", "Zero.Core.WebApi/"]
COPY ["Zero.Core.IRepositories/Zero.Core.IRepositories.csproj", "Zero.Core.IRepositories/"]
COPY ["Zero.Core.EfCore/Zero.Core.EfCore.csproj", "Zero.Core.EfCore/"]
COPY ["Zero.Core.Domain/Zero.Core.Domain.csproj", "Zero.Core.Domain/"]
COPY ["Zero.Core.Common/Zero.Core.Common.csproj", "Zero.Core.Common/"]
COPY ["Zero.Core.IServices/Zero.Core.IServices.csproj", "Zero.Core.IServices/"]
COPY ["Zero.Core.Quartz/Zero.Core.Quartz.csproj", "Zero.Core.Quartz/"]
RUN dotnet restore "Zero.Core.WebApi/Zero.Core.WebApi.csproj"
COPY . .
WORKDIR "/src/Zero.Core.WebApi"
RUN dotnet build "Zero.Core.WebApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Zero.Core.WebApi.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Zero.Core.WebApi.dll"]