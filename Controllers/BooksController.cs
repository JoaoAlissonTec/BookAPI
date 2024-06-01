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
    public class BooksController : ControllerBase
    {
        private readonly BookCatalogDBContext _context;

        public BooksController(BookCatalogDBContext context)
        {
            _context = context;
        }

        // GET: api/Books
        [HttpGet("{take:int}/{page:int}")]
        public async Task<ActionResult<IEnumerable<Book>>> GetBooks(
            [FromRoute] int take = 25,
            [FromRoute] int page = 1)
        {
            if(take > 1000)
            {
                return BadRequest();
            }

            var total = await _context.Books.CountAsync();
            PaginationResult? pagination = Util.CalculatePagination(total, take, page);

            if (page < 1 || page > pagination.TotalPages && take > 0)
            {
                return NotFound();
            }


            var books = await _context.
                Books
                .Include(x => x.Author)
                .AsNoTracking()
                .Skip(pagination.StartIndex)
                .Take(take)
                .ToListAsync();

            return Ok(new
            {
                total,
                pagination,
                data = books
            });
        }

        // GET: api/Books/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Book>> GetBook(int id)
        {
            var book = await _context.Books.FindAsync(id);

            if (book == null)
            {
                return NotFound();
            }

            return book;
        }

        // PUT: api/Books/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBook(int id, Book book)
        {
            if (id != book.Id)
            {
                return BadRequest();
            }

            _context.Entry(book).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BookExists(id))
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

        // POST: api/Books
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Book>> PostBook(Book book)
        {
            _context.Books.Add(book);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetBook", new { id = book.Id }, book);
        }

        // DELETE: api/Books/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBook(int id)
        {
            var book = await _context.Books.FindAsync(id);
            if (book == null)
            {
                return NotFound();
            }

            _context.Books.Remove(book);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool BookExists(int id)
        {
            return _context.Books.Any(e => e.Id == id);
        }
    }
}
