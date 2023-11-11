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
        private readonly IMapper _mapper;

        public TipovehiculosController(PARCIAL1Context context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Tipovehiculo>>> GetTipoVehiculo()
        {
            var tipovehiculo = await _context.TipoVehiculo.ToListAsync();

            if (tipovehiculo == null || !tipovehiculo.Any())
            {
                return NotFound();
            }

            return tipovehiculo;
        }
        // GET: api/Tipovehiculos/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Tipovehiculo>> GetTipovehiculo(int id)
        {
            var tipovehiculo = await _context.TipoVehiculo.FindAsync(id);

            if (tipovehiculo == null)
            {
                return NotFound();
            }

            return tipovehiculo;
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTipovehiculo(int id, TipovehiculoUpdateDto tiposvehiculodto)
        {
            try
            {
                if (id != tiposvehiculodto.TipoVehiculoID)
                {
                    return BadRequest("ID no coincide con el objeto a actualizar.");
                }

                var existingTipovehiculo = await _context.TipoVehiculo.FindAsync(id);

                if (existingTipovehiculo == null)
                {
                    return NotFound("Tipo de vehículo no encontrado.");
                }

                // Actualizamos solo las propiedades necesarias
                existingTipovehiculo.Nombre = tiposvehiculodto.Nombre;
                existingTipovehiculo.TarifaPorDia = tiposvehiculodto.TarifaPorDia;

                _context.Entry(existingTipovehiculo).State = EntityState.Modified;

                await _context.SaveChangesAsync();

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno del servidor: {ex.Message}");
            }
        }
        [HttpPost]
        public async Task<ActionResult<Tipovehiculo>> PostTipovehiculo(TipovehiculoCrearDto tipoVehiculoDto)
        {
            var tipovehiculo = _mapper.Map<Tipovehiculo>(tipoVehiculoDto);

            _context.TipoVehiculo.Add(tipovehiculo);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetTipovehiculo), new { id = tipovehiculo.TipoVehiculoID }, tipovehiculo);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTipovehiculo(int id)
        {
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
            return _context.TipoVehiculo.Any(e => e.TipoVehiculoID == id);
        }
    }
}

