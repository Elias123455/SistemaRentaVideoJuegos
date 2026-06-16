using System;

namespace RentaVideojuegos.Models
{
    public class Jugador
    {
        public int IdJugador { get; set; }
        public string NombreCompleto { get; set; }
        public string Email { get; set; }
        public string Clave { get; set; }
        public bool EsAdministrador { get; set; }
        public string Estado { get; set; }
    }
}