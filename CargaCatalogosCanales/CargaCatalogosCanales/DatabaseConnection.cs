using MySqlConnector;

namespace CargaCatalogosCanales;


/*
 *  conexion de base de datos 
 *  aca se contempla la configuracion de la conexion a la base de datos, asi como la creacion de la misma
 *  
 *  
 */

//se utilizo copilot para generar el codigo de conexion a la base de datos, ya que es un codigo muy comun y repetitivo, y no se requiere de mucha logica de negocio, solo se requiere de la configuracion de la conexion a la base de datos
internal sealed class DatabaseOptions
{
    public string Server { get; init; } = "localhost";
    public uint Port { get; init; } = 3306;
    public string UserName { get; init; } = "root";
    public string Password { get; init; } = string.Empty;
    public string Database { get; init; } = "catalogos";

    public static DatabaseOptions Default => new();

    public string BuildConnectionString()
    {
        return new MySqlConnectionStringBuilder
        {
            Server = Server,
            Port = Port,
            UserID = UserName,
            Password = Password,
            Database = Database,
            SslMode = MySqlSslMode.None,
            ConnectionTimeout = 5
        }.ConnectionString;
    }
}

internal static class DatabaseConnection
{
    private static string? _connectionString;

    public static bool IsConfigured => !string.IsNullOrWhiteSpace(_connectionString);

    public static bool TryConfigure(DatabaseOptions options, out string error)
    {
        try
        {
            string connectionString = options.BuildConnectionString();

            using MySqlConnection connection = new(connectionString);
            connection.Open();

            using MySqlCommand command = new("SELECT 1", connection);
            command.ExecuteScalar();

            _connectionString = connectionString;
            error = string.Empty;
            return true;
        }
        catch (MySqlException exception)
        {
            error = exception.Message;
            return false;
        }
        catch (Exception exception)
        {
            error = exception.Message;
            return false;
        }
    }

    public static MySqlConnection CreateConnection()
    {
        return CreateConnection(null);
    }

    public static MySqlConnection CreateConnection(string? database)
    {
        if (string.IsNullOrWhiteSpace(_connectionString))
        {
            throw new InvalidOperationException("La conexión a la base de datos no está configurada.");
        }

        if (string.IsNullOrWhiteSpace(database))
        {
            return new MySqlConnection(_connectionString);
        }

        MySqlConnectionStringBuilder builder = new(_connectionString)
        {
            Database = database
        };

        return new MySqlConnection(builder.ConnectionString);
    }
}

internal static class DatabaseStartup
{
    public static bool EnsureConnection()
    {
        DatabaseOptions options = DatabaseOptions.Default;

        if (DatabaseConnection.TryConfigure(options, out _))
        {
            return true;
        }

        using DatabaseSettingsDialog dialog = new(options);
        return dialog.ShowDialog() == DialogResult.OK;
    }
}
