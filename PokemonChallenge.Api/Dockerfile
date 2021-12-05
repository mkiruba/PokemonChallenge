#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["PokemonChallenge.Api/PokemonChallenge.Api.csproj", "PokemonChallenge.Api/"]
RUN dotnet restore "PokemonChallenge.Api/PokemonChallenge.Api.csproj"
COPY . .
WORKDIR "/src/PokemonChallenge.Api"
RUN dotnet build "PokemonChallenge.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "PokemonChallenge.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "PokemonChallenge.Api.dll"]