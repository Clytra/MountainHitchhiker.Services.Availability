FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["src/MountainHitchhiker.Services.Availability.Api/MountainHitchhiker.Services.Availability.Api.csproj", "src/MountainHitchhiker.Services.Availability.Api/"]
RUN dotnet restore "src/MountainHitchhiker.Services.Availability.Api/MountainHitchhiker.Services.Availability.Api.csproj"
COPY . .
WORKDIR "/src/src/MountainHitchhiker.Services.Availability.Api"
RUN dotnet build "MountainHitchhiker.Services.Availability.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "MountainHitchhiker.Services.Availability.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MountainHitchhiker.Services.Availability.Api.dll"]
