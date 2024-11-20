FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["TwitterClone.API/TwitterClone.API.csproj", "TwitterClone.API/"]
COPY ["TwitterClone.Application/TwitterClone.Application.csproj", "TwitterClone.Application/"]
COPY ["TwitterClone.Domain/TwitterClone.Domain.csproj", "TwitterClone.Domain/"]
COPY ["TwitterClone.Infrastructure/TwitterClone.Infrastructure.csproj", "TwitterClone.Infrastructure/"]
RUN dotnet restore "TwitterClone.API/TwitterClone.API.csproj"
COPY . .
WORKDIR "/src/TwitterClone.API"
RUN dotnet build "TwitterClone.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

RUN dotnet tool install --global dotnet-ef
ENV PATH="$PATH:/root/.dotnet/tools"

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "TwitterClone.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "TwitterClone.API.dll"]
