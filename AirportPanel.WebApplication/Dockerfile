FROM microsoft/dotnet:2.1-aspnetcore-runtime-nanoserver-1709 AS base
WORKDIR /app
EXPOSE 2095
EXPOSE 44372

FROM microsoft/dotnet:2.1-sdk-nanoserver-1709 AS build
WORKDIR /src
COPY AirportPanel.WebApplication/AirportPanel.WebApplication.csproj AirportPanel.WebApplication/
RUN dotnet restore AirportPanel.WebApplication/AirportPanel.WebApplication.csproj
COPY . .
WORKDIR /src/AirportPanel.WebApplication
RUN dotnet build AirportPanel.WebApplication.csproj -c Release -o /app

FROM build AS publish
RUN dotnet publish AirportPanel.WebApplication.csproj -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "AirportPanel.WebApplication.dll"]
