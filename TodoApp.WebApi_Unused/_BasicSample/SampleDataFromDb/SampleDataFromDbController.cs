using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TodoApp.WebApi.Entities;
using TodoApp.WebApi.Helpers;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TodoApp.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SampleDataFromDbController : ControllerBase
    {
        private readonly DataContext _context;

        public SampleDataFromDbController(DataContext context)
        {
            _context = context;
        }

        // GET: api/SampleDataFromDb
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SampleDataFromDb>>> Get()
        {
            if (_context.SampleDataFromDb == null)
            {
                return NotFound();
            }
            return await _context.SampleDataFromDb.ToListAsync();
        }

        // GET api/SampleDataFromDb/5
        [HttpGet("{id}")]
        public async Task<ActionResult<SampleDataFromDb>> Get(int id)
        {
            if (_context.SampleDataFromDb == null)
            {
                return NotFound();
            }
            var data = await _context.SampleDataFromDb.FindAsync(id);

            if (data == null)
            {
                return NotFound();
            }

            return data;
        }

        // POST api/SampleDataFromDb
        [HttpPost]
        public async Task<ActionResult<SampleDataFromDb>> Post(SampleDataFromDb data)
        {
            if (_context.SampleDataFromDb == null)
            {
                return Problem($"Entity set {nameof(_context.SampleDataFromDb)} is null.");
            }

            _context.SampleDataFromDb.Add(data);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(Get), new { id = data.Id }, data);
        }
    }
}
