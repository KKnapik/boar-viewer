#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["Api.BoarService/Api.BoarService.csproj", "Api.BoarService/"]
RUN dotnet restore "Api.BoarService/Api.BoarService.csproj"
COPY . .
WORKDIR "/src/Api.BoarService"
RUN dotnet build "Api.BoarService.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Api.BoarService.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Api.BoarService.dll"]