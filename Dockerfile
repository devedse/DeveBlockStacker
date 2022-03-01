#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/sdk:6.0-bullseye-slim-amd64 AS build
WORKDIR /src
#COPY ["DeveBlockStacker.Core/DeveBlockStacker.Core.csproj", "DeveBlockStacker.Core/"]
#COPY ["DeveBlockStacker.DesktopGL/DeveBlockStacker.DesktopGL.csproj", "DeveBlockStacker.DesktopGL/"]
#RUN dotnet restore "DeveBlockStacker.DesktopGL/DeveBlockStacker.DesktopGL.csproj"
COPY . .
WORKDIR "/src/DeveBlockStacker.DesktopGL"