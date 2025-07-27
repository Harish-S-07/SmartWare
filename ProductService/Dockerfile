FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build

WORKDIR /src

COPY ["ProductService.Api/ProductService.Api.csproj", "ProductService.Api/"]
COPY ["ProductService.Application/ProductService.Application.csproj", "ProductService.Application/"]
COPY ["ProductService.Domain/ProductService.Domain.csproj", "ProductService.Domain/"]
COPY ["ProductService.Infrastructure/ProductService.Infrastructure.csproj", "ProductService.Infrastructure/"]
COPY ["ProductService.sln", "./"]

RUN dotnet restore "ProductService.Api/ProductService.Api.csproj"

COPY . .

ENV PATH="$PATH:/root/.dotnet/tools"

RUN apt-get update && apt-get install -y unzip curl \
    && dotnet tool install --global dotnet-ef

WORKDIR "/src/ProductService.Api"
RUN dotnet publish -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:9.0
WORKDIR /app

RUN apt-get update && apt-get install -y netcat-traditional

COPY --from=build /app/publish ./

COPY ProductService.Api/wait.sh /wait.sh

RUN chmod +x /wait.sh

ENTRYPOINT ["/wait.sh", "postgres_db", "5432", "dotnet", "ProductService.Api.dll"]