using CargaCatalogosCanales.Entidades;
using MySqlConnector;

namespace CargaCatalogosCanales.Datos;

internal sealed class BitacoraDAL
{
    public List<RegistroBitacora> ConsultarRecientes()
    {
        using MySqlConnection conexion = DatabaseConnection.CreateConnection("bitacora");
        conexion.Open();
        using MySqlCommand comando = new(@"SELECT nombre_archivo, tipo_archivo, tabla_destino, fecha_inicio, fecha_fin,
            cantidad_registros, estado, mensaje
            FROM bitacora_edenilson_guevara_log_ejecucion
            ORDER BY id_ejecucion DESC LIMIT 100", conexion);
        using MySqlDataReader lector = comando.ExecuteReader();
        List<RegistroBitacora> registros = [];

        while (lector.Read())
        {
            registros.Add(new RegistroBitacora(
                lector.GetString(0), lector.GetString(1), Texto(lector, 2), lector.GetDateTime(3),
                lector.IsDBNull(4) ? null : lector.GetDateTime(4), lector.GetInt32(5), lector.GetString(6), Texto(lector, 7)));
        }

        return registros;
    }

    public long RegistrarInicio(RegistroCarga carga)
    {
        using MySqlConnection conexion = DatabaseConnection.CreateConnection("bitacora");
        conexion.Open();
        using MySqlCommand comando = new(@"INSERT INTO bitacora_edenilson_guevara_log_ejecucion
            (nombre_archivo, tipo_archivo, tabla_destino, cantidad_registros, estado, mensaje)
            VALUES (@archivo, @tipo, @tabla, 0, 'EN_PROCESO', 'Carga iniciada desde la aplicación');
            SELECT LAST_INSERT_ID();", conexion);
        comando.Parameters.AddWithValue("@archivo", carga.NombreArchivo);
        comando.Parameters.AddWithValue("@tipo", carga.TipoArchivo);
        comando.Parameters.AddWithValue("@tabla", carga.TablaDestino);
        return Convert.ToInt64(comando.ExecuteScalar());
    }

    public void RegistrarFinalizacion(long idEjecucion, RegistroCarga carga, int cantidad, bool exitoso, string mensaje)
    {
        using MySqlConnection conexion = DatabaseConnection.CreateConnection("bitacora");
        conexion.Open();
        using MySqlCommand actualizar = new(@"UPDATE bitacora_edenilson_guevara_log_ejecucion
            SET fecha_fin = NOW(), cantidad_registros = @cantidad, estado = @estado, mensaje = @mensaje
            WHERE id_ejecucion = @id", conexion);
        actualizar.Parameters.AddWithValue("@cantidad", cantidad);
        actualizar.Parameters.AddWithValue("@estado", exitoso ? "COMPLETADO" : "ERROR");
        actualizar.Parameters.AddWithValue("@mensaje", mensaje);
        actualizar.Parameters.AddWithValue("@id", idEjecucion);
        actualizar.ExecuteNonQuery();

        if (!exitoso)
        {
            return;
        }

        using MySqlCommand control = new(@"INSERT INTO bitacora_edenilson_guevara_control_cargas_diarias
            (nombre_archivo, tabla_destino, fecha_proceso, cantidad_registros, estado, mensaje)
            VALUES (@archivo, @tabla, @fecha, @cantidad, 'COMPLETADO', @mensaje)", conexion);
        control.Parameters.AddWithValue("@archivo", carga.NombreArchivo);
        control.Parameters.AddWithValue("@tabla", carga.TablaDestino);
        control.Parameters.AddWithValue("@fecha", carga.FechaProceso);
        control.Parameters.AddWithValue("@cantidad", cantidad);
        control.Parameters.AddWithValue("@mensaje", mensaje);
        control.ExecuteNonQuery();
    }

    private static string Texto(MySqlDataReader lector, int indice) => lector.IsDBNull(indice) ? string.Empty : lector.GetString(indice);
}
