#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Ste.User.Host/Ste.User.Host.csproj", "Ste.User.Host/"]
RUN dotnet restore "Ste.User.Host/Ste.User.Host.csproj"
COPY . .
WORKDIR "/src/Ste.User.Host"
RUN dotnet build "Ste.User.Host.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Ste.User.Host.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Ste.User.Host.dll"]