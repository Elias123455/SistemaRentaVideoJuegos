using System;
using System.Linq;
using DataModels;
using LinqToDB;

namespace RentaVideojuegos.Pages
{
    
    public partial class BitacoraPage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargarBitacora();
            }
        }

        private void CargarBitacora()
        {
            using (var db = new RentaVideojuegosDB("RentaBD"))
            {
                try
                {
                    var consulta = from b in db.GetTable<DataModels.Bitacora>()
                                       
                                   join j in db.GetTable<Jugador>() on b.IdJugador equals j.IdJugador

                                   
                                   join a in db.GetTable<Alquiler>() on b.IdAlquiler equals a.IdAlquiler into aj
                                   from a in aj.DefaultIfEmpty()

                                       
                                   join v in db.GetTable<Videojuego>() on (a != null ? a.IdVideojuego : 0) equals v.IdVideojuego into vj
                                   from v in vj.DefaultIfEmpty()

                                   orderby b.FechaDeLaAccion descending
                                   select new
                                   {
                                       IdBitacora = b.IdBitacora,

                                      
                                       IdJugador = j.NombreCompleto,

                                       AccionRealizada = b.AccionRealizada,
                                       FechaDeLaAccion = b.FechaDeLaAccion,
                                       IdAlquiler = v != null ? v.Titulo : "N/A"
                                   };

                    gvBitacora.DataSource = consulta.ToList();
                    gvBitacora.DataBind();
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine("Error al cargar la bitácora: " + ex.Message);
                }
            }
        }
    }
}