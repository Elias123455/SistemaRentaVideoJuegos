using System;
using System.Linq;
using System.Web.UI.WebControls;
using DataModels;
using LinqToDB;
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
            using (var db = new RentaVideojuegosDB("MyDatabase"))
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

            
            if (!int.TryParse(btn.CommandArgument, out int idVideojuegoSeleccionado)) return;

            
            int idJugadorSimulado = 1;

            using (var db = new RentaVideojuegosDB("RentaBD"))
            {
                // INICIO DE LA LÓGICA TRANSACCIONAL
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

                  
                        CargarCatalogo();
                    }
                    catch (Exception ex)
                    {
                     
                        transaction.Rollback();
                        System.Diagnostics.Debug.WriteLine("Error en la transacción (Rollback): " + ex.Message);
                    }
                }
            }
        }
    }
}