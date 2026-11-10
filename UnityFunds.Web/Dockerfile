FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS base
WORKDIR /app
EXPOSE 8080
ENV ASPNETCORE_URLS=http://+:8080
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src
COPY ["UnityFunds.Web/UnityFunds.Web.csproj", "UnityFunds.Web/"]
RUN dotnet restore "UnityFunds.Web/UnityFunds.Web.csproj"
COPY . .
WORKDIR /src/UnityFunds.Web
RUN dotnet publish "UnityFunds.Web.csproj" -c Release -o /app/publish /p:UseAppHost=false
FROM base AS final
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "UnityFunds.Web.dll"]
