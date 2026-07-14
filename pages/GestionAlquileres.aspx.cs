using System;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataModels;
using LinqToDB;

namespace RentaVideojuegos.Pages
{
    public partial class GestionAlquileres : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            
            if (Session["IdJugador"] == null || Session["Rol"] == null || Session["Rol"].ToString() != "Admin")
            {
                Response.Redirect("~/Pages/IniciarSesion.aspx");
                return;
            }

            if (!IsPostBack)
            {
                CargarAlquileres();
            }
        }

        private void CargarAlquileres()
        {
            using (var db = new RentaVideojuegosDB("RentaBD"))
            {
                try
                {
                  
                    var consulta = from a in db.GetTable<Alquiler>()
                                   join j in db.GetTable<Jugador>() on a.IdJugador equals j.IdJugador
                                   join v in db.GetTable<Videojuego>() on a.IdVideojuego equals v.IdVideojuego
                                   select new
                                   {
                                       IdAlquiler = a.IdAlquiler,
                                       Jugador = j.NombreCompleto,
                                       Videojuego = v.Titulo,
                                       FechaInicio = a.FechaInicio,
                                       FechaDevolucion = a.FechaDevolucion,
                                       Estado = a.Estado
                                   };

                   
                    if (!string.IsNullOrWhiteSpace(txtFiltroJugador.Text))
                    {
                        string filtroNombre = txtFiltroJugador.Text.Trim().ToLower();
                        consulta = consulta.Where(c => c.Jugador.ToLower().Contains(filtroNombre));
                    }

                   
                    if (!string.IsNullOrWhiteSpace(txtFiltroInicio.Text) && !string.IsNullOrWhiteSpace(txtFiltroFin.Text))
                    {
                        DateTime fechaIni = Convert.ToDateTime(txtFiltroInicio.Text).Date;
                        DateTime fechaFin = Convert.ToDateTime(txtFiltroFin.Text).Date;
                        consulta = consulta.Where(c => c.FechaInicio >= fechaIni && c.FechaDevolucion <= fechaFin);
                    }

                    gvAlquileres.DataSource = consulta.OrderByDescending(c => c.IdAlquiler).ToList();
                    gvAlquileres.DataBind();
                }
                catch (Exception ex)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "errLoad", $"alert('Error al cargar datos: {ex.Message}');", true);
                }
            }
        }

        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            CargarAlquileres();
        }

        protected void gvAlquileres_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "CancelarRenta")
            {
                int idAlquiler = Convert.ToInt32(e.CommandArgument);
                int idAdminActual = Convert.ToInt32(Session["IdJugador"]);

                using (var db = new RentaVideojuegosDB("RentaBD"))
                {
                    using (var transaction = db.BeginTransaction())
                    {
                        try
                        {
                           
                            db.GetTable<Alquiler>()
                              .Where(a => a.IdAlquiler == idAlquiler)
                              .Set(a => a.Estado, 'I')
                              .Update();

                            
                            db.GetTable<Bitacora>()
                              .Insert(() => new Bitacora
                              {
                                  IdAlquiler = idAlquiler,
                                  IdJugador = idAdminActual,
                                  AccionRealizada = "CANCELADO",
                                  FechaDeLaAccion = DateTime.Now
                              });

                            transaction.Commit();

                            ScriptManager.RegisterStartupScript(this, this.GetType(), "okCancel", "alert('Alquiler cancelado con éxito.');", true);

                            
                            CargarAlquileres();
                        }
                        catch (Exception ex)
                        {
                            transaction.Rollback();
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "errCancel", $"alert('Error al cancelar: {ex.Message}');", true);
                        }
                    }
                }
            }
        }
    }
}