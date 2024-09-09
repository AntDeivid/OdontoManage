# Imagem base para runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
#EXPOSE 5217
#EXPOSE 7188

# Imagem para build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY *.sln ./
COPY ./OdontoManage.API/*.csproj ./OdontoManage.API/
COPY ./OdontoManage.Application/*.csproj ./OdontoManage.Application/
COPY ./OdontoManage.Core/*.csproj ./OdontoManage.Core/
COPY ./OdontoManage.Infrastructure/*.csproj ./OdontoManage.Infrastructure/

# Restore de dependências
RUN dotnet restore

# Copia o restante do código
COPY . .

# Build do projeto
WORKDIR /src/OdontoManage.API
RUN dotnet build -c Release -o /app/build

# Publicação da aplicação
FROM build AS publish
RUN dotnet publish -c Release -o /app/publish

# Imagem final com o runtime
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "OdontoManage.API.dll"]
