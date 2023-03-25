using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using languageSchoolAPI.Context;
using languageSchoolAPI.Models;

namespace languageSchoolAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LogEntryController : ControllerBase
    {
        private readonly LogEntryContext _context;

        public LogEntryController(LogEntryContext context)
        {
            _context = context;
        }

        // GET: api/LogEntry
        [HttpGet]
        public async Task<ActionResult<IEnumerable<LogEntry>>> GetLogEntries()
        {
            return await _context.LogEntries.ToListAsync();
        }

        // GET: api/LogEntry/5
        [HttpGet("{id}")]
        public async Task<ActionResult<LogEntry>> GetLogEntry(int id)
        {
            var logEntry = await _context.LogEntries.FindAsync(id);

            if (logEntry == null)
            {
                return NotFound();
            }

            return logEntry;
        }

        // PUT: api/LogEntry/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutLogEntry(int id, LogEntry logEntry)
        {
            if (id != logEntry.Id)
            {
                return BadRequest();
            }

            _context.Entry(logEntry).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LogEntryExists(id))
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

        // POST: api/LogEntry
        [HttpPost]
        public async Task<ActionResult<LogEntry>> PostLogEntry(LogEntry logEntry)
        {
            _context.LogEntries.Add(logEntry);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetLogEntry", new { id = logEntry.Id }, logEntry);
        }

        // DELETE: api/LogEntry/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteLogEntry(int id)
        {
            var logEntry = await _context.LogEntries.FindAsync(id);
            if (logEntry == null)
            {
                return NotFound();
            }

            _context.LogEntries.Remove(logEntry);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool LogEntryExists(int id)
        {
            return _context.LogEntries.Any(e => e.Id == id);
        }
    }
}
