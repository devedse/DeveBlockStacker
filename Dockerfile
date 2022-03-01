FROM mcr.microsoft.com/dotnet/sdk:6.0-bullseye-slim-amd64 AS build
#Also install dotnet 3.1
RUN curl -sSL https://dot.net/v1/dotnet-install.sh | bash /dev/stdin -Channel 3.1 -InstallDir /usr/share/dotnet
RUN apt update && apt install libfreetype6 -y

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
RUN dotnet publish "DeveBlockStacker.DesktopGL.csproj" -c Release -o /app/publish /p:Version=$VER /p:UseAppHost=false