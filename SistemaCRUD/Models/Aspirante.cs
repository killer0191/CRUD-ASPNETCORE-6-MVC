using System.Reflection.Metadata;

namespace SistemaCRUD.Models
{
    public class Aspirante
    {
        public int id { get; set; }
        public string nombre { get; set; }
        public string apellido { get; set; }
        public string correoElectronico{ get; set; }
        public string numTelefonico { get; set; }
        public string lugarNacimiento { get; set; }
    }
}
