namespace CargaCatalogosCanales
{
    partial class Form1
    {
        private System.ComponentModel.IContainer components = null!;
        private TableLayoutPanel layoutPrincipal = null!;
        private FlowLayoutPanel barraAcciones = null!;
        private Label nombreArchivoLabel = null!;
        private TextBox _nombreArchivoTextBox = null!;
        private Button cargarDatosButton = null!;
        private Button limpiarButton = null!;
        private Button guardarDatosButton = null!;
        private Button exportarCatalogosButton = null!;
        private DataGridView _catalogoGrid = null!;
        private FlowLayoutPanel piePagina = null!;
        private Button bitacoraButton = null!;
        private Label _estadoLabel = null!;
        private ErrorProvider _errores = null!;

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
            layoutPrincipal = new TableLayoutPanel();
            barraAcciones = new FlowLayoutPanel();
            nombreArchivoLabel = new Label();
            _nombreArchivoTextBox = new TextBox();
            cargarDatosButton = new Button();
            limpiarButton = new Button();
            guardarDatosButton = new Button();
            exportarCatalogosButton = new Button();
            _catalogoGrid = new DataGridView();
            piePagina = new FlowLayoutPanel();
            bitacoraButton = new Button();
            _estadoLabel = new Label();
            _errores = new ErrorProvider(components);
            ((System.ComponentModel.ISupportInitialize)_catalogoGrid).BeginInit();
            ((System.ComponentModel.ISupportInitialize)_errores).BeginInit();
            SuspendLayout();

            layoutPrincipal.ColumnCount = 1;
            layoutPrincipal.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            layoutPrincipal.Dock = DockStyle.Fill;
            layoutPrincipal.Padding = new Padding(14);
            layoutPrincipal.RowCount = 3;
            layoutPrincipal.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            layoutPrincipal.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            layoutPrincipal.RowStyles.Add(new RowStyle(SizeType.AutoSize));

            barraAcciones.AutoSize = true;
            barraAcciones.Dock = DockStyle.Fill;
            barraAcciones.Padding = new Padding(0, 0, 0, 8);
            barraAcciones.WrapContents = true;

            nombreArchivoLabel.AutoSize = true;
            nombreArchivoLabel.ForeColor = Color.FromArgb(31, 52, 78);
            nombreArchivoLabel.Margin = new Padding(0, 7, 8, 0);
            nombreArchivoLabel.Text = "Nombre del archivo:";

            _nombreArchivoTextBox.BackColor = Color.White;
            _nombreArchivoTextBox.Margin = new Padding(0, 3, 18, 0);
            _nombreArchivoTextBox.Size = new Size(220, 23);
            _nombreArchivoTextBox.Validating += NombreArchivoTextBox_Validating;

            cargarDatosButton.AutoSize = true;
            cargarDatosButton.BackColor = Color.FromArgb(230, 234, 239);
            cargarDatosButton.FlatStyle = FlatStyle.Flat;
            cargarDatosButton.ForeColor = Color.FromArgb(31, 52, 78);
            cargarDatosButton.Margin = new Padding(0, 3, 8, 0);
            cargarDatosButton.Padding = new Padding(10, 3, 10, 3);
            cargarDatosButton.Text = "Cargar datos";
            cargarDatosButton.Click += CargarDatos_Click;

            limpiarButton.AutoSize = true;
            limpiarButton.BackColor = Color.FromArgb(230, 234, 239);
            limpiarButton.FlatStyle = FlatStyle.Flat;
            limpiarButton.ForeColor = Color.FromArgb(31, 52, 78);
            limpiarButton.Margin = new Padding(0, 3, 8, 0);
            limpiarButton.Padding = new Padding(10, 3, 10, 3);
            limpiarButton.Text = "Limpiar";
            limpiarButton.Click += Limpiar_Click;

            guardarDatosButton.AutoSize = true;
            guardarDatosButton.BackColor = Color.FromArgb(230, 234, 239);
            guardarDatosButton.FlatStyle = FlatStyle.Flat;
            guardarDatosButton.ForeColor = Color.FromArgb(31, 52, 78);
            guardarDatosButton.Margin = new Padding(0, 3, 8, 0);
            guardarDatosButton.Padding = new Padding(10, 3, 10, 3);
            guardarDatosButton.Text = "Guardar datos";
            guardarDatosButton.Click += GuardarDatos_Click;

            exportarCatalogosButton.AutoSize = true;
            exportarCatalogosButton.BackColor = Color.FromArgb(230, 234, 239);
            exportarCatalogosButton.FlatStyle = FlatStyle.Flat;
            exportarCatalogosButton.ForeColor = Color.FromArgb(31, 52, 78);
            exportarCatalogosButton.Margin = new Padding(0, 3, 8, 0);
            exportarCatalogosButton.Padding = new Padding(10, 3, 10, 3);
            exportarCatalogosButton.Text = "Exportar catálogos";
            exportarCatalogosButton.Click += ExportarCatalogos_Click;

            barraAcciones.Controls.AddRange(new Control[] { nombreArchivoLabel, _nombreArchivoTextBox, cargarDatosButton, limpiarButton, guardarDatosButton, exportarCatalogosButton });

            _catalogoGrid.AllowUserToAddRows = true;
            _catalogoGrid.AllowUserToDeleteRows = true;
            _catalogoGrid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            _catalogoGrid.BackgroundColor = SystemColors.Window;
            _catalogoGrid.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            _catalogoGrid.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(31, 52, 78);
            _catalogoGrid.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            _catalogoGrid.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            _catalogoGrid.Dock = DockStyle.Fill;
            _catalogoGrid.EnableHeadersVisualStyles = false;
            _catalogoGrid.RowHeadersVisible = false;
            _catalogoGrid.RowsDefaultCellStyle.BackColor = Color.White;
            _catalogoGrid.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(244, 247, 250);
            _catalogoGrid.Columns.AddRange(new DataGridViewColumn[]
            {
                new DataGridViewTextBoxColumn { Name = "codigo", HeaderText = "Código", SortMode = DataGridViewColumnSortMode.NotSortable },
                new DataGridViewTextBoxColumn { Name = "nombre", HeaderText = "Nombre", SortMode = DataGridViewColumnSortMode.NotSortable },
                new DataGridViewTextBoxColumn { Name = "departamento", HeaderText = "Departamento", SortMode = DataGridViewColumnSortMode.NotSortable },
                new DataGridViewTextBoxColumn { Name = "municipio", HeaderText = "Municipio", SortMode = DataGridViewColumnSortMode.NotSortable },
                new DataGridViewTextBoxColumn { Name = "direccion", HeaderText = "Dirección", SortMode = DataGridViewColumnSortMode.NotSortable }
            });

            piePagina.AutoSize = true;
            piePagina.Dock = DockStyle.Fill;
            piePagina.Padding = new Padding(0, 8, 0, 0);
            piePagina.WrapContents = false;

            bitacoraButton.AutoSize = true;
            bitacoraButton.BackColor = Color.FromArgb(31, 52, 78);
            bitacoraButton.FlatStyle = FlatStyle.Flat;
            bitacoraButton.ForeColor = Color.White;
            bitacoraButton.Margin = new Padding(0, 0, 14, 0);
            bitacoraButton.Padding = new Padding(10, 3, 10, 3);
            bitacoraButton.Text = "Ver bitácora";
            bitacoraButton.Click += BitacoraButton_Click;

            _estadoLabel.AutoSize = true;
            _estadoLabel.ForeColor = Color.FromArgb(69, 79, 92);
            _estadoLabel.Margin = new Padding(0, 7, 0, 0);
            _estadoLabel.Text = "Escriba el nombre del archivo ubicado en C:\\subir_archivos.";
            piePagina.Controls.AddRange(new Control[] { bitacoraButton, _estadoLabel });

            _errores.ContainerControl = this;
            layoutPrincipal.Controls.Add(barraAcciones, 0, 0);
            layoutPrincipal.Controls.Add(_catalogoGrid, 0, 1);
            layoutPrincipal.Controls.Add(piePagina, 0, 2);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(244, 247, 250);
            ClientSize = new Size(1120, 650);
            Controls.Add(layoutPrincipal);
            MinimumSize = new Size(900, 520);
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Carga de catálogos de canales - CONTOSO";
            ((System.ComponentModel.ISupportInitialize)_catalogoGrid).EndInit();
            ((System.ComponentModel.ISupportInitialize)_errores).EndInit();
            ResumeLayout(false);
        }
    }
}
