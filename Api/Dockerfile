FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /app
COPY Api/Api.csproj Api/Api.csproj
COPY Core/Core.csproj Core/Core.csproj
COPY Infrastructure/Infrastructure.csproj Infrastructure/Infrastructure.csproj
RUN dotnet restore Api

COPY . .
RUN dotnet publish Api -c release -o out

FROM mcr.microsoft.com/dotnet/aspnet:6.0
WORKDIR /app
COPY --from=build /app/out .

ARG BUILD_VERSION
ENV BUILD_VERSION $BUILD_VERSION
ENV ASPNETCORE_URLS=http://+:8001

ENTRYPOINT ["dotnet", "Api.dll"]