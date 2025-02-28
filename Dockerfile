FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["/src/Company.Api/Company.Api.csproj", "src/Company.Api/Company.Api/"]
COPY ["/src/Company.Api/Company.Domain.csproj", "src/Company.Api/Company.Domain/"]
COPY ["/src/Company.Api/Company.Infrastructure.csproj", "src/Company.Api/Company.Infrastructure/"]

RUN dotnet restore "/src/Company.Api/Company.Api.csproj"
COPY . .
WORKDIR "/src/src/Company.Api"
RUN dotnet build "./Company.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Company.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Company.Api.dll"]