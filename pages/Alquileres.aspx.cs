using System;
using System.Linq;
using DataModels;

namespace RentaVideojuegos.Pages
{
    public partial class Alquileres : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargarMisAlquileres();
            }
        }

        private void CargarMisAlquileres()
        {
            
            using (var db = new RentaVideojuegosDB("MyDatabase"))
            {
                try
                {
                    
                    gvAlquileres.DataSource = db.Alquilers.ToList();
                    gvAlquileres.DataBind();
                }
                catch (Exception ex)
                {
                    
                    System.Diagnostics.Debug.WriteLine("Error cargando alquileres: " + ex.Message);
                }
            }
        }
    }
}