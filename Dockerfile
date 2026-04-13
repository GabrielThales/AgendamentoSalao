# 1. Etapa de Build (Usa o SDK completo do .NET 10)
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

# Copia os arquivos de projeto (csproj) e restaura dependências
COPY ["AgendamentoSalao.Api/AgendamentoSalao.Api.csproj", "AgendamentoSalao.Api/"]
RUN dotnet restore "AgendamentoSalao.Api/AgendamentoSalao.Api.csproj"

# Copia o resto do código e compila a aplicação
COPY . .
WORKDIR "/src/AgendamentoSalao.Api"
RUN dotnet build "AgendamentoSalao.Api.csproj" -c Release -o /app/build

# 2. Etapa de Publish (Otimiza o código gerado)
FROM build AS publish
RUN dotnet publish "AgendamentoSalao.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

# 3. Etapa Final (Usa a imagem mais leve apenas para execução)
FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS final
WORKDIR /app
EXPOSE 8080

# Copia apenas os binários gerados na etapa de publish
COPY --from=publish /app/publish .

# Comando que inicia a API
ENTRYPOINT ["dotnet", "AgendamentoSalao.Api.dll"]