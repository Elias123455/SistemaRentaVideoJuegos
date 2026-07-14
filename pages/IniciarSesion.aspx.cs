using DataModels;
using System;
using System.Linq;
using System.Web.UI;

namespace RentaVideojuegos.pages
{
    public partial class IniciarSesion : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected void btnIngresar_Click(object sender, EventArgs e)
        {
            try
            {
                string correo = txtEmail.Text.Trim();
                string contrasena = txtClave.Text.Trim();

                using (RentaVideojuegosDB db = new RentaVideojuegosDB("RentaBD"))
                {
                    // Usamos 'A' (char) y unificamos la lógica en una sola consulta
                    var jugador = db.Jugadors.FirstOrDefault(j => j.Email == correo && j.Clave == contrasena && j.Estado == 'A');

                    if (jugador != null)
                    {
                        Session["IdJugador"] = jugador.IdJugador;
                        Session["NombreCompleto"] = jugador.NombreCompleto;
                        Session["EsAdministrador"] = jugador.EsAdministrador;
                        Session["Rol"] = jugador.EsAdministrador ? "Admin" : "Jugador";

                        Response.Redirect("~/Pages/CatalogoJuegos.aspx");
                    }
                    else
                    {
                        lblError.Text = "Correo o contraseña incorrectos, o cuenta inactiva.";

                        // IMPORTANTE: Para que la pantalla de carga se quite si el login falla,
                        // debemos ocultarla manualmente mediante JavaScript:
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "ocultarCarga",
                            "document.getElementById('pantallaCarga').style.display = 'none';", true);
                    }
                }
            }
            catch (Exception ex)
            {
                lblError.Text = "Ocurrió un error: " + ex.Message;

                // Ocultar carga en caso de error
                ScriptManager.RegisterStartupScript(this, this.GetType(), "ocultarCarga",
                    "document.getElementById('pantallaCarga').style.display = 'none';", true);
            }
        }

    }
}