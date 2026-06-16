using System;

namespace RentaVideojuegos.Models
{
    public class Sucursal
    {
        public int IdSucursal { get; set; }
        public string Nombre { get; set; }
        public string Direccion { get; set; }
        public string Telefono { get; set; }
        public decimal CostoPorDia { get; set; }
        public string Estado { get; set; }
    }
}