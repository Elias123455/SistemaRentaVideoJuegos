using System;
using System.Linq;
using System.Web.UI;
using DataModels;
using LinqToDB;

namespace RentaVideojuegos.Pages
{
    public partial class ModificarAlquiler : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["IdJugador"] == null)
            {
                Response.Redirect("~/Pages/IniciarSesion.aspx");
                return;
            }

            if (!IsPostBack)
            {
                if (Request.QueryString["id"] != null && int.TryParse(Request.QueryString["id"], out int idAlquiler))
                {
                    CargarDatosAlquiler(idAlquiler);
                }
                else
                {
                    RedirigirSegunRol();
                }
            }
        }

        private void CargarDatosAlquiler(int idAlquiler)
        {
            using (var db = new RentaVideojuegosDB("RentaBD"))
            {
                var alquiler = db.GetTable<Alquiler>().FirstOrDefault(a => a.IdAlquiler == idAlquiler);

                if (alquiler == null)
                {
                    RedirigirSegunRol();
                    return;
                }

                string rol = Session["Rol"].ToString();
                DateTime fechaActual = DateTime.Now.Date;
                DateTime fechaInicioBD = alquiler.FechaInicio.Date;
                DateTime fechaDevolucionBD = alquiler.FechaDevolucion.Date;

                // Alerta 1: El alquiler ya fue cancelado en el pasado
                if (alquiler.Estado == 'I')
                {
                    MostrarAlertaYRedirigir("Este alquiler se encuentra inactivo o cancelado y no se puede modificar.");
                    return;
                }

                // Alerta 2: El tiempo del alquiler ya expiró
                if (fechaDevolucionBD <= fechaActual)
                {
                    MostrarAlertaYRedirigir("No puedes modificar este alquiler porque la fecha de devolución ya venció.");
                    return;
                }

                // Alerta 3: si el alquiler ya está en curso
                if (fechaInicioBD <= fechaActual && fechaDevolucionBD > fechaActual && rol == "Jugador")
                {
                    MostrarAlertaYRedirigir("Regla de sistema: Como jugador, no puedes modificar un alquiler que ya está en curso hoy.");
                    return;
                }

                
                txtFechaInicio.Text = alquiler.FechaInicio.ToString("yyyy-MM-dd");
                txtFechaDevolucion.Text = alquiler.FechaDevolucion.ToString("yyyy-MM-dd");

                
                if (fechaInicioBD <= fechaActual)
                {
                    txtFechaInicio.Enabled = false;
                }

                ViewState["CostoPorDia"] = alquiler.CostoPorDia;
            }
        }

       
        private void MostrarAlertaYRedirigir(string mensaje)
        {
            string rolUsuario = Session["Rol"] != null ? Session["Rol"].ToString() : "";
            string paginaDestino = rolUsuario == "Admin" ? "GestionAlquileres.aspx" : "Alquileres.aspx";

            ScriptManager.RegisterStartupScript(this, this.GetType(), "alertaRegla",
                $"alert('{mensaje}'); window.location='{paginaDestino}';", true);
        }

        private void RedirigirSegunRol()
        {
            string rol = Session["Rol"] != null ? Session["Rol"].ToString() : "";
            if (rol == "Admin")
            {
                Response.Redirect("~/Pages/GestionAlquileres.aspx");
            }
            else
            {
                Response.Redirect("~/Pages/Alquileres.aspx");
            }
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                DateTime fechaActual = DateTime.Now.Date;
                DateTime nuevaFechaInicio = Convert.ToDateTime(txtFechaInicio.Text).Date;
                DateTime nuevaFechaDevolucion = Convert.ToDateTime(txtFechaDevolucion.Text).Date;

                if (txtFechaInicio.Enabled && nuevaFechaInicio <= fechaActual)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "errIni", "alert('La fecha de inicio debe ser mayor a la actual.');", true);
                    return;
                }

                if (nuevaFechaDevolucion <= fechaActual)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "errDev", "alert('La fecha de devolución debe ser mayor a la actual.');", true);
                    return;
                }

                if (nuevaFechaDevolucion <= nuevaFechaInicio)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "errRango", "alert('La fecha de devolución debe ser posterior a la fecha de inicio.');", true);
                    return;
                }

                int totalDiasAlquiler = (nuevaFechaDevolucion - nuevaFechaInicio).Days;
                decimal costoPorDia = Convert.ToDecimal(ViewState["CostoPorDia"]);
                decimal costoTotal = totalDiasAlquiler * costoPorDia;
                int idAlquiler = Convert.ToInt32(Request.QueryString["id"]);
                int idJugadorActual = Convert.ToInt32(Session["IdJugador"]);

                using (var db = new RentaVideojuegosDB("RentaBD"))
                {
                    using (var transaction = db.BeginTransaction())
                    {
                        try
                        {
                            db.GetTable<Alquiler>()
                              .Where(a => a.IdAlquiler == idAlquiler)
                              .Set(a => a.FechaInicio, nuevaFechaInicio)
                              .Set(a => a.FechaDevolucion, nuevaFechaDevolucion)
                              .Set(a => a.TotalDiasAlquiler, totalDiasAlquiler)
                              .Set(a => a.CostoTotal, costoTotal)
                              .Update();

                            db.GetTable<Bitacora>()
                              .Insert(() => new Bitacora
                              {
                                  IdAlquiler = idAlquiler,
                                  IdJugador = idJugadorActual,
                                  AccionRealizada = "CORREGIDO",
                                  FechaDeLaAccion = DateTime.Now
                              });

                            transaction.Commit();

                            string rolUsuario = Session["Rol"] != null ? Session["Rol"].ToString() : "";
                            string paginaDestino = rolUsuario == "Admin" ? "GestionAlquileres.aspx" : "Alquileres.aspx";

                            ScriptManager.RegisterStartupScript(this, this.GetType(), "ok", $"alert('Modificado con éxito'); window.location='{paginaDestino}';", true);
                        }
                        catch (Exception)
                        {
                            transaction.Rollback();
                            throw;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "errSave", $"alert('Error: {ex.Message}');", true);
            }
        }
    }
}