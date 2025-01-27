FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080
EXPOSE 5003

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ENV ASPNETCORE_ENVIRONMENT=Development
WORKDIR /src
COPY ["PostApi.csproj", "./"]
RUN dotnet restore "./PostApi.csproj"
COPY . .
WORKDIR "/src/."
RUN dotnet build "./PostApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "./PostApi.csproj" -c Release -o /app/publish

FROM base AS final
ENV ASPNETCORE_HTTP_PORTS=5003
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "PostApi.dll", "--environment=Development"]