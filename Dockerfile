# ---------- build stage ----------
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy everything
COPY Code ./Code

# Restore and publish (runtime-dependent, portable)
RUN dotnet restore Code/LiquidTransform.Web/LiquidTransform.Web.csproj
RUN dotnet publish Code/LiquidTransform.Web/LiquidTransform.Web.csproj \
    -c Release \
    -o /out

# ---------- runtime stage ----------
FROM mcr.microsoft.com/dotnet/aspnet:8.0-alpine
WORKDIR /app

# Add ICU for globalization (optional if DotLiquid uses culture)
RUN apk add --no-cache icu-libs

COPY --from=build /out .

USER app
EXPOSE 8080
ENTRYPOINT ["dotnet", "LiquidTransform.Web.dll", "--urls", "http://0.0.0.0:8080"]
