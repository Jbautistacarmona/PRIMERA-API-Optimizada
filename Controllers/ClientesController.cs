using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PRIMERA_API.Data;
using PRIMERA_API.Data.Dto;
using PRIMERA_API.Data.Models;

namespace PRIMERA_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientesController : ControllerBase
    {
        private readonly PARCIAL1Context _context;
        private readonly IMapper mapper;

        public ClientesController(PARCIAL1Context context, IMapper mapper)
        {
            _context = context;
            this.mapper = mapper;
        }

        // GET: api/Clientes
        public async Task<ActionResult<IEnumerable<Cliente>>> GetCliente()
        {
            var clientes = await _context.Cliente.ToListAsync();

            if (clientes == null)
            {
                return NotFound();
            }

            return clientes;
        }
        // GET: api/Clientes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Cliente>> GetCliente(int id)
        {
          if (_context.Cliente == null)
          {
              return NotFound();
          }
            var cliente = await _context.Cliente.FindAsync(id);

            if (cliente == null)
            {
                return NotFound();
            }

            return cliente;
        }
        // PUT: api/Clientes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCliente(int id, ClienteUpdateDto clientedto)
        {
            if (id != clientedto.ClienteID)
            {
                return BadRequest();
            }
            var cliente = mapper.Map<Cliente>(clientedto);

            _context.Entry(cliente).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ClienteExists(id))
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
        // POST: api/Clientes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Cliente>> PostCliente(ClienteCrearDto clientedto)
        {
          if (_context.Cliente == null)
          {
              return Problem("Entity set 'PARCIAL1Context.Cliente'  is null.");
          }

            var cliente = mapper.Map<Cliente>(clientedto);
            _context.Cliente.Add(cliente);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCliente", new { id = cliente.ClienteID }, cliente);
        }
        // DELETE: api/Clientes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCliente(int id)
        {
            if (_context.Cliente == null)
            {
                return NotFound();
            }
            var cliente = await _context.Cliente.FindAsync(id);
            if (cliente == null)
            {
                return NotFound();
            }

            _context.Cliente.Remove(cliente);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ClienteExists(int id)
        {
            return (_context.Cliente?.Any(e => e.ClienteID == id)).GetValueOrDefault();
        }
    }
}
