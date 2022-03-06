#FROM mcr.microsoft.com/dotnet/aspnet:6.0-bullseye-slim-amd64 AS build
##Also install dotnet 3.1
##RUN curl -sSL https://dot.net/v1/dotnet-install.sh | bash /dev/stdin -Channel 3.1 -InstallDir /usr/share/dotnet
##RUN apt update && apt install libfreetype6 -y

FROM nginx:stable-alpine AS base
COPY ./DeveBlockStacker.Blazor/nginxconfig/nginx.conf /etc/nginx/nginx.conf
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["DeveBlockStacker.Blazor/DeveBlockStacker.Blazor.csproj", "DeveBlockStacker.Blazor/"]
RUN dotnet restore "DeveBlockStacker.Blazor/DeveBlockStacker.Blazor.csproj"
COPY . .
WORKDIR "/src/DeveBlockStacker.Blazor"
RUN dotnet build "DeveBlockStacker.Blazor.csproj" -c Release -o /app/build

FROM build AS publish
ARG BUILD_VERSION
ARG VER=${BUILD_VERSION:-1.0.0}
RUN dotnet publish "DeveBlockStacker.Blazor.csproj" -c Release -o /app/publish /p:Version=$VER /p:UseAppHost=false

FROM base AS final
WORKDIR /usr/share/nginx/html
COPY --from=publish /app/publish/wwwroot .
CMD ["nginx", "-g", "daemon off;"]