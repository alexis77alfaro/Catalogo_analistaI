namespace CargaCatalogosCanales
{
    partial class DatabaseSettingsDialog
    {
        private System.ComponentModel.IContainer components = null!;
        private Label encabezadoLabel = null!;
        private GroupBox datosConexionGroupBox = null!;
        private Label servidorLabel = null!;
        private Label puertoLabel = null!;
        private Label usuarioLabel = null!;
        private Label contrasenaLabel = null!;
        private Label esquemaLabel = null!;
        private TextBox _serverTextBox = null!;
        private NumericUpDown _portInput = null!;
        private TextBox _userTextBox = null!;
        private TextBox _passwordTextBox = null!;
        private ComboBox _databaseComboBox = null!;
        private CheckBox _mostrarContrasenaCheckBox = null!;
        private Label _estadoConexionLabel = null!;
        private Button _probarConexionButton = null!;
        private Button _conectarButton = null!;
        private Button cancelarButton = null!;

        protected override void Dispose(bool disposing)
        {
            if (disposing && components != null)
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            encabezadoLabel = new Label();
            datosConexionGroupBox = new GroupBox();
            servidorLabel = new Label();
            puertoLabel = new Label();
            usuarioLabel = new Label();
            contrasenaLabel = new Label();
            esquemaLabel = new Label();
            _serverTextBox = new TextBox();
            _portInput = new NumericUpDown();
            _userTextBox = new TextBox();
            _passwordTextBox = new TextBox();
            _databaseComboBox = new ComboBox();
            _mostrarContrasenaCheckBox = new CheckBox();
            _estadoConexionLabel = new Label();
            _probarConexionButton = new Button();
            _conectarButton = new Button();
            cancelarButton = new Button();
            datosConexionGroupBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)_portInput).BeginInit();
            SuspendLayout();

            encabezadoLabel.AutoSize = true;
            encabezadoLabel.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            encabezadoLabel.Location = new Point(18, 16);
            encabezadoLabel.Text = "Configuración de conexión MySQL";

            datosConexionGroupBox.Controls.AddRange(new Control[] { servidorLabel, puertoLabel, usuarioLabel, contrasenaLabel, esquemaLabel, _serverTextBox, _portInput, _userTextBox, _passwordTextBox, _databaseComboBox, _mostrarContrasenaCheckBox });
            datosConexionGroupBox.Location = new Point(18, 48);
            datosConexionGroupBox.Size = new Size(480, 220);
            datosConexionGroupBox.Text = "Datos del servidor";

            servidorLabel.AutoSize = true;
            servidorLabel.Location = new Point(18, 32);
            servidorLabel.Text = "Servidor:";
            puertoLabel.AutoSize = true;
            puertoLabel.Location = new Point(18, 68);
            puertoLabel.Text = "Puerto:";
            usuarioLabel.AutoSize = true;
            usuarioLabel.Location = new Point(18, 104);
            usuarioLabel.Text = "Usuario:";
            contrasenaLabel.AutoSize = true;
            contrasenaLabel.Location = new Point(18, 140);
            contrasenaLabel.Text = "Contraseña:";
            esquemaLabel.AutoSize = true;
            esquemaLabel.Location = new Point(18, 176);
            esquemaLabel.Text = "Base de datos / esquema:";

            _serverTextBox.Location = new Point(178, 29);
            _serverTextBox.Size = new Size(280, 23);
            _portInput.Location = new Point(178, 65);
            _portInput.Maximum = 65535;
            _portInput.Minimum = 1;
            _portInput.Size = new Size(120, 23);
            _portInput.Value = 3306;
            _userTextBox.Location = new Point(178, 101);
            _userTextBox.Size = new Size(280, 23);
            _passwordTextBox.Location = new Point(178, 137);
            _passwordTextBox.Size = new Size(280, 23);
            _passwordTextBox.UseSystemPasswordChar = true;
            _databaseComboBox.DropDownStyle = ComboBoxStyle.DropDown;
            _databaseComboBox.Items.AddRange(new object[] { "catalogos", "bitacora" });
            _databaseComboBox.Location = new Point(178, 173);
            _databaseComboBox.Size = new Size(280, 23);
            _mostrarContrasenaCheckBox.AutoSize = true;
            _mostrarContrasenaCheckBox.Location = new Point(322, 65);
            _mostrarContrasenaCheckBox.Text = "Mostrar contraseña";
            _mostrarContrasenaCheckBox.CheckedChanged += MostrarContrasenaCheckBox_CheckedChanged;

            _estadoConexionLabel.AutoSize = false;
            _estadoConexionLabel.ForeColor = Color.DimGray;
            _estadoConexionLabel.Location = new Point(18, 280);
            _estadoConexionLabel.Size = new Size(480, 36);

            _probarConexionButton.Location = new Point(178, 327);
            _probarConexionButton.Size = new Size(110, 30);
            _probarConexionButton.Text = "Probar conexión";
            _probarConexionButton.Click += ProbarConexionButton_Click;
            _conectarButton.Location = new Point(394, 327);
            _conectarButton.Size = new Size(104, 30);
            _conectarButton.Text = "Conectar";
            _conectarButton.Click += ConectarButton_Click;
            cancelarButton.DialogResult = DialogResult.Cancel;
            cancelarButton.Location = new Point(294, 327);
            cancelarButton.Size = new Size(94, 30);
            cancelarButton.Text = "Cancelar";

            AcceptButton = _conectarButton;
            CancelButton = cancelarButton;
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(516, 374);
            Controls.AddRange(new Control[] { encabezadoLabel, datosConexionGroupBox, _estadoConexionLabel, _probarConexionButton, _conectarButton, cancelarButton });
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            ShowInTaskbar = false;
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Conexión a MySQL";
            datosConexionGroupBox.ResumeLayout(false);
            datosConexionGroupBox.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)_portInput).EndInit();
            ResumeLayout(false);
        }
    }
}
