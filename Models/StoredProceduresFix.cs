using System;
using LinqToDB;
using LinqToDB.Data;

namespace DataModels
{
    public static class StoredProceduresFix
    {
        // Esta versión corregida usa inicializadores de objeto, evitando el error de conversión
        public static int SpInsertarVideojuegoCorregido(this RentaVideojuegosDB db, int idSucursal, string titulo, string descripcion, string idCategoria, DateTime fecha, string dev, string dist, string img, string trail)
        {
            return db.ExecuteProc("[dbo].[sp_InsertarVideojuego]",
                new DataParameter("@idSucursal", idSucursal) { DataType = DataType.Int32 },
                new DataParameter("@titulo", titulo) { DataType = DataType.VarChar, Size = 100 },
                new DataParameter("@descripcion", descripcion) { DataType = DataType.Text },
                new DataParameter("@idCategoria", idCategoria) { DataType = DataType.VarChar, Size = 100 },
                new DataParameter("@fechaLanzamiento", fecha) { DataType = DataType.Date },
                new DataParameter("@desarrolladora", dev) { DataType = DataType.VarChar, Size = 100 },
                new DataParameter("@distribuidora", dist) { DataType = DataType.VarChar, Size = 100 },
                new DataParameter("@imagen", img) { DataType = DataType.VarChar, Size = 255 },
                new DataParameter("@trailer", trail) { DataType = DataType.VarChar, Size = 255 }
            );
        }
    }
}