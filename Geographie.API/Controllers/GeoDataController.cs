using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Geographie.API.Data;
using Geographie.API.Models;
using CsvHelper;
using System.Globalization;
using Geographie.API.Mappings;
using CsvHelper.Configuration;

namespace Geographie.API.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/[controller]")]
    public class GeoDataController : ControllerBase
    {
        private readonly GeoDataContext _context;
        private readonly ILogger<GeoDataController> _logger;

        public GeoDataController(GeoDataContext context, ILogger<GeoDataController> logger)
        {
            _context = context;
            _logger = logger;
        }

        // GET: api/GeoData
        [HttpGet]
        public async Task<ActionResult<IEnumerable<GeographieData>>> GetGeographies()
        {
            if (_context.Geographies == null)
            {
                return NotFound();
            }

            try
            {
                return await _context.Geographies.ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Het ophalen was mislukt.");
                return StatusCode(500, "Er is iets misgegaan bij het ophalen van de gegevens.");
            }
        }

        // GET: api/GeoData/5
        [HttpGet("{id}")]
        public async Task<ActionResult<GeographieData>> GetGeographieData(int id)
        {
            if (_context.Geographies == null)
            {
                return NotFound();
            }

            var geographieData = await _context.Geographies.FindAsync(id);

            if (geographieData == null)
            {
                return NotFound();
            }

            return geographieData;
        }

        // PUT: api/GeoData/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutGeographieData(int id, GeographieData geographieData)
        {
            if (id != geographieData.Id)
            {
                return BadRequest();
            }

            _context.Entry(geographieData).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!GeographieDataExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/GeoData
        [HttpPost]
        public async Task<ActionResult<GeographieData>> PostGeographieData(GeographieData geographieData)
        {
            if (_context.Geographies == null)
            {
                return Problem("Entity set 'GeoDataContext.Geographies'  is null.");
            }
            try
            {
                _context.Geographies.Add(geographieData);
                await _context.SaveChangesAsync();

                return CreatedAtAction("GetGeographieData", new { id = geographieData.Id }, geographieData);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Toevoegen van een record mislukt.");
                return StatusCode(500, "Er is iets misgegaan bij het toevoegen van deze record.");
            }
        }

        // DELETE: api/GeoData/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteGeographieData(int id)
        {
            if (_context.Geographies == null)
            {
                return NotFound();
            }
            var geographieData = await _context.Geographies.FindAsync(id);
            if (geographieData == null)
            {
                return NotFound();
            }

            _context.Geographies.Remove(geographieData);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool GeographieDataExists(int id)
        {
            return (_context.Geographies?.Any(e => e.Id == id)).GetValueOrDefault();
        }

        [HttpPost("import")]
        public async Task<IActionResult> ImportCSV(IFormFile file)
        {
            if (file == null || file.Length == 0) return BadRequest("Geen bestand ontvangen.");

            try
            {
                using var stream = file.OpenReadStream();
                using var reader = new StreamReader(stream);
                var config = new CsvConfiguration(CultureInfo.InvariantCulture)
                {
                    Delimiter = ";", // Gebruik de puntkomma als seperator
                    TrimOptions = TrimOptions.Trim,
                    IgnoreBlankLines = true
                };
                using var csv = new CsvReader(reader, config);
                csv.Context.RegisterClassMap<GeographieDataMap>();

                var records = csv.GetRecords<GeographieData>().ToList();
                var validRecords = new List<GeographieData>();

                foreach (var record in records)
                {
                    if (!int.TryParse(record.Huisnummer, out var huisnummer)) huisnummer = 0;
                    if (!int.TryParse(record.Huisnummertoevoeging, out var huisnummertoev)) huisnummertoev = 0;
                    if (!int.TryParse(record.Oppervlakteverblijfsobject, out var opper)) opper = 0;
                    if (!int.TryParse(record.Pandbouwjaar, out var pbouwjaar)) pbouwjaar = 0;
                    if (!int.TryParse(record.X, out var tx)) tx = 0;
                    if (!int.TryParse(record.Y, out var ty)) ty = 0;
                    if (!double.TryParse(record.Lon?.Replace(".", "").Replace(",", "."), out var lon)) lon = 0;
                    if (!double.TryParse(record.Lat?.Replace(".", "").Replace(",", "."), out var lat)) lat = 0;

                    validRecords.Add(new GeographieData
                    {
                        Openbareruimte = record.Openbareruimte,
                        Huisnummer = huisnummer.ToString(),
                        Huisletter = record.Huisletter,
                        Huisnummertoevoeging = huisnummertoev.ToString(),
                        Postcode = record.Postcode,
                        Woonplaats = record.Woonplaats,
                        Gemeente = record.Gemeente,
                        Provincie = record.Provincie,
                        Nummeraanduiding = record.Nummeraanduiding,
                        Verblijfsobjectgebruiksdoel = record.Verblijfsobjectgebruiksdoel,
                        Oppervlakteverblijfsobject = opper.ToString(),
                        Verblijfsobjectstatus = record.Verblijfsobjectstatus,
                        ObjectId = record.ObjectId,
                        ObjectType = record.ObjectType,
                        Nevenadres = record.Nevenadres,
                        PandId = record.PandId,
                        Pandstatus = record.Pandstatus,
                        Pandbouwjaar = pbouwjaar.ToString(),
                        X = tx.ToString(),
                        Y = ty.ToString(),
                        Lon = lon.ToString(),
                        Lat = lat.ToString()
                    });
                }

                _context.Geographies.AddRange(validRecords);
                await _context.SaveChangesAsync();
                return Ok(new
                {
                    AlleRecords = records.Count
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Fout bij CSV import.");
                return StatusCode(500, "Er is een fout opgetreden bij het importeren van je CSV bestand.");
            }
        }
    }
}
