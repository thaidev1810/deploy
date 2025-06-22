FROM mcr.microsoft.com/dotnet/runtime:8.0 AS base
USER $APP_UID
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["BloodDonation.Apis/BloodDonation.Apis.csproj", "BloodDonation.Apis/"]
RUN dotnet restore "BloodDonation.Apis/BloodDonation.Apis.csproj"
COPY . .
WORKDIR "/src/BloodDonation.Apis"
RUN dotnet build "BloodDonation.Apis.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "BloodDonation.Apis.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "BloodDonation.Apis.dll"]
