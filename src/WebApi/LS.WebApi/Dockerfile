#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:3.1 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:3.1 AS build
WORKDIR /src
COPY ["src/WebApi/LS.WebApi/LS.WebApi.csproj", "src/WebApi/LS.WebApi/"]
RUN dotnet restore "src/WebApi/LS.WebApi/LS.WebApi.csproj"
COPY . .
WORKDIR "/src/src/WebApi/LS.WebApi"
RUN dotnet build "LS.WebApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "LS.WebApi.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "LS.WebApi.dll"]