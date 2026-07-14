using DataModels;
using LinqToDB;
using System;
using System.Linq;

namespace RentaVideojuegos.Pages
{
    public partial class Alquileres : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargarMisAlquileres();
            }
        }

        private void CargarMisAlquileres()
        {
            using (var db = new RentaVideojuegosDB("RentaBD"))
            {
                try
                {
                    int idJugadorActual = Convert.ToInt32(Session["IdJugador"]);
                    string rol = Session["Rol"] != null ? Session["Rol"].ToString() : "";

                    var consulta = from a in db.GetTable<Alquiler>()
                                   join v in db.GetTable<Videojuego>() on a.IdVideojuego equals v.IdVideojuego
                                   select new
                                   {
                                       idAlquiler = a.IdAlquiler,
                                 
                                       idVideojuego = v.Titulo,
                                       fechaInicio = a.FechaInicio,
                                       fechaDevolucion = a.FechaDevolucion,
                                       costoTotal = a.CostoTotal,
                                       estado = a.Estado,
                                       IdJugadorReal = a.IdJugador 
                                   };

                    if (rol == "Jugador")
                    {
                        consulta = consulta.Where(c => c.IdJugadorReal == idJugadorActual);
                    }

                    gvAlquileres.DataSource = consulta.ToList();
                    gvAlquileres.DataBind();
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine("Error cargando alquileres: " + ex.Message);
                }
            }
        }
    }
}