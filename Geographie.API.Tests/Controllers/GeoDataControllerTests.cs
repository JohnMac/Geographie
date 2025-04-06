using FluentAssertions;
using Geographie.API.Controllers;
using Geographie.API.Data;
using Geographie.API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Geographie.API.Tests.Controllers
{
    public class GeoDataControllerTests
    {
        // Helper method without a parameter
        private GeoDataController CreateController()
        {
            var options = new DbContextOptionsBuilder<GeoDataContext>()
                .UseInMemoryDatabase(databaseName: "GeoDataTestsDB")
                .Options;

            var context = new GeoDataContext(options);

            context.Geographies.Add(new GeographieData
            {
                Openbareruimte = "Laan van Noi",
                Huisnummer = "22",
                Postcode = "1133BA",
                Woonplaats = "WoningEen"
            });

            context.SaveChanges();
            var logger = LoggerFactory.Create(builder => builder.AddConsole()).CreateLogger<GeoDataController>();
            return new GeoDataController(context, logger);
        }

        // Helper setup method with a parameter
        private (GeoDataController controller, GeoDataContext context) CreateController(string dbName)
        {
            var options = new DbContextOptionsBuilder<GeoDataContext>()
                .UseInMemoryDatabase(databaseName: dbName)
                .Options;

            var context = new GeoDataContext(options);
            var logger = LoggerFactory.Create(builder => builder.AddConsole()).CreateLogger<GeoDataController>();
            var controller = new GeoDataController(context, logger);
            return (controller, context);
        }

        // Return all available geo data without specific Ids
        [Fact]
        public async Task GetGeoData_ReturnsAllItems()
        {
            var controller = CreateController();
            var result = await controller.GetGeographies();

            // Assert
            result.Should().NotBeNull();
            var data = result.Value as List<Models.GeographieData>;
            data.Should().NotBeNull();
            data.Should().HaveCountGreaterThan(0);
        }

        // Return a specific record with an Id
        [Fact]
        public async Task GetGeoDataById_ReturnsCorrectRecord()
        {
            var dbName = nameof(GetGeoDataById_ReturnsCorrectRecord);
            var (controller, context) = CreateController(dbName);
            var record = new GeographieData
            {
                Openbareruimte = "Straterweg",
                Huisnummer = "1"
            };
            context.Geographies.Add(record);
            context.SaveChanges();

            var result = await controller.GetGeographieData(record.Id);

            // Assert
            result.Should().NotBeNull();
            var data = result.Value as GeographieData;
            data.Should().NotBeNull();
            data.Id.Should().Be(record.Id);
        }

        // Unit test for the Post method
        [Fact]
        public async Task PostGeoData_CreatesNewRecord()
        {
            var dbName = nameof(PostGeoData_CreatesNewRecord);
            var (controller, context) = CreateController(dbName);
            var record = new GeographieData
            {
                Openbareruimte = "Laanerweg",
                Woonplaats = "Dorp aan zee"
            };

            var result = await controller.PostGeographieData(record);

            // Assert
            var createdResult = result.Result as CreatedAtActionResult;
            createdResult.Should().NotBeNull();
            var data = createdResult.Value as GeographieData;
            data.Should().NotBeNull();
            data.Openbareruimte.Should().Be("Laanerweg");
        }

        // Unit test for the Put method with an Id
        [Fact]
        public async Task PutGeoData_UpdatesRecord()
        {
            var dbName = nameof(PutGeoData_UpdatesRecord);
            var (controller, context) = CreateController(dbName);
            var record = new GeographieData
            {
                Openbareruimte = "Andere ruimte",
                Huisnummer = "2"
            };
            context.Geographies.Add(record);
            context.SaveChanges();

            var updatedRecord = record;
            updatedRecord.Openbareruimte = "Aangepaste ruimte";

            var result = await controller.PutGeographieData(record.Id, updatedRecord);

            // Assert
            result.Should().BeOfType<NoContentResult>();
            var updated = await context.Geographies.FindAsync(record.Id);
            updated.Should().NotBeNull();
            updated.Openbareruimte.Should().Be("Aangepaste ruimte");
        }

        // Unit test for the Delete method with an Id
        [Fact]
        public async Task DeleteGeoData_RemovesRecord()
        {
            var dbName = nameof(DeleteGeoData_RemovesRecord);
            var (controller, context) = CreateController(dbName);
            var record = new GeographieData
            {
                Openbareruimte = "Te verwijderen record",
                Huisnummer = "6"
            };
            context.Geographies.Add(record);
            context.SaveChanges();

            var result = await controller.DeleteGeographieData(record.Id);

            // Assert 
            result.Should().BeOfType<NoContentResult>();
            var deleted = await context.Geographies.FindAsync(record.Id);
            deleted.Should().BeNull();
        }
    }
}
