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
                    
                    var resultado = db.Jugadors.FirstOrDefault(j => j.Email == correo && j.Clave == contrasena && j.Estado == 'A');

                    if (resultado != null)
                    {
                        Session["IdJugador"] = resultado.IdJugador;
                        Session["NombreCompleto"] = resultado.NombreCompleto;
                        Session["EsAdministrador"] = resultado.EsAdministrador;
                        Session["Rol"] = resultado.EsAdministrador ? "Admin" : "Jugador";

                        Response.Redirect("~/Pages/CatalogoJuegos.aspx");
                    }
                    else
                    {
                        
                        lblError.Text = "Correo o contraseña incorrectos, o cuenta inactiva.";
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