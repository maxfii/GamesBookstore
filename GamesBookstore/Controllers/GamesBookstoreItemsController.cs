using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using GamesBookstore.Models;

namespace GamesBookstore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GamesBookstoreItemsController : ControllerBase
    {
        private readonly GamesBookstoreContext _context;
        private readonly ILogger<GamesBookstoreContext> _logger;

        public GamesBookstoreItemsController(ILogger<GamesBookstoreContext> logger, GamesBookstoreContext context)
        {
            _context = context;
            _logger = logger;
        }

        // GET: api/GamesBookstoreItems
        [HttpGet]
        public async Task<ActionResult<IEnumerable<GamesBookstoreItem>>> GetGamesBookstoreItems()
        {

            _logger.LogInformation("GET: all GamesBookstoreItems");
            return await _context.GamesBookstoreItems.ToListAsync();
        }

        // GET: api/GamesBookstoreItems/5
        [HttpGet("{id}")]
        public async Task<ActionResult<GamesBookstoreItem>> GetGamesBookstoreItem(long id)
        {
            var gamesBookstoreItem = await _context.GamesBookstoreItems.FindAsync(id);
            _logger.LogInformation("Getting item {Id}", id);

            if (gamesBookstoreItem == null)
            {
                _logger.LogWarning("GET({id}) NOT FOUND", id);
                return NotFound();
            }

            return gamesBookstoreItem;
        }

        // PUT: api/GamesBookstoreItems/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutGamesBookstoreItem(long id, GamesBookstoreItem gamesBookstoreItem)
        {
            _logger.LogInformation("Modifying item N {id}", id);
            if (id != gamesBookstoreItem.ID)
            {
                _logger.LogError("Id from request body doesn't equal to id in the method argument");
                return BadRequest();
            }

            _context.Entry(gamesBookstoreItem).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!GamesBookstoreItemExists(id))
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

        // POST: api/GamesBookstoreItems
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<GamesBookstoreItem>> PostGamesBookstoreItem(GamesBookstoreItem item)
        {
            _logger.LogInformation("Creating item N {id}", item.ID);
            _context.GamesBookstoreItems.Add(item);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetGamesBookstoreItem", new { id = item.ID }, item);
        }

        // DELETE: api/GamesBookstoreItems/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteGamesBookstoreItem(long id)
        {
            _logger.LogInformation("Deleting item N {id}", id);
            var gamesBookstoreItem = await _context.GamesBookstoreItems.FindAsync(id);
            if (gamesBookstoreItem == null)
            {
                _logger.LogInformation("ERROR: No found item with id: {id}", id);
                return NotFound();
            }

            _context.GamesBookstoreItems.Remove(gamesBookstoreItem);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool GamesBookstoreItemExists(long id)
        {
            return _context.GamesBookstoreItems.Any(e => e.ID == id);
        }
    }
}
