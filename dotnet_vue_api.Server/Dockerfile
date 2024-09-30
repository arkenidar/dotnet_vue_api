# Vedere https://aka.ms/customizecontainer per informazioni su come personalizzare il contenitore di debug e su come Visual Studio usa questo Dockerfile per compilare le immagini per un debug più rapido.

# Questa fase viene usata durante l'esecuzione da Visual Studio in modalità rapida (impostazione predefinita per la configurazione di debug)
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081


# Questa fase viene usata per compilare il progetto di servizio
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS with-node
RUN apt-get update
RUN apt-get install curl
RUN curl -sL https://deb.nodesource.com/setup_20.x | bash
RUN apt-get -y install nodejs


FROM with-node AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["dotnet_vue_api.Server/dotnet_vue_api.Server.csproj", "dotnet_vue_api.Server/"]
COPY ["dotnet_vue_api.client/dotnet_vue_api.client.esproj", "dotnet_vue_api.client/"]
RUN dotnet restore "./dotnet_vue_api.Server/dotnet_vue_api.Server.csproj"
COPY . .
WORKDIR "/src/dotnet_vue_api.Server"
RUN dotnet build "./dotnet_vue_api.Server.csproj" -c $BUILD_CONFIGURATION -o /app/build

# Questa fase viene usata per pubblicare il progetto di servizio da copiare nella fase finale
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./dotnet_vue_api.Server.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# Questa fase viene usata nell'ambiente di produzione o durante l'esecuzione da Visual Studio in modalità normale (impostazione predefinita quando non si usa la configurazione di debug)
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "dotnet_vue_api.Server.dll"]
