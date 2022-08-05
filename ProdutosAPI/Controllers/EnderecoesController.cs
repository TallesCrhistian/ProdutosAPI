using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProdutosAPI.Data;
using ProdutosAPI.Models;

namespace ProdutosAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EnderecoesController : ControllerBase
    {
        private readonly AppDbContext _context;

        public EnderecoesController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/Enderecoes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Endereco>>> GetEnderecoes()
        {
          if (_context.Enderecoes == null)
          {
              return NotFound();
          }
            return await _context.Enderecoes.ToListAsync();
        }

        // GET: api/Enderecoes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Endereco>> GetEndereco(int id)
        {
          if (_context.Enderecoes == null)
          {
              return NotFound();
          }
            var endereco = await _context.Enderecoes.FindAsync(id);

            if (endereco == null)
            {
                return NotFound();
            }

            return endereco;
        }

        // PUT: api/Enderecoes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEndereco(int id, Endereco endereco)
        {
            if (id != endereco.Id)
            {
                return BadRequest();
            }

            _context.Entry(endereco).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EnderecoExists(id))
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

        // POST: api/Enderecoes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Endereco>> PostEndereco(Endereco endereco)
        {
          if (_context.Enderecoes == null)
          {
              return Problem("Entity set 'AppDbContext.Enderecoes'  is null.");
          }
            _context.Enderecoes.Add(endereco);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetEndereco", new { id = endereco.Id }, endereco);
        }

        // DELETE: api/Enderecoes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEndereco(int id)
        {
            if (_context.Enderecoes == null)
            {
                return NotFound();
            }
            var endereco = await _context.Enderecoes.FindAsync(id);
            if (endereco == null)
            {
                return NotFound();
            }

            _context.Enderecoes.Remove(endereco);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool EnderecoExists(int id)
        {
            return (_context.Enderecoes?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
