# Χρησιμοποιούμε την επίσημη εικόνα της Microsoft για .NET 8
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

# Αντιγράφουμε το project και επαναφέρουμε τα πακέτα
COPY *.csproj .
RUN dotnet restore

# Αντιγράφουμε όλο τον κώδικα και κάνουμε build
COPY . .
RUN dotnet publish -c Release -o out

# Δημιουργούμε την τελική, μικρότερη εικόνα με το runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app

# Αντιγράφουμε τα published αρχεία από το build στάδιο
COPY --from=build /app/out .

# Θέτουμε τη θύρα που θα ακούει η εφαρμογή
ENV ASPNETCORE_URLS=http://+:80

# Λέμε στο Docker ποια εντολή να τρέξει για να ξεκινήσει η εφαρμογή
ENTRYPOINT ["dotnet", "Backend.dll"]