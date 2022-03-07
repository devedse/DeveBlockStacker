#FROM mcr.microsoft.com/dotnet/aspnet:6.0-bullseye-slim-amd64 AS build
##Also install dotnet 3.1
##RUN curl -sSL https://dot.net/v1/dotnet-install.sh | bash /dev/stdin -Channel 3.1 -InstallDir /usr/share/dotnet
##RUN apt update && apt install libfreetype6 -y

FROM mcr.microsoft.com/dotnet/aspnet:6.0-bullseye-slim-amd64 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
#FROM mono:latest AS build
RUN apt-get update
RUN apt-get install apt-transport-https dirmngr -y
RUN apt-key adv --keyserver hkp://keyserver.ubuntu.com:80 --recv-keys 3FA7E0328081BFF6A14DA29AA6A19B38D3D831EF
RUN echo "deb https://download.mono-project.com/repo/debian stable-buster main" | tee /etc/apt/sources.list.d/mono-official-stable.list
RUN apt-get update
RUN apt-get install mono-complete -y
#RUN curl -sSL https://dot.net/v1/dotnet-install.sh | bash /dev/stdin -Channel 3.1 -InstallDir /usr/share/dotnet
#RUN apt update && apt install libfreetype6 -y
WORKDIR /src
COPY ["DeveBlockStacker.WebGL/DeveBlockStacker.WebGL.csproj", "DeveBlockStacker.WebGL/"]
#RUN dotnet restore "DeveBlockStacker.WebGL/DeveBlockStacker.WebGL.csproj"
COPY . .
WORKDIR "/src/DeveBlockStacker.WebGL"
#RUN dotnet build "DeveBlockStacker.WebGL.csproj" -c Release -o /app/build
#run msbuild /p:Configuration=Release "DeveBlockStacker.WebGL.csproj" /p:OutputPath=/app/build
#
#FROM build AS publish
#ARG BUILD_VERSION
#ARG VER=${BUILD_VERSION:-1.0.0}
#RUN dotnet publish "DeveBlockStacker.WebGL.csproj" -c Release -o /app/publish /p:Version=$VER /p:UseAppHost=false
#
#FROM base AS final
#WORKDIR /app
#COPY --from=publish /app/publish .
#ENTRYPOINT ["dotnet", "DeveBlockStacker.WebGL.dll"]