using DataModels;
using LinqToDB;
using System;
using System.Web.UI;

namespace RentaVideojuegos.pages
{
    public partial class Registrarse : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected void btnRegistrar_Click(object sender, EventArgs e)
        {
            try
            {
                
                string nombre = txtNomCompleto.Text.Trim();
                string correo = txtEmail.Text.Trim();
                string contrasena = txtClave.Text.Trim();
                

                using (RentaVideojuegosDB db = new RentaVideojuegosDB("RentaBD"))
                {
                 
                    db.GetTable<Jugador>().Insert(() => new Jugador
                    {
                        NombreCompleto = nombre,
                        Email = correo,
                        Clave = contrasena,
                        EsAdministrador = false,
                        Estado = 'A'
                    });
                }

          
                Response.Redirect("~/Pages/IniciarSesion.aspx");
            }
            catch (Exception ex)
            {
                lblError.Text = "Error al registrar: " + ex.Message;
            }
        }
    }
}