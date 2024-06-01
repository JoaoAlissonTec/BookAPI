using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BookAPI.Data;
using BookAPI.Models;
using BookAPI.Utils;

namespace BookAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorsController : ControllerBase
    {
        private readonly BookCatalogDBContext _context;

        public AuthorsController(BookCatalogDBContext context)
        {
            _context = context;
        }

        // GET: api/Authors
        [HttpGet("{take:int}/{page:int}")]
        public async Task<ActionResult<IEnumerable<Author>>> GetAuthors(
            [FromRoute] int take = 25,
            [FromRoute] int page = 1)
        {
            if (take > 1000)
            {
                return BadRequest();
            }

            var total = await _context.Authors.CountAsync();
            PaginationResult? pagination = Util.CalculatePagination(total, take, page);

            if (page < 1 || page > pagination.TotalPages && take > 0)
            {
                return NotFound();
            }


            var authors = await _context.
                Authors
                .AsNoTracking()
                .Skip(pagination.StartIndex)
                .Take(take)
                .ToListAsync();

            return Ok(new
            {
                total,
                pagination,
                data = authors
            });
        }

        // GET: api/Authors/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Author>> GetAuthor(int id)
        {
            var author = await _context.Authors.FindAsync(id);

            if (author == null)
            {
                return NotFound();
            }

            return author;
        }

        // PUT: api/Authors/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAuthor(int id, Author author)
        {
            if (id != author.Id)
            {
                return BadRequest();
            }

            _context.Entry(author).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AuthorExists(id))
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

        // POST: api/Authors
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Author>> PostAuthor(Author author)
        {
            _context.Authors.Add(author);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetAuthor", new { id = author.Id }, author);
        }

        // DELETE: api/Authors/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAuthor(int id)
        {
            var author = await _context.Authors.FindAsync(id);
            if (author == null)
            {
                return NotFound();
            }

            _context.Authors.Remove(author);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool AuthorExists(int id)
        {
            return _context.Authors.Any(e => e.Id == id);
        }
    }
}
