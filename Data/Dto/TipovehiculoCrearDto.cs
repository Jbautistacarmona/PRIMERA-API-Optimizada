using System.ComponentModel.DataAnnotations;

namespace PRIMERA_API.Data.Dto
{
    public class TipovehiculoCrearDto
    {
        public string? Nombre { get; set; } = null!;
        public decimal TarifaPorDia { get; set; }
    }
}
