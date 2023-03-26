using languageSchoolAPI.Context;
using languageSchoolAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
        public async Task<ActionResult<IEnumerable<LogEntryModel>>> GetLogEntry()
        {
            return await _context.LogEntry.ToListAsync();
        }

        // GET: api/LogEntry/5
        [HttpGet("{id}")]
        public async Task<ActionResult<LogEntryModel>> GetLogEntry(int id)
        {
            var logEntry = await _context.LogEntry.FindAsync(id);

            if (logEntry == null)
            {
                return NotFound();
            }

            return logEntry;
        }



        //private bool LogEntryExists(int id)
        //{
        //    return _context.LogEntry.Any(e => e.Id == id);
        //}

        [HttpPost]
        public async Task<ActionResult<LogEntryModel>> CreateLogEntry(string description, string type)
        {
            LogEntryModel log = new LogEntryModel();
            log.Description = description;
            log.Date = DateTime.Now;
            log.Type = type;

            _context.LogEntry.Add(log);
            await _context.SaveChangesAsync();

            return Ok();
        }
    }
}
