# Estágio de build
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src
COPY ["src/AgroSolutions.Properties.Service.Api/AgroSolutions.Properties.Service.Api.csproj", "src/AgroSolutions.Properties.Service.Api/"]
COPY ["src/AgroSolutions.Properties.Service.Application/AgroSolutions.Properties.Service.Application.csproj", "src/AgroSolutions.Properties.Service.Application/"]
COPY ["src/AgroSolutions.Properties.Service.Domain/AgroSolutions.Properties.Service.Domain.csproj", "src/AgroSolutions.Properties.Service.Domain/"]
COPY ["src/AgroSolutions.Properties.Service.Infra/AgroSolutions.Properties.Service.Infra.csproj", "src/AgroSolutions.Properties.Service.Infra/"]

# Restaurar dependências
RUN dotnet restore "src/AgroSolutions.Properties.Service.Api/AgroSolutions.Properties.Service.Api.csproj"

# Copiar tudo
COPY . .

# Build
WORKDIR "/src/src/AgroSolutions.Properties.Service.Api"
RUN dotnet build "AgroSolutions.Properties.Service.Api.csproj" -c Release -o /app/build

# Estágio de publicação
FROM build AS publish
RUN dotnet publish "AgroSolutions.Properties.Service.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

# Estágio final
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "AgroSolutions.Properties.Service.Api.dll"]
