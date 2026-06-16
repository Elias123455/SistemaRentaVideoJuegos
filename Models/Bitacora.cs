using System;

namespace RentaVideojuegos.Models
{
    public class Bitacora
    {
        public int IdBitacora { get; set; }
        public int IdAlquiler { get; set; }
        public int IdJugador { get; set; }
        public string AccionRealizada { get; set; }
        public DateTime FechaDeLaAccion { get; set; }
    }
}