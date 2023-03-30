#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/runtime:3.1-buster-slim AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["ACME.WorkerService/ACME.WorkerService.csproj", "ACME.WorkerService/"]
RUN dotnet restore "ACME.WorkerService/ACME.WorkerService.csproj"
COPY . .
WORKDIR "/src/ACME.WorkerService"
RUN dotnet build "ACME.WorkerService.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ACME.WorkerService.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ACME.WorkerService.dll"]