#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["Assignment.Services.BookStore.API/Assignment.Services.BookStore.API.csproj", "Assignment.Services.BookStore.API/"]
RUN dotnet restore "Assignment.Services.BookStore.API/Assignment.Services.BookStore.API.csproj"
COPY . .
WORKDIR "/src/Assignment.Services.BookStore.API"
RUN dotnet build "Assignment.Services.BookStore.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Assignment.Services.BookStore.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Assignment.Services.BookStore.API.dll"]