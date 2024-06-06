FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build

RUN mkdir https
RUN dotnet dev-certs https -ep ./https/aspnetapp.pfx -p some45656454very12334124long566776567passwordgoeshere
RUN dotnet dev-certs https --trust

ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Planner.WebUI/Planner.WebUI.csproj", "Planner.WebUI/"]
COPY ["Planner.Data/Planner.Data.csproj", "Planner.Data/"]
COPY ["Planner.Ui/Planner.Ui.csproj", "Planner.Ui/"]
RUN dotnet restore "Planner.WebUI/Planner.WebUI.csproj"
COPY . .
WORKDIR "/src/Planner.WebUI"
RUN dotnet build "Planner.WebUI.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "Planner.WebUI.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --chmod=0755 --from=build /https/* /https/
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Planner.WebUI.dll"]
