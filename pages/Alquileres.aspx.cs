using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Web.UI;

namespace RentaVideojuegos.Pages
{
    public partial class Alquileres : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected void btnProbarConexion_Click(object sender, EventArgs e)
        {
            var csEntry = ConfigurationManager.ConnectionStrings["RentaBD"];

            if (csEntry == null)
            {
                // Esta línea nos dirá la ruta exacta del archivo que el servidor está usando
                string ruta = AppDomain.CurrentDomain.SetupInformation.ConfigurationFile;
                lblResultado.Text = "No encuentra 'RentaBD'. El servidor está leyendo este archivo: " + ruta;
                lblResultado.CssClass = "text-warning fw-bold";
                return;
            }

            using (SqlConnection conexion = new SqlConnection(csEntry.ConnectionString))
            {
                try
                {
                    conexion.Open();
                    lblResultado.Text = "¡Conexión exitosa a la Base de Datos!";
                    lblResultado.CssClass = "text-success fw-bold";
                }
                catch (Exception ex)
                {
                    lblResultado.Text = "Error de SQL: " + ex.Message;
                    lblResultado.CssClass = "text-danger fw-bold";
                }
            }
        }
    }
}