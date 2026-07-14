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
            if (Session["Rol"] == null || Session["Rol"].ToString() != "Admin")
            {
                Response.Redirect("~/Pages/IniciarSesion.aspx");
                return;
            }

            if (!IsPostBack)
            {
                CargarSucursales();
                CargarCategorias();
                CargarVideojuegos();
            }
        }

        // Método para buscar las sucursales en SQL y ponerlas en el menú desplegable
        private void CargarSucursales()
        {
            using (var db = new RentaVideojuegosDB("RentaBD"))
            {
                try
                {
                    ddlSucursal.DataSource = db.Sucursals.ToList();
                    ddlSucursal.DataTextField = "Nombre";
                    ddlSucursal.DataValueField = "IdSucursal";
                    ddlSucursal.DataBind();
                }
                catch (Exception ex)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "errSuc", $"alert('Error sucursales: {ex.Message}');", true);
                }
                ddlSucursal.Items.Insert(0, new ListItem("-- Seleccione una Sucursal --", "0"));
            }
        }

        // Método para cargar categorías
        private void CargarCategorias()
        {
            ddlCategoria.Items.Clear();
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
                try
                {
                    gvVideojuegos.DataSource = db.SpListarVideojuegos().ToList();
                    gvVideojuegos.DataBind();
                }
                catch (Exception ex)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "errGrid", $"alert('Error catálogo: {ex.Message}');", true);
                }
            }
        }

        // RNF-003: Método que captura el evento del botón para guardar un nuevo videojuego y su imagen.
        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                string nombreArchivoImagen = "default.png";

                // 1. Lógica para guardar la foto físicamente en la carpeta del servidor
                if (fuImagen.HasFile)
                {
                    string carpetaDestino = Server.MapPath("~/images/portadas/");
                    if (!Directory.Exists(carpetaDestino))
                    {
                        Directory.CreateDirectory(carpetaDestino);
                    }

                    nombreArchivoImagen = Path.GetFileName(fuImagen.FileName);
                    string rutaFisica = Path.Combine(carpetaDestino, nombreArchivoImagen);
                    fuImagen.SaveAs(rutaFisica);
                }

                // 2. Lógica para enviar los datos a SQL Server
                using (var db = new RentaVideojuegosDB("RentaBD"))
                {
                    int idSucursalSeleccionada = Convert.ToInt32(ddlSucursal.SelectedValue);
                    string idCategoriaSeleccionada = ddlCategoria.SelectedValue;

                    // Validamos si existe un ID guardado (Modo Editar)
                    if (ViewState["IdJuegoEditando"] != null)
                    {
                        int idEditando = Convert.ToInt32(ViewState["IdJuegoEditando"]);

                        // Rescatar la imagen actual por si no se sube una nueva
                        var juegoActual = db.GetTable<Videojuego>().FirstOrDefault(v => v.IdVideojuego == idEditando);
                        string imagenFinal = fuImagen.HasFile ? nombreArchivoImagen : juegoActual.Imagen;

                        // Actualizar (UPDATE)
                        db.GetTable<Videojuego>()
                          .Where(v => v.IdVideojuego == idEditando)
                          .Set(v => v.IdSucursal, idSucursalSeleccionada)
                          .Set(v => v.Titulo, txtTitulo.Text)
                          .Set(v => v.Descripcion, txtDescripcion.Text)
                          .Set(v => v.IdCategoria, idCategoriaSeleccionada)
                          .Set(v => v.FechaLanzamiento, Convert.ToDateTime(txtFechaLanzamiento.Text))
                          .Set(v => v.Desarrolladora, txtDesarrolladora.Text)
                          .Set(v => v.Distribuidora, txtDistribuidora.Text)
                          .Set(v => v.Trailer, txtTrailer.Text)
                          .Set(v => v.Imagen, imagenFinal)
                          .Update();

                        // Limpiar el estado de edición al terminar
                        ViewState["IdJuegoEditando"] = null;
                    }
                    else
                    {
                        // Modo Crear (INSERT)
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
                }

                // 3. Limpiar las cajas de texto, listas desplegables y recargar la tabla
                txtTitulo.Text = string.Empty;
                txtDescripcion.Text = string.Empty;
                txtDesarrolladora.Text = string.Empty;
                txtDistribuidora.Text = string.Empty;
                txtFechaLanzamiento.Text = string.Empty;
                txtTrailer.Text = string.Empty;
                ddlSucursal.SelectedIndex = 0;
                ddlCategoria.SelectedIndex = 0;

                ScriptManager.RegisterStartupScript(this, this.GetType(), "alertSuccess", "alert('¡Operación realizada con éxito!');", true);
                CargarVideojuegos();
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alertError", $"alert('Error al guardar: {ex.Message}');", true);
            }
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

                            ScriptManager.RegisterStartupScript(this, this.GetType(), "delSuccess", "alert('Registro eliminado.');", true);
                            CargarVideojuegos();
                        }
                        catch (Exception ex)
                        {
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "delErr", $"alert('Error al eliminar: {ex.Message}');", true);
                        }
                    }
                }
                else if (e.CommandName == "EditarJuego")
                {
                    using (var db = new RentaVideojuegosDB("RentaBD"))
                    {
                        try
                        {
                            var juego = db.GetTable<Videojuego>().FirstOrDefault(v => v.IdVideojuego == idJuegoSeleccionado);
                            if (juego != null)
                            {
                                txtTitulo.Text = juego.Titulo;
                                txtDescripcion.Text = juego.Descripcion;
                                txtDesarrolladora.Text = juego.Desarrolladora;
                                txtDistribuidora.Text = juego.Distribuidora;
                                txtTrailer.Text = juego.Trailer;
                                txtFechaLanzamiento.Text = juego.FechaLanzamiento.ToString("yyyy-MM-dd");
                                ddlSucursal.SelectedValue = juego.IdSucursal.ToString();
                                ddlCategoria.SelectedValue = juego.IdCategoria;

                                // AGREGADO: Guardar el ID en el ViewState
                                ViewState["IdJuegoEditando"] = juego.IdVideojuego;
                            }
                        }
                        catch (Exception ex)
                        {
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "edtErr", $"alert('Error al cargar datos: {ex.Message}');", true);
                        }
                    }
                }
            }
        }
    }
}