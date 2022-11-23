#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["Services/Catalogue/Catalogue.API/Catalogue.API.csproj", "Services/Catalogue/Catalogue.API/"]
RUN dotnet restore "Services/Catalogue/Catalogue.API/Catalogue.API.csproj"
COPY . .
WORKDIR "/src/Services/Catalogue/Catalogue.API"
RUN dotnet build "Catalogue.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Catalogue.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Catalogue.API.dll"]