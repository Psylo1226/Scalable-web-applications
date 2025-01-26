FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080
EXPOSE 5001

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ENV ASPNETCORE_ENVIRONMENT=Development
WORKDIR /src
COPY ["IdentityApi.csproj", "./"]
RUN dotnet restore "./IdentityApi.csproj"
COPY . .
WORKDIR "/src/."
RUN dotnet build "./IdentityApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "./IdentityApi.csproj" -c Release -o /app/publish

FROM base AS final
ENV ASPNETCORE_HTTP_PORTS=5001
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "IdentityApi.dll", "--environment=Development"]