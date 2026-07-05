using System;
using System.IO;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls; 
using DataModels;
using LinqToDB;
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
                CargarSucursales(); 
                CargarCategorias(); 
                CargarVideojuegos();
            }
        }

        //  Método para buscar las sucursales en SQL y ponerlas en el menú desplegable
        private void CargarSucursales()
        {
            using (var db = new RentaVideojuegosDB("RentaBD"))
            {
                ddlSucursal.DataSource = db.Sucursals.ToList();
                ddlSucursal.DataTextField = "Nombre";
                ddlSucursal.DataValueField = "IdSucursal";
                ddlSucursal.DataBind();

                // Agrega una opción por defecto al inicio
                ddlSucursal.Items.Insert(0, new ListItem("-- Seleccione una Sucursal --", "0"));
            }
        }

        // Método para cargar categorías
        private void CargarCategorias()
        {
            ddlCategoria.Items.Add(new ListItem("-- Seleccione una Categoría --", "0"));
            ddlCategoria.Items.Add(new ListItem("Acción y Aventura", "CAT-01"));
            ddlCategoria.Items.Add(new ListItem("Deportes", "CAT-02"));
            ddlCategoria.Items.Add(new ListItem("RPG", "CAT-03"));
        }

        // RNF-003: Método para consultar la base de datos y llenar el GridView con la lista de videojuegos.
        private void CargarVideojuegos()
        {
            using (var db = new RentaVideojuegosDB("RentaBD"))
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
            using (var db = new RentaVideojuegosDB("RentaBD"))
            {

                int idSucursalSeleccionada = Convert.ToInt32(ddlSucursal.SelectedValue);
                string idCategoriaSeleccionada = ddlCategoria.SelectedValue;

                db.SpInsertarVideojuego(
                    idSucursalSeleccionada,
                    txtTitulo.Text,
                    txtDescripcion.Text,
                    idCategoriaSeleccionada,
                    Convert.ToDateTime(txtFechaLanzamiento.Text),
                    txtDesarrolladora.Text,
                    txtDistribuidora.Text,
                    nombreArchivoImagen,
                    txtTrailer.Text
                );
            }

            // 3. Limpiar las cajas de texto, listas desplegables y recargar la tabla
            txtTitulo.Text = string.Empty;
            txtDescripcion.Text = string.Empty;
            ddlSucursal.SelectedIndex = 0; 
            ddlCategoria.SelectedIndex = 0;

            CargarVideojuegos();
        }

        
        protected void gvVideojuegos_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            
            if (e.CommandArgument != null && int.TryParse(e.CommandArgument.ToString(), out int idJuegoSeleccionado))
            {
                if (e.CommandName == "EliminarJuego")
                {
                    using (var db = new RentaVideojuegosDB("RentaBD"))
                    {
                        try
                        {
                           
                            db.GetTable<Videojuego>()
                              .Where(v => v.IdVideojuego == idJuegoSeleccionado)
                              .Delete();

                         
                            CargarVideojuegos();
                        }
                        catch (Exception ex)
                        {
                            System.Diagnostics.Debug.WriteLine("Error al eliminar: " + ex.Message);
                           
                        }
                    }
                }
                else if (e.CommandName == "EditarJuego")
                {
                    // Lógica para Editar (Fase inicial)
                  
                    using (var db = new RentaVideojuegosDB("RentaBD"))
                    {
                        var juego = db.GetTable<Videojuego>().FirstOrDefault(v => v.IdVideojuego == idJuegoSeleccionado);
                        if (juego != null)
                        {
                            txtTitulo.Text = juego.Titulo;
                            txtDescripcion.Text = juego.Descripcion;
                            txtDesarrolladora.Text = juego.Desarrolladora;
                            txtDistribuidora.Text = juego.Distribuidora;

                           
                            System.Diagnostics.Debug.WriteLine("Datos cargados para el juego: " + juego.Titulo);
                        }
                    }
                }
            }
        }
    }
}