# Use .NET 9 SDK to build the app
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /source

# Copy and restore dependencies
COPY LabPipeLine.csproj .
RUN dotnet restore "LabPipeLine.csproj"

# Copy everything else and build
COPY . .
RUN dotnet publish "LabPipeLine.csproj" -c Release -o /app/publish

# Build runtime image
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /app/publish .
ENV ASPNETCORE_URLS=http://+:8080
EXPOSE 8080
ENTRYPOINT ["dotnet", "LabPipeLine.dll"]
