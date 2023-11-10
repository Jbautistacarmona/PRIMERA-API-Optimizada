using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Text.Json.Serialization;

namespace PRIMERA_API.Data.Models
{
    public class Alquiler
    {
        public int AlquilerID { get; set; }
        public int ClienteID { get; set; }
        public int TipoVehiculoID { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }
        public decimal MontoCobro { get; set; } // Asegúrate de que el tipo de dato sea el correcto

        // Propiedades de navegación para establecer relaciones
        //[JsonIgnore]
        public virtual Cliente? Cliente { get; set; }
        //[JsonIgnore]
        public virtual Tipovehiculo? Tipovehiculo { get; set; }

    }
}
