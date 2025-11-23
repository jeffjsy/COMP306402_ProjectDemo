# -------------------------
# 1. Build Stage
# -------------------------
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy csproj and restore
COPY ["COMP306402_ProjectDemo.csproj", "./"]
RUN dotnet restore

# Copy all project files
COPY . .

# Build and publish
RUN dotnet publish -c Release -o /app/publish


# -------------------------
# 2. Runtime Stage
# -------------------------
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app

# Expose port 8080 (ECS requires this by convention)
EXPOSE 8080

# Copy the compiled files from build stage
COPY --from=build /app/publish .

# ASP.NET Core expects URL binding override
ENV ASPNETCORE_URLS=http://+:8080

# Start the API
ENTRYPOINT ["dotnet", "COMP306402_ProjectDemo.dll"]
