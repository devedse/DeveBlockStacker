FROM mcr.microsoft.com/dotnet/sdk:6.0-bullseye-slim-amd64 AS build
WORKDIR /src
COPY ["DeveBlockStacker.Core/DeveBlockStacker.Core.csproj", "DeveBlockStacker.Core/"]
COPY ["DeveBlockStacker.DesktopGL/DeveBlockStacker.DesktopGL.csproj", "DeveBlockStacker.DesktopGL/"]
RUN dotnet restore "DeveBlockStacker.DesktopGL/DeveBlockStacker.DesktopGL.csproj"
COPY . .
WORKDIR "/src/DeveBlockStacker.DesktopGL"
RUN dotnet build "DeveBlockStacker.DesktopGL.csproj" -c Release -o /app/build

FROM build AS publish
ARG BUILD_VERSION
ARG VER=${BUILD_VERSION:-1.0.0}
RUN dotnet publish "DeveBlockStacker.ConsoleApp.csproj" -c Release -o /app/publish /p:Version=$VER /p:UseAppHost=false