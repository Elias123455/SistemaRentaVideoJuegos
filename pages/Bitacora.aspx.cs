using System;
using System.Linq;
using DataModels;
using LinqToDB;

namespace RentaVideojuegos.Pages
{
    // Cambiamos el nombre a BitacoraPage para que no choque con la tabla Bitacora
    public partial class BitacoraPage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargarBitacora();
            }
        }

        private void CargarBitacora()
        {
            using (var db = new RentaVideojuegosDB("MyDatabase"))
            {
                try
                {
                    // Usamos la propiedad REAL: FechaDeLaAccion
                    gvBitacora.DataSource = db.GetTable<DataModels.Bitacora>()
                                              .OrderByDescending(b => b.FechaDeLaAccion)
                                              .ToList();
                    gvBitacora.DataBind();
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine("Error al cargar la bitácora: " + ex.Message);
                }
            }
        }
    }
}