#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["src/PokeFun/PokeFun.csproj", "PokeFun/"]
COPY ["src/PokeFun.FunTranslations/PokeFun.FunTranslations.csproj", "PokeFun.FunTranslations/"]
COPY ["src/PokeFun.PokeApi/PokeFun.PokeApi.csproj", "PokeFun.PokeApi/"]
RUN dotnet restore "PokeFun/PokeFun.csproj"
COPY src/ .
WORKDIR "/src/PokeFun"
RUN dotnet build "PokeFun.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "PokeFun.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "PokeFun.dll"]
