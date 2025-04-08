# GeoData Applicatie

Een full-stack webapplicatie voor het beheren van geografische gegevens.  
De backend is gebouwd met .NET 7 Web API, en de frontend met Angular 17 (standalone).

## Inhoud

- ✅ .NET 7 Web API (Entity Framework Core, SQL Server, Swagger, Docker)
- ✅ Angular 17 frontend (Material UI, reactive forms, routing)
- ✅ CSV-import feature (optioneel)
- ✅ API-versiebeheer en unit tests
- ✅ Gehost in Docker of lokaal

## Installatie-instructies

### Backend (.NET API)

#### Vereisten:
- [.NET SDK 7.0]
- SQL Server Express / Docker
- Visual Studio 2022+ (optioneel)
- Swagger (automatisch actief bij start)

#### Build en run:
```bash
cd Geographie.API
dotnet build
dotnet run
```

#### API draait op:
- https://localhost:7028 (via Visual Studio)
- Of https://localhost:32771 (indien in Docker)

#### Database setup:
- Indien nog niet uitgevoerd, eerst een migratie toevoegen via Visual Studio, en daarna:
dotnet ef database update
- Vervolgens in Visual Studio:
Update-Database

### Frontend (Angular)
#### Vereisten:
- Node.js 18+
- Angular CLI 17+

#### Installatie
- cd geo-data-frontend
- npm install

#### Lokaal runnen
- npm start, of ng serve
- Frontend bereikbaar op http://localhost:4200

## API-documentatie met Swagger
### Endpoints
- GET /api/v1/GeoData
- POST /api/v1/GeoData
- GET /api/v1//GeoData{id}
- PUT /api/v1//GeoData{id}
- DELETE /api/v1//GeoData{id}
- POST /api/v1//GeoData/import

## Unit Tests 
### Testproject: Geographie.API.Tests\
- Unit tests voor de happy flow van de Controller Endpoints die hierboven zijn genoemd.

## Docker
- Backend container start automatisch bij start van Visual Studio.
- Docker runt met: docker run -p 32771:443 .
