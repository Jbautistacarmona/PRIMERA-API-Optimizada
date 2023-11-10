using PRIMERA_API.Data.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace PRIMERA_API.Data.Dto
{
    public class AlquilerCrearDto
    {
        public int ClienteID { get; set; }
        public int TipoVehiculoID { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }
        public decimal MontoCobro { get; set; } // Asegúrate de que el tipo de dato sea el correcto
    }
}
