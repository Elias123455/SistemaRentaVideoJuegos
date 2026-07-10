using DataModels;
using LinqToDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI; 
using System.Web.UI.WebControls;

namespace RentaVideojuegos.Pages
{
    public partial class CatalogoJuegos : System.Web.UI.Page
    {
        List<int> juegosRentados = new List<int>();

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
                    
                    int idJugadorSimulado = 1;
                    juegosRentados = db.Alquilers
                        .Where(a => a.IdJugador == idJugadorSimulado && a.Estado == 'A')
                        .Select(a => (int)a.IdVideojuego)
                        .ToList();

                   
                    rptCatalogo.DataSource = db.SpListarVideojuegos().ToList();
                    rptCatalogo.DataBind();
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine("Error al cargar el catálogo: " + ex.Message);
                }
            }
        }

       
        protected void rptCatalogo_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                Button btnAlquilar = (Button)e.Item.FindControl("btnAlquilar");

                if (btnAlquilar != null && int.TryParse(btnAlquilar.CommandArgument, out int idJuego))
                {
                    
                    if (juegosRentados.Contains(idJuego))
                    {
                        btnAlquilar.Text = "RENTADO";
                        btnAlquilar.Enabled = false;
                        btnAlquilar.BackColor = System.Drawing.Color.Gray;
                        btnAlquilar.Style.Add("cursor", "not-allowed");
                    }
                }
            }
        }

        protected void btnAlquilar_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;

            if (!int.TryParse(btn.CommandArgument, out int idVideojuegoSeleccionado)) return;

            int idJugadorSimulado = 1;

            using (var db = new RentaVideojuegosDB("RentaBD"))
            {
                // INICIO DE LA LÓGICA TRANSACCIONAL (INTACTA)
                using (var transaction = db.BeginTransaction())
                {
                    try
                    {
                        // PASO 1: Insertar el registro en Alquiler
                        int idAlquilerGenerado = Convert.ToInt32(db.GetTable<Alquiler>()
                            .InsertWithIdentity(() => new Alquiler
                            {
                                IdVideojuego = idVideojuegoSeleccionado,
                                IdJugador = idJugadorSimulado,
                                FechaInicio = DateTime.Now,
                                FechaDevolucion = DateTime.Now.AddDays(7),
                                TotalDiasAlquiler = 7,
                                CostoPorDia = 2500m,
                                CostoTotal = 17500m,
                                FechaCreacion = DateTime.Now,
                                Estado = 'A'
                            }));

                        // PASO 2: (Espacio reservado) 

                        // PASO 3: Registrar la operación de forma segura en la Bitácora
                        db.GetTable<DataModels.Bitacora>()
                          .Insert(() => new DataModels.Bitacora
                          {
                              IdAlquiler = idAlquilerGenerado,
                              IdJugador = idJugadorSimulado,
                              AccionRealizada = "Renta de Videojuego",
                              FechaDeLaAccion = DateTime.Now
                          });

                        transaction.Commit();

                        System.Diagnostics.Debug.WriteLine("Transacción completada. Alquiler ID: " + idAlquilerGenerado);

                        
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "alertSuccess", "alert('¡Juego rentado con éxito! Tienes 7 días para disfrutarlo.');", true);

                        CargarCatalogo();
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        System.Diagnostics.Debug.WriteLine("Error en la transacción (Rollback): " + ex.Message);

                        
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "alertError", $"alert('Error al rentar: {ex.Message}');", true);
                    }
                }
            }
        }
    }
}