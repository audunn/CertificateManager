FROM mcr.microsoft.com/dotnet/core/aspnet:2.2-stretch-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:2.2-stretch AS build
WORKDIR /src
COPY ["CertificateManager.csproj", "CertificateManager/"]
RUN dotnet restore "CertificateManager/CertificateManager.csproj"
COPY . CertificateManager/
WORKDIR "/src/CertificateManager"
RUN dotnet build "CertificateManager.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "CertificateManager.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
EXPOSE 80

ENTRYPOINT ["dotnet", "CertificateManager.dll"]