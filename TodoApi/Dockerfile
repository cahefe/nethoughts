FROM mcr.microsoft.com/dotnet/core/runtime:3.1-nanoserver-1909 as base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-nanoserver-1909 as build
WORKDIR /src
COPY "TodoApi.csproj" "TodoApi/"
RUN dotnet restore "TodoApi/TodoApi.csproj"
COPY . .
WORKDIR "/src/TodoApi"
RUN dotnet build "TodoApi.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "TodoApi.csproj" -c Release -o /app

EXPOSE 5000

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT [ "dotnet", "TodoApi.dll" ]