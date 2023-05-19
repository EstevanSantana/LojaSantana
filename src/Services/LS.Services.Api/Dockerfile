#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:3.1 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:3.1 AS build
WORKDIR /src
COPY ["./Services/LS.Services.Api/LS.Services.Api.csproj", "Services/LS.Services.Api/"]
COPY ["./Domain/LS.Domain.Core/LS.Domain.Core.csproj", "Domain/LS.Domain.Core/"]
COPY ["./Domain/LS.Domain/LS.Domain.csproj", "Domain/LS.Domain/"]
COPY ["./Infra/LS.Infra.CrossCutting.Identity/LS.Infra.CrossCutting.Identity.csproj", "Infra/LS.Infra.CrossCutting.Identity/"]
COPY ["./Infra/LS.Infra.Data.Write/LS.Infra.Data.Write.csproj", "Infra/LS.Infra.Data.Write/"]
COPY ["./Infra/LS.Infra.IoC/LS.Infra.CrossCutting.IoC.csproj", "Infra/LS.Infra.IoC/"]
COPY ["./Application/LS.Application/LS.Application.csproj", "Application/LS.Application/"]
COPY ["./Infra/LS.Infra.Data.Read/LS.Infra.Data.Read.csproj", "Infra/LS.Infra.Data.Read/"]

RUN dotnet restore "Services/LS.Services.Api/LS.Services.Api.csproj"
COPY . .
WORKDIR "Services/LS.Services.Api"
RUN dotnet build "LS.Services.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "LS.Services.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "LS.Services.Api.dll"]