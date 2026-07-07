using DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

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
                string email = txtEmail.Text.Trim();
                string clave = txtClave.Text.Trim();
                bool administrador = chkEsAdministrador.Checked;

                using (RentaVideojuegosDB db = new RentaVideojuegosDB("MyDatabase"))
                {
                    db.SpInsertarJugador(nombre, email, clave, administrador);
                }

                Response.Redirect("~/Pages/Login.aspx");
            }
            catch (Exception ex)
            {
                lblError.Text = "Error al registrar: " + ex.Message;
            }
        }
    }
}