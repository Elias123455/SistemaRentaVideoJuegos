using System;
using System.Linq;
using System.Web.UI.WebControls;
using DataModels;

namespace RentaVideojuegos.Pages
{
    public partial class CatalogoJuegos : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargarCatalogo();
            }
        }

        private void CargarCatalogo()
        {
            using (var db = new RentaVideojuegosDB("RentaBD"))
            {
                try
                {
                    rptCatalogo.DataSource = db.SpListarVideojuegos().ToList();
                    rptCatalogo.DataBind();
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine("Error al cargar el catálogo: " + ex.Message);
                }
            }
        }

        protected void btnAlquilar_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            string idVideojuego = btn.CommandArgument;
            // La lógica de la renta la haremos en el siguiente bloque
            System.Diagnostics.Debug.WriteLine("Clic en rentar juego: " + idVideojuego);
        }
    }
}