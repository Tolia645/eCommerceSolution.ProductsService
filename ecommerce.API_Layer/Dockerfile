FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["ecommerce.API_Layer/ecommerce.API_Layer.csproj", "ecommerce.API_Layer/"]
COPY ["eCommerce.BLL/eCommerce.BLL.csproj", "eCommerce.BLL/"]
COPY ["eCommerce.DAL/eCommerce.DAL.csproj", "eCommerce.DAL/"]
RUN dotnet restore "ecommerce.API_Layer/ecommerce.API_Layer.csproj"
COPY . .
WORKDIR "/src/ecommerce.API_Layer"
RUN dotnet build "ecommerce.API_Layer.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "ecommerce.API_Layer.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ecommerce.API_Layer.dll"]
