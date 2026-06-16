using System;

namespace RentaVideojuegos.Models
{
    public class Videojuego
    {
        public int IdVideojuego { get; set; }
        public int IdSucursal { get; set; }
        public string Titulo { get; set; }
        public string Descripcion { get; set; }
        public string IdCategoria { get; set; }
        public DateTime FechaLanzamiento { get; set; }
        public string Desarrolladora { get; set; }
        public string Distribuidora { get; set; }
        public string Imagen { get; set; }
        public string Trailer { get; set; }
        public string Estado { get; set; }
    }
}