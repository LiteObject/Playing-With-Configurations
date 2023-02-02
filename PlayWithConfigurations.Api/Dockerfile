#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["PlayWithConfigurations.Api/PlayWithConfigurations.Api.csproj", "PlayWithConfigurations.Api/"]
RUN dotnet restore "PlayWithConfigurations.Api/PlayWithConfigurations.Api.csproj"
COPY . .
WORKDIR "/src/PlayWithConfigurations.Api"
RUN dotnet build "PlayWithConfigurations.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "PlayWithConfigurations.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

RUN apt update
RUN apt install -y nano
RUN apt install -y curl

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "PlayWithConfigurations.Api.dll"]