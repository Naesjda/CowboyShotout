FROM mcr.microsoft.com/dotnet/runtime:7.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["ShotemUp/ShotemUp.csproj", "ShotemUp/"]
RUN dotnet restore "ShotemUp/ShotemUp.csproj"
COPY . .
WORKDIR "/src/ShotemUp"
RUN dotnet build "ShotemUp.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ShotemUp.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ShotemUp.dll"]
