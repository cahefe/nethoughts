#https://medium.com/@nickfane/introduction-to-worker-services-in-net-core-3-0-4bb3fc631225
FROM mcr.microsoft.com/dotnet/core/runtime:3.1-nanoserver-1909 as base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-nanoserver-1909 as build
WORKDIR /src
COPY "AutoStartWorkerService.csproj" "AutoStartWorkerService/"
RUN dotnet restore "AutoStartWorkerService/AutoStartWorkerService.csproj"
COPY . .
WORKDIR "/src/AutoStartWorkerService"
RUN dotnet build "AutoStartWorkerService.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "AutoStartWorkerService.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT [ "dotnet", "AutoStartWorkerService.dll" ]