using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyApp.Data;
using MyApp.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ViewHistoryController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ViewHistoryController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ViewHistory>>> GetViewHistories()
        {
            return await _context.ViewHistories.Include(vh => vh.Product).ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ViewHistory>> GetViewHistory(int id)
        {
            var viewHistory = await _context.ViewHistories.Include(vh => vh.Product).FirstOrDefaultAsync(vh => vh.Id == id);

            if (viewHistory == null)
            {
                return NotFound();
            }

            return viewHistory;
        }

        [HttpPost]
        public async Task<ActionResult<ViewHistory>> PostViewHistory(ViewHistory viewHistory)
        {
            _context.ViewHistories.Add(viewHistory);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetViewHistory), new { id = viewHistory.Id }, viewHistory);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutViewHistory(int id, ViewHistory viewHistory)
        {
            if (id != viewHistory.Id)
            {
                return BadRequest();
            }

            _context.Entry(viewHistory).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ViewHistoryExists(id))
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

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteViewHistory(int id)
        {
            var viewHistory = await _context.ViewHistories.FindAsync(id);
            if (viewHistory == null)
            {
                return NotFound();
            }

            _context.ViewHistories.Remove(viewHistory);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ViewHistoryExists(int id)
        {
            return _context.ViewHistories.Any(e => e.Id == id);
        }
    }
}

