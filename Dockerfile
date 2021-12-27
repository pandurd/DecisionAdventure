#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80

# Setup NodeJs
RUN apt-get update && \
    apt-get install -y wget && \
    apt-get install -y gnupg2 && \
    wget -qO- https://deb.nodesource.com/setup_16.x | bash - && \
    apt-get install -y build-essential nodejs
# End setup

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build

# Setup NodeJs
RUN apt-get update && \
    apt-get install -y wget && \
    apt-get install -y gnupg2 && \
    wget -qO- https://deb.nodesource.com/setup_16.x | bash - && \
    apt-get install -y build-essential nodejs
# End setup

WORKDIR /src
COPY ["DecisionAdventure/DecisionAdventure.csproj", "DecisionAdventure/"]
RUN dotnet restore "DecisionAdventure/DecisionAdventure.csproj"
COPY . .
WORKDIR "/src/DecisionAdventure"
RUN dotnet build "DecisionAdventure.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "DecisionAdventure.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "DecisionAdventure.dll"]