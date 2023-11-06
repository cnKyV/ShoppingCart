FROM mcr.microsoft.com/dotnet/runtime:7.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["ShoppingCart.Console/ShoppingCart.Console.csproj", "ShoppingCart.Console/"]
RUN dotnet restore "ShoppingCart.Console/ShoppingCart.Console.csproj"
COPY . .
WORKDIR "/src/ShoppingCart.Console"
RUN dotnet build "ShoppingCart.Console.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ShoppingCart.Console.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ShoppingCart.Console.dll"]
