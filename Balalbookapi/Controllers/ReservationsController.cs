using Balalbookapi.Data;
using Balalbookapi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Balalbookapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReservationsController : ControllerBase
    {
        private readonly BookDbContext _context;

        public ReservationsController(BookDbContext context)
        {
            _context = context;
        }

        // GET: api/ProductCategories
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Reservation>>> GetReservations()
        {
            if (_context.reservations == null)
            {
                return NotFound();
            }
            return await _context.reservations.ToListAsync();
        }

        // GET: api/ProductCategories/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Reservation>> GetReservation(int id)
        {
            if (_context.reservations == null)
            {
                return NotFound();
            }
            var reservat = await _context.reservations.FindAsync(id);

            if (reservat == null)
            {
                return NotFound();
            }

            return reservat;
        }

        // PUT: api/ProductCategories/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutReservation(int id, Reservation reservat)
        {
            if (id != reservat.Id)
            {
                return BadRequest();
            }

            _context.Entry(reservat).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ReservationExists(id))
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

        // POST: api/ProductCategories
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Reservation>> PostReservation(Reservation reservat)
        {
            if (_context.reservations == null)
            {
                return Problem("Entity set 'StoreSalePointContext.ProductCategories'  is null.");
            }
            _context.reservations.Add(reservat);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetReservation", new { id = reservat.Id }, reservat);
        }

        // DELETE: api/ProductCategories/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteReservation(int id)
        {
            if (_context.reservations == null)
            {
                return NotFound();
            }
            var reservat = await _context.reservations.FindAsync(id);
            if (reservat == null)
            {
                return NotFound();
            }

            _context.reservations.Remove(reservat);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ReservationExists(int id)
        {
            return (_context.reservations?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
