using System;
using System.Web.UI;

namespace RentaVideojuegos
{
    public partial class SiteMaster : MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["Rol"] != null)
            {
                if (Session["NombreCompleto"] != null)
                {
                    litNombreUsuario.Text = Session["NombreCompleto"].ToString();
                }
                string rol = Session["Rol"].ToString();

                if (rol == "Admin")
                {
                    // vista admin
                    menuVideojuegos.Visible = true;
                    menuSucursales.Visible = true;
                    menuBitacora.Visible = true;
                    menuCatalogo.Visible = true;
                    menuAlquileres.Visible = false;
                }
                else if (rol == "Jugador")
                {
                    // vista jugador
                    menuCatalogo.Visible = true;
                    menuAlquileres.Visible = true;
                    menuVideojuegos.Visible = false;
                    menuSucursales.Visible = false;
                    menuBitacora.Visible = false;
                }
            }
            else
            {
                // Si alguien intenta entrar a las páginas sin iniciar sesión, 

                menuVideojuegos.Visible = false;
                menuSucursales.Visible = false;
                menuBitacora.Visible = false;
                menuAlquileres.Visible = false;
            }
           }
        }

        protected void btnCerrarSesion_Click(object sender, EventArgs e)
        {
            Session.Clear();
            Session.Abandon();
            Response.Redirect("~/pages/IniciarSesion.aspx");
        }
    }
}