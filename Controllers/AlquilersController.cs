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
using Newtonsoft.Json;

namespace PRIMERA_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AlquilersController : ControllerBase
    {
        private readonly PARCIAL1Context _context;
        private readonly IMapper mapper;

        public AlquilersController(PARCIAL1Context context, IMapper mapper)
        {
            _context = context;
            this.mapper = mapper;
        }
        // GET: api/Alquilers
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Alquiler>>> GetAlquiler()
        {
            var alquileres = await _context.Alquiler.ToListAsync();

            if (alquileres == null)
            {
                return NotFound();
            }

            return alquileres;
        }
        // GET: api/Alquilers/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Alquiler>> GetAlquiler(int id)
        {
          if (_context.Alquiler == null)
          {
              return NotFound();
          }
            var alquiler = await _context.Alquiler.FindAsync(id);

            if (alquiler == null)
            {
                return NotFound();
            }
            return alquiler;
        }
        // PUT: api/Alquilers/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAlquiler(int id, AlquilerUpdateDto alquilerdto)
        {
            if (id != alquilerdto.AlquilerID)
            {
                return BadRequest();
            }
            var alquiler = mapper.Map<Alquiler>(alquilerdto);

            _context.Entry(alquiler).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AlquilerExists(id))
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
        // POST: api/Alquilers
        [HttpPost]
        public async Task<ActionResult<Alquiler>> PostAlquiler(AlquilerCrearDto alquilerdto)
        {
            if (_context.Alquiler == null)
            {
                return Problem("Entity set 'PARCIAL1Context.Alquiler' is null.");
            }

            var alquiler = mapper.Map<Alquiler>(alquilerdto);

            // Establece la Fecha de Inicio con la fecha actual
            alquiler.FechaInicio = DateTime.Now;

            _context.Alquiler.Add(alquiler);
            await _context.SaveChangesAsync();

            // El AlquilerID se generará automáticamente después de agregarlo a la base de datos

            return CreatedAtAction("GetAlquiler", new { id = alquiler.AlquilerID }, alquiler);
        }
        // DELETE: api/Alquilers/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAlquiler(int id)
        {
            if (_context.Alquiler == null)
            {
                return NotFound();
            }
            var alquiler = await _context.Alquiler.FindAsync(id);
            if (alquiler == null)
            {
                return NotFound();
            }
            _context.Alquiler.Remove(alquiler);
            await _context.SaveChangesAsync();
            return NoContent();
        }
        private bool AlquilerExists(int id)
        {
            return (_context.Alquiler?.Any(e => e.AlquilerID == id)).GetValueOrDefault();
        }
    }
}
