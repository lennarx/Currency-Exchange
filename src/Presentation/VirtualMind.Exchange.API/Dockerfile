#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["VirtualMind.Exchange.API/VirtualMind.Exchange.API.csproj", "VirtualMind.Exchange.API/"]
RUN dotnet restore "VirtualMind.Exchange.API/VirtualMind.Exchange.API.csproj"
COPY . .
WORKDIR "/src/VirtualMind.Exchange.API"
RUN dotnet build "VirtualMind.Exchange.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "VirtualMind.Exchange.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "VirtualMind.Exchange.API.dll"]