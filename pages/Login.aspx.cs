using DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace RentaVideojuegos.pages
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnIngresar_Click(object sender, EventArgs e)
        {
            try
            {
                string email = txtEmail.Text.Trim();
                string clave = txtClave.Text.Trim();

                using (RentaVideojuegosDB db = new RentaVideojuegosDB("MyDatabase"))
                {
                    var resultado = db.SpValidarJugador(email, clave).FirstOrDefault();

                    if (resultado != null)
                    {
                        // Guardar datos del jugador en Session
                        Session["IdJugador"] = resultado.IdJugador;
                        Session["NombreCompleto"] = resultado.NombreCompleto;
                        Session["EsAdministrador"] = resultado.EsAdministrador;

                        Response.Redirect("~/Pages/Alquileres.aspx");
                    }
                    else
                    {
                        lblError.Text = "Correo o contraseña incorrectos.";
                    }
                }
            }
            catch (Exception ex)
            {
                lblError.Text = "Ocurrió un error: " + ex.Message;
            }
        }
    }
}