using System;
using System.IO;
using System.Linq;
using System.Web.UI;
using DataModels; 

namespace RentaVideojuegos
{
    // RNF-003: Clase encargada de la lógica de negocio y presentación para la gestión del catálogo de videojuegos.
    public partial class GestionVideojuegos : System.Web.UI.Page
    {
        // RNF-003: Método que se ejecuta al cargar la página. Inicializa el catálogo.
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargarVideojuegos();
            }
        }

        // RNF-003: Método para consultar la base de datos y llenar el GridView con la lista de videojuegos.
        private void CargarVideojuegos()
        {
            using (var db = new RentaVideojuegosDB())
            {
                
                gvVideojuegos.DataSource = db.SpListarVideojuegos().ToList();
                gvVideojuegos.DataBind();
            }
        }

        // RNF-003: Método que captura el evento del botón para guardar un nuevo videojuego y su imagen.
        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            string nombreArchivoImagen = "default.png";

            // 1. Lógica para guardar la foto físicamente en la carpeta del servidor
            if (fuImagen.HasFile)
            {
                nombreArchivoImagen = fuImagen.FileName;
                string rutaFisica = Server.MapPath("~/images/portadas/" + nombreArchivoImagen);
                fuImagen.SaveAs(rutaFisica);
            }

            // 2. Lógica para enviar los datos a SQL Server
            using (var db = new RentaVideojuegosDB())
            {
               
                db.SpInsertarVideojuego(
                    1,
                    txtTitulo.Text,
                    txtDescripcion.Text,
                    "CAT-01",
                    Convert.ToDateTime(txtFechaLanzamiento.Text),
                    txtDesarrolladora.Text,
                    txtDistribuidora.Text,
                    nombreArchivoImagen,
                    txtTrailer.Text
                );
            }

            // 3. Limpiar las cajas de texto y recargar la tabla
            txtTitulo.Text = string.Empty;
            txtDescripcion.Text = string.Empty;
            CargarVideojuegos();
        }
    }
}