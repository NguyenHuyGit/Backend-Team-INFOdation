#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:3.1 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:3.1 AS build
WORKDIR /src
COPY ["EcommerceSolution.BackendAPI/EcommerceSolution.BackendAPI.csproj", "EcommerceSolution.BackendAPI/"]
RUN dotnet restore "EcommerceSolution.BackendAPI/EcommerceSolution.BackendAPI.csproj"
COPY . .
WORKDIR "/src/EcommerceSolution.BackendAPI"
RUN dotnet build "EcommerceSolution.BackendAPI.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "EcommerceSolution.BackendAPI.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "EcommerceSolution.BackendAPI.dll"]