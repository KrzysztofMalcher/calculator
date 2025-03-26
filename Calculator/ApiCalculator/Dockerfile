FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 5002

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["ApiCalculator/ApiCalculator.csproj", "ApiCalculator/"]
COPY ["Calculator/Calculator.csproj", "Calculator/"]
RUN dotnet restore "ApiCalculator/ApiCalculator.csproj"
COPY . .
WORKDIR "/src/ApiCalculator"
RUN dotnet build "ApiCalculator.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "ApiCalculator.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ApiCalculator.dll"]
