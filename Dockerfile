FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /app
EXPOSE 80

COPY src/SMS.API/SMS.API.csproj src/SMS.API/
COPY src/SMS.Application/SMS.Application.csproj src/SMS.Application/
COPY src/SMS.Infrastructure/SMS.Infrastructure.csproj src/SMS.Infrastructure/
COPY src/SMS.Domain/SMS.Domain.csproj src/SMS.Domain/

WORKDIR /app/src/SMS.API
RUN dotnet restore

WORKDIR /app/src
COPY src/ .

WORKDIR /app/src/SMS.API
RUN dotnet publish -c Release -o /app/out

FROM mcr.microsoft.com/dotnet/aspnet:7.0
COPY --from=build /app/out .
ENTRYPOINT ["dotnet", "SMS.API.dll"]