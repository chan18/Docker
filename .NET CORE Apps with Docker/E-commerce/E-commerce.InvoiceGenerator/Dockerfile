#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/runtime:6.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["E-commerce.InvoiceGenerator/E-commerce.InvoiceGenerator.csproj", "E-commerce.InvoiceGenerator/"]
RUN dotnet restore "E-commerce.InvoiceGenerator/E-commerce.InvoiceGenerator.csproj"
COPY . .
WORKDIR "/src/E-commerce.InvoiceGenerator"
RUN dotnet build "E-commerce.InvoiceGenerator.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "E-commerce.InvoiceGenerator.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "E-commerce.InvoiceGenerator.dll"]