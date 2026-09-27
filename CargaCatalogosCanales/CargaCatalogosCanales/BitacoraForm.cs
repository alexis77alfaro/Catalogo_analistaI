using CargaCatalogosCanales.Entidades;
using CargaCatalogosCanales.Negocio;

namespace CargaCatalogosCanales;

internal sealed class BitacoraForm : Form
{
    private readonly BitacoraBLL _bitacoraBLL = new();
    private readonly DataGridView _grid = new();
    private readonly Button _actualizarButton = new();
    private readonly Label _estadoLabel = new();

    public BitacoraForm()
    {
        Text = "Bitácora de cargas";
        StartPosition = FormStartPosition.CenterParent;
        MinimumSize = new Size(900, 420);
        Size = new Size(1100, 550);

        TableLayoutPanel layout = new() { ColumnCount = 1, Dock = DockStyle.Fill, Padding = new Padding(12), RowCount = 3 };
        layout.RowStyles.Add(new RowStyle(SizeType.AutoSize));
        layout.RowStyles.Add(new RowStyle(SizeType.Percent, 100));
        layout.RowStyles.Add(new RowStyle(SizeType.AutoSize));

        _actualizarButton.AutoSize = true;
        _actualizarButton.Text = "Actualizar";
        _actualizarButton.Click += ActualizarButton_Click;

        _grid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        _grid.BackgroundColor = SystemColors.Window;
        _grid.Dock = DockStyle.Fill;
        _grid.ReadOnly = true;
        _grid.RowHeadersVisible = false;
        _grid.Columns.Add("archivo", "Archivo");
        _grid.Columns.Add("tipo", "Tipo");
        _grid.Columns.Add("tabla", "Tabla destino");
        _grid.Columns.Add("inicio", "Inicio");
        _grid.Columns.Add("fin", "Fin");
        _grid.Columns.Add("cantidad", "Registros");
        _grid.Columns.Add("estado", "Estado");
        _grid.Columns.Add("mensaje", "Mensaje");

        _estadoLabel.AutoSize = true;
        _estadoLabel.ForeColor = Color.DimGray;
        layout.Controls.Add(_actualizarButton, 0, 0);
        layout.Controls.Add(_grid, 0, 1);
        layout.Controls.Add(_estadoLabel, 0, 2);
        Controls.Add(layout);
        Shown += async (_, _) => await CargarBitacoraAsync();
    }

    private async void ActualizarButton_Click(object? sender, EventArgs e) => await CargarBitacoraAsync();

    private async Task CargarBitacoraAsync()
    {
        try
        {
            _actualizarButton.Enabled = false;
            _estadoLabel.Text = "Consultando bitácora...";
            List<RegistroBitacora> registros = await Task.Run(_bitacoraBLL.ConsultarRecientes);
            _grid.Rows.Clear();
            foreach (RegistroBitacora registro in registros)
            {
                _grid.Rows.Add(registro.NombreArchivo, registro.TipoArchivo, registro.TablaDestino,
                    registro.FechaInicio.ToString("yyyy-MM-dd HH:mm"), registro.FechaFin?.ToString("yyyy-MM-dd HH:mm") ?? string.Empty,
                    registro.CantidadRegistros, registro.Estado, registro.Mensaje);
            }

            _estadoLabel.Text = $"{registros.Count} ejecución(es) mostrada(s).";
        }
        catch
        {
            _estadoLabel.Text = "No fue posible consultar la bitácora.";
            MessageBox.Show(this, "No fue posible consultar la bitácora. Verifique la conexión y las tablas.", Text,
                MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        finally
        {
            _actualizarButton.Enabled = true;
        }
    }
}
