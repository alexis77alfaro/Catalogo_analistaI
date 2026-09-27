namespace CargaCatalogosCanales;


/*@Edagueava
 * clase para mostrar el dialogo de configuracion de la base de datos
 * esta se realizo para que el usuario pueda configurar la conexion a la base de datos, y probarla antes de conectarse
 * ademase de ser una buena practica de desarrollo, ya que permite al usuario verificar que la conexion es correcta antes de conectarse a la base de datos y dinamica
 * */
internal sealed partial class DatabaseSettingsDialog : Form
{
    public DatabaseSettingsDialog(DatabaseOptions initialOptions)
    {
        InitializeComponent();
        _serverTextBox.Text = initialOptions.Server;
        _portInput.Value = initialOptions.Port;
        _userTextBox.Text = initialOptions.UserName;
        _passwordTextBox.Text = initialOptions.Password;
        _databaseComboBox.Text = initialOptions.Database;
    }

    private async void ProbarConexionButton_Click(object? sender, EventArgs e)
    {
        await IntentarConexionAsync(false);
    }

    private async void ConectarButton_Click(object? sender, EventArgs e)
    {
        await IntentarConexionAsync(true);
    }

    private void MostrarContrasenaCheckBox_CheckedChanged(object? sender, EventArgs e)
    {
        _passwordTextBox.UseSystemPasswordChar = !_mostrarContrasenaCheckBox.Checked;
    }

    private async Task IntentarConexionAsync(bool cerrarCuandoConecte)
    {
        DatabaseOptions options = new()
        {
            Server = _serverTextBox.Text.Trim(),
            Port = decimal.ToUInt32(_portInput.Value),
            UserName = _userTextBox.Text.Trim(),
            Password = _passwordTextBox.Text,
            Database = _databaseComboBox.Text.Trim()
        };

        if (string.IsNullOrWhiteSpace(options.Server) ||
            string.IsNullOrWhiteSpace(options.UserName) ||
            string.IsNullOrWhiteSpace(options.Database))
        {
            MostrarEstado("Servidor, usuario y base de datos son obligatorios.", false);
            return;
        }

        _probarConexionButton.Enabled = false;
        _conectarButton.Enabled = false;
        MostrarEstado("Probando conexión con MySQL...", null);

        (bool connected, string error) result = await Task.Run(() =>
        {
            bool connected = DatabaseConnection.TryConfigure(options, out string error);
            return (connected, error);
        });

        _probarConexionButton.Enabled = true;
        _conectarButton.Enabled = true;

        if (!result.connected)
        {
            MostrarEstado("No se pudo conectar. Verifique servidor, usuario, contraseña y esquema.", false);
            return;
        }

        MostrarEstado("Conexión correcta con MySQL.", true);
        if (cerrarCuandoConecte)
        {
            DialogResult = DialogResult.OK;
            Close();
        }
    }

    private void MostrarEstado(string mensaje, bool? correcto)
    {
        _estadoConexionLabel.Text = mensaje;
        _estadoConexionLabel.ForeColor = correcto switch
        {
            true => Color.ForestGreen,
            false => Color.Firebrick,
            _ => Color.DimGray
        };
    }
}
