using CargaCatalogosCanales.Entidades;
using MySqlConnector;

namespace CargaCatalogosCanales.Datos;

/**
 * clase para manejar la logica de acceso a datos de los catalogos
 * esta clase se encarga de manejar la logica de acceso a datos de los catalogos, como insertar, consultar y eliminar
 * 
 */

internal sealed class CatalogoDAL
{
    public int ReemplazarCatalogo(Catalogo catalogo, IReadOnlyCollection<RegistroCatalogo> registros, DateTime fechaProceso)
    {
        using MySqlConnection conexion = DatabaseConnection.CreateConnection("catalogos");
        conexion.Open();

        // MySQL confirma TRUNCATE implícitamente; las validaciones ocurren antes de llegar a este punto.
        using (MySqlCommand truncate = new($"TRUNCATE TABLE `{catalogo.TablaDestino}`", conexion))
        {
            truncate.ExecuteNonQuery();
        }

        using MySqlTransaction transaccion = conexion.BeginTransaction();
        using MySqlCommand insert = new($"INSERT INTO `{catalogo.TablaDestino}` (codigo, nombre, departamento, municipio, direccion, fecha_proceso) VALUES (@codigo, @nombre, @departamento, @municipio, @direccion, @fechaProceso)", conexion, transaccion);
        insert.Parameters.Add("@codigo", MySqlDbType.VarChar);
        insert.Parameters.Add("@nombre", MySqlDbType.VarChar);
        insert.Parameters.Add("@departamento", MySqlDbType.VarChar);
        insert.Parameters.Add("@municipio", MySqlDbType.VarChar);
        insert.Parameters.Add("@direccion", MySqlDbType.VarChar);
        insert.Parameters.Add("@fechaProceso", MySqlDbType.DateTime).Value = fechaProceso;

        foreach (RegistroCatalogo registro in registros)
        {
            insert.Parameters["@codigo"].Value = registro.Codigo;
            insert.Parameters["@nombre"].Value = ValorNulo(registro.Nombre);
            insert.Parameters["@departamento"].Value = ValorNulo(registro.Departamento);
            insert.Parameters["@municipio"].Value = ValorNulo(registro.Municipio);
            insert.Parameters["@direccion"].Value = ValorNulo(registro.Direccion);
            insert.ExecuteNonQuery();
        }

        transaccion.Commit();
        return registros.Count;
    }

    public List<RegistroCatalogo> Consultar(Catalogo catalogo)
    {
        using MySqlConnection conexion = DatabaseConnection.CreateConnection("catalogos");
        conexion.Open();
        using MySqlCommand comando = new($"SELECT codigo, nombre, departamento, municipio, direccion FROM `{catalogo.TablaDestino}` ORDER BY id", conexion);
        using MySqlDataReader lector = comando.ExecuteReader();
        List<RegistroCatalogo> registros = [];

        while (lector.Read())
        {
            registros.Add(new RegistroCatalogo(lector.GetString(0), Texto(lector, 1), Texto(lector, 2), Texto(lector, 3), Texto(lector, 4)));
        }

        return registros;
    }

    private static object ValorNulo(string valor) => string.IsNullOrWhiteSpace(valor) ? DBNull.Value : valor;

    private static string Texto(MySqlDataReader lector, int indice) => lector.IsDBNull(indice) ? string.Empty : lector.GetString(indice);
}
