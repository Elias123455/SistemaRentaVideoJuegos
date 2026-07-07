using System;
using System.Linq;
using DataModels;
using LinqToDB;

namespace RentaVideojuegos.Pages
{
    public partial class GestionSucursales : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargarSucursales();
            }
        }

        private void CargarSucursales()
        {
            using (var db = new RentaVideojuegosDB("MyDatabase"))
            {
                try
                {
                    gvSucursales.DataSource = db.Sucursals.ToList();
                    gvSucursales.DataBind();
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine("Error al cargar sucursales: " + ex.Message);
                }
            }
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            using (var db = new RentaVideojuegosDB("MyDatabase"))
            {
                db.GetTable<Sucursal>()
                  .Insert(() => new Sucursal
                  {
                      Nombre = txtNombre.Text,
                      Direccion = txtDireccion.Text,
                      Telefono = txtTelefono.Text
                  });
            }

            txtNombre.Text = string.Empty;
            txtDireccion.Text = string.Empty;
            txtTelefono.Text = string.Empty;

            CargarSucursales();
        }
    }
}