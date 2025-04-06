using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Geographie.API.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Geographies",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Openbareruimte = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Huisnummer = table.Column<int>(type: "int", nullable: false),
                    Huisletter = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Huisnummertoevoeging = table.Column<int>(type: "int", nullable: false),
                    Postcode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Woonplaats = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Gemeente = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Provincie = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Nummeraanduiding = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Verblijfsobjectgebruiksdoel = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Oppervlakteverblijfsobject = table.Column<int>(type: "int", nullable: false),
                    Verblijfsobjectstatus = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ObjectId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ObjectType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Nevenadres = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PandId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Pandstatus = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Pandbouwjaar = table.Column<int>(type: "int", nullable: false),
                    X = table.Column<int>(type: "int", nullable: false),
                    Y = table.Column<int>(type: "int", nullable: false),
                    Lon = table.Column<double>(type: "float", nullable: false),
                    Lat = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Geographies", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Geographies");
        }
    }
}
