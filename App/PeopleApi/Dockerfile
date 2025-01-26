FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080
EXPOSE 5002

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ENV ASPNETCORE_ENVIRONMENT=Development
WORKDIR /src
COPY ["PeopleApi.csproj", "./"]
RUN dotnet restore "./PeopleApi.csproj"
COPY . .
WORKDIR "/src/."
RUN dotnet build "./PeopleApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "./PeopleApi.csproj" -c Release -o /app/publish

FROM base AS final
ENV ASPNETCORE_HTTP_PORTS=5002
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "PeopleApi.dll", "--environment=Development"]