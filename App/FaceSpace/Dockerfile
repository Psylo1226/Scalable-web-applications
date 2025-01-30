FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080
EXPOSE 5000

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ENV ASPNETCORE_ENVIRONMENT=Development
WORKDIR /src
COPY ["FaceSpace.csproj", "./"]
RUN dotnet restore "./FaceSpace.csproj"
COPY . .
WORKDIR "/src/."
RUN dotnet build "./FaceSpace.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "./FaceSpace.csproj" -c Release -o /app/publish

FROM base AS final
ENV ASPNETCORE_HTTPS_PORTS=5000
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "FaceSpace.dll", "--environment=Development"]