FROM mcr.microsoft.com/dotnet/runtime:7.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["TempForTesting/TempForTesting.csproj", "TempForTesting/"]
RUN dotnet restore "TempForTesting/TempForTesting.csproj"
COPY . .
WORKDIR "/src/TempForTesting"
RUN dotnet build "TempForTesting.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "TempForTesting.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "TempForTesting.dll"]
