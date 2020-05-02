FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build
WORKDIR /src

COPY Registration/Registration.csproj Registration/
RUN dotnet restore Registration/Registration.csproj
COPY . .
WORKDIR "/src/Registration/"
RUN dotnet build "Registration.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Registration.csproj" -c Release -o /app/publish

FROM base as final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Registration.dll"]