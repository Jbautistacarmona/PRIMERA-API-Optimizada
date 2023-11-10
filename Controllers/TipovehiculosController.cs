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
    public class TipovehiculosController : ControllerBase
    {
        private readonly PARCIAL1Context _context;
        private readonly IMapper mapper;

        public TipovehiculosController(PARCIAL1Context context, IMapper mapper)
        {
            _context = context;
            this.mapper = mapper;
        }

        // GET: api/Tipovehiculos
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Tipovehiculo>>> GetTipoVehiculo()
        {
            var tipovehiculo = await _context.TipoVehiculo.ToListAsync();

            if (tipovehiculo == null)
            {
                return NotFound();
            }

            return tipovehiculo;
        }

        // GET: api/Tipovehiculos/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Tipovehiculo>> GetTipovehiculo(int id)
        {
          if (_context.TipoVehiculo == null)
          {
              return NotFound();
          }
            var tipovehiculo = await _context.TipoVehiculo.FindAsync(id);

            if (tipovehiculo == null)
            {
                return NotFound();
            }

            return tipovehiculo;
        }

        // PUT: api/Tipovehiculos/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTipovehiculo(int id, TipovehiculoUpdateDto tiposvehiculodto)
        {
            if (id != tiposvehiculodto.TipoVehiculoID)
            {
                return BadRequest();
            }
            var tipovehiculo = mapper.Map<Tipovehiculo>(tiposvehiculodto);

            _context.Entry(tipovehiculo).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TipovehiculoExists(id))
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

        // POST: api/Tipovehiculos
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Tipovehiculo>> PostTipovehiculo(TipovehiculoCrearDto tiposvehiculodto)
        {
          if (_context.TipoVehiculo == null)
          {
              return Problem("Entity set 'PARCIAL1Context.TiposVehiculo'  is null.");
          }
            var tipovehiculo = mapper.Map<Tipovehiculo>(tiposvehiculodto);
            _context.TipoVehiculo.Add(tipovehiculo);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTipovehiculo", new { id = tipovehiculo.TipoVehiculoID }, tipovehiculo);
        }

        // DELETE: api/Tipovehiculos/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTipovehiculo(int id)
        {
            if (_context.TipoVehiculo == null)
            {
                return NotFound();
            }
            var tipovehiculo = await _context.TipoVehiculo.FindAsync(id);
            if (tipovehiculo == null)
            {
                return NotFound();
            }

            _context.TipoVehiculo.Remove(tipovehiculo);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TipovehiculoExists(int id)
        {
            return (_context.TipoVehiculo?.Any(e => e.TipoVehiculoID == id)).GetValueOrDefault();
        }
    }
}
