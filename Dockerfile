# ------------------------
# Runtime base
# ------------------------
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 10000

# ------------------------
# Build stage
# ------------------------
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy solution contents
COPY . .

# Restore ONLY the API project
RUN dotnet restore Fitness.API/Fitness.API.csproj

# Publish API
RUN dotnet publish Fitness.API/Fitness.API.csproj -c Release -o /app/publish

# ------------------------
# Final stage
# ------------------------
FROM base AS final
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "Fitness.API.dll"]
