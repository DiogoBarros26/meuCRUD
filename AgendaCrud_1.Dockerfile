# ─── Stage 1: Build ───────────────────────────────────────────────────────────
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copia o arquivo de projeto e restaura dependências
COPY AgendaCrud_1/*.csproj ./AgendaCrud_1/
RUN dotnet restore ./AgendaCrud_1/AgendaCrud_1.csproj

# Copia o restante do código e publica
COPY AgendaCrud_1/. ./AgendaCrud_1/
WORKDIR /src/AgendaCrud_1
RUN dotnet publish -c Release -o /app/publish

# ─── Stage 2: Runtime ─────────────────────────────────────────────────────────
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app

COPY --from=build /app/publish .

EXPOSE 5000
ENV ASPNETCORE_URLS=http://+:5000

ENTRYPOINT ["dotnet", "AgendaCrud_1.dll"]
