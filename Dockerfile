# Build stage
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

# Copy solution and project files
COPY QuantityMeasurementApp.sln ./
COPY QuantityMeasurementAppAPI/QuantityMeasurementAppAPI.csproj QuantityMeasurementAppAPI/
COPY QuantityMeasurementAppBusinessLayer/QuantityMeasurementAppBusinessLayer.csproj QuantityMeasurementAppBusinessLayer/
COPY QuantityMeasurementAppModel/QuantityMeasurementAppModel.csproj QuantityMeasurementAppModel/
COPY QuantityMeasurementAppRepositoryLayer/QuantityMeasurementAppRepositoryLayer.csproj QuantityMeasurementAppRepositoryLayer/

# Restore packages
RUN dotnet restore

# Copy all source files
COPY . .

# Build and publish
RUN dotnet publish QuantityMeasurementAppAPI/QuantityMeasurementAppAPI.csproj \
    -c Release \
    -o /app/publish \
    --no-restore

# Runtime stage
FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS runtime
WORKDIR /app

COPY --from=build /app/publish .

# Render sets PORT env variable
ENV ASPNETCORE_URLS=http://0.0.0.0:${PORT:-8080}

EXPOSE 8080

ENTRYPOINT ["dotnet", "QuantityMeasurementAppAPI.dll"]
