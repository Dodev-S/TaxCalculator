#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["src/TaxCalculator.Api/TaxCalculator.Api.csproj", "src/TaxCalculator.Api/"]
COPY ["src/TaxCalculator.Core/TaxCalculator.Core.csproj", "src/TaxCalculator.Core/"]
COPY ["src/TaxCalculator.Domain/TaxCalculator.Domain.csproj", "src/TaxCalculator.Domain/"]
COPY ["src/TaxCalculator.Infrastructure/TaxCalculator.Infrastructure.csproj", "src/TaxCalculator.Infrastructure/"]
RUN dotnet restore "src/TaxCalculator.Api/TaxCalculator.Api.csproj"
COPY . .
WORKDIR "/src/src/TaxCalculator.Api"
RUN dotnet build "TaxCalculator.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "TaxCalculator.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "TaxCalculator.Api.dll"]