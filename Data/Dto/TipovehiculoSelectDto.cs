using System.ComponentModel.DataAnnotations;

namespace PRIMERA_API.Data.Dto
{
    public class TipovehiculoSelectDto
    {
        public int TipoVehiculoID { get; set; }
        public string? Nombre { get; set; } = null!;
        public decimal TarifaPorDia { get; set; }
    }
}
