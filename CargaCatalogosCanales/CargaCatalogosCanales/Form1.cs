using CargaCatalogosCanales.Entidades;
using CargaCatalogosCanales.Negocio;
using CargaCatalogosCanales.Utilidades;
using System.Diagnostics;
using System.IO;

namespace CargaCatalogosCanales;

public partial class Form1 : Form
{
    private readonly CatalogoBLL _catalogoBLL = new();
    private Catalogo? _catalogoCargado;

    public Form1()
    {
        InitializeComponent();
    }

    // ============================================================
    // CARGAR DATOS
    // ============================================================

    private async void CargarDatos_Click(
        object? sender,
        EventArgs e)
    {
        if (!ValidarNombreArchivo())
        {
            return;
        }

        try
        {
            CambiarEstado("Cargando archivo...");

            string nombreArchivo =
                _nombreArchivoTextBox.Text.Trim();

            (Catalogo catalogo, List<RegistroCatalogo> registros) =
                await Task.Run(
                    () => _catalogoBLL.CargarArchivo(nombreArchivo));

            _catalogoCargado = catalogo;

            MostrarRegistros(registros);

            CambiarEstado(
                $"Se cargaron {registros.Count} registro(s) " +
                $"de {catalogo.Nombre}.");
        }
        catch
        {
            MostrarError(
                "No fue posible cargar el archivo. " +
                "Verifique el formato e inténtelo nuevamente.");
        }
    }

    // ============================================================
    // LIMPIAR
    // ============================================================

    private void Limpiar_Click(
        object? sender,
        EventArgs e)
    {
        if (_catalogoGrid.Rows
            .Cast<DataGridViewRow>()
            .Any(fila => !fila.IsNewRow) &&
            MessageBox.Show(
                this,
                "Se eliminarán los datos no guardados. " +
                "¿Desea continuar?",
                Text,
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question) != DialogResult.Yes)
        {
            return;
        }

        LimpiarFormularioSinConfirmacion();
    }

    // ============================================================
    // GUARDAR DATOS
    // ============================================================

    private async void GuardarDatos_Click(
        object? sender,
        EventArgs e)
    {
        if (!ValidarNombreArchivo() ||
            _catalogoCargado is null)
        {
            MostrarError(
                "Primero cargue un archivo válido antes de guardar.");

            return;
        }

        List<RegistroCatalogo> registros =
            ObtenerRegistros();

        if (!ValidarRegistros(registros))
        {
            return;
        }

        try
        {
            CambiarEstado("Guardando datos...");

            ResultadoCarga resultado =
                await Task.Run(
                    () => _catalogoBLL.Guardar(
                        _catalogoCargado,
                        _nombreArchivoTextBox.Text.Trim(),
                        registros));

            MessageBox.Show(
                this,
                $"Se guardaron {resultado.CantidadRegistros} " +
                "registros y se actualizó la bitácora.",
                Text,
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);

            LimpiarFormularioSinConfirmacion();
        }
        catch
        {
            MostrarError(
                "No fue posible guardar los datos. " +
                "Consulte la bitácora para revisar el resultado.");
        }
    }

    // ============================================================
    // EXPORTAR CATÁLOGOS
    // ============================================================

    private async void ExportarCatalogos_Click(
        object? sender,
        EventArgs e)
    {
        if (_catalogoCargado is null)
        {
            MostrarError(
                "Primero cargue un catálogo válido antes de exportarlo.");

            return;
        }

        try
        {
            // ========================================================
            // 1. OBTENER REGISTROS DEL DATAGRIDVIEW
            // ========================================================

            List<RegistroCatalogo> registros =
                ObtenerRegistros();

            if (!ValidarRegistros(registros))
            {
                return;
            }

            // ========================================================
            // 2. BUSCAR PLANTILLA DENTRO DEL PROYECTO
            // ========================================================

            string rutaPlantilla =
                Path.Combine(
                    AppContext.BaseDirectory,
                    "Recursos",
                    "PLANTILLA_REPORTE_CANALES.xlsx");

            if (!File.Exists(rutaPlantilla))
            {
                MostrarError(
                    "No se encontró la plantilla de exportación.\n\n" +
                    "Archivo requerido:\n" +
                    "PLANTILLA_REPORTE_CANALES.xlsx\n\n" +
                    $"Ruta buscada:\n{rutaPlantilla}\n\n" +
                    "Verifique que la plantilla exista dentro de " +
                    "la carpeta Recursos de la aplicación.");

                return;
            }

            // ========================================================
            // 3. OBTENER NOMBRE DEL ARCHIVO DE ORIGEN
            // ========================================================

            string nombreArchivoOrigen =
                _nombreArchivoTextBox.Text.Trim();

            if (string.IsNullOrWhiteSpace(
                nombreArchivoOrigen))
            {
                MostrarError(
                    "No se encontró el nombre del archivo de origen.");

                return;
            }

            // ========================================================
            // 4. VALIDAR RUTA DE EXPORTACIÓN
            // ========================================================

            string carpetaReporte =
                @"C:\Reportes_canales";

            if (!Directory.Exists(carpetaReporte))
            {
                MostrarError(
                    "La ruta de exportación no está creada.\n\n" +
                    $"Ruta requerida:\n{carpetaReporte}\n\n" +
                    "Por favor, cree la carpeta o verifique que " +
                    "el nombre de la ruta sea correcto.");

                return;
            }

            // ========================================================
            // 5. GENERAR NOMBRE DEL REPORTE
            // ========================================================

            string nombreReporte =
                $"{DateTime.Now:yyyyMMdd}_reportes_canales.xlsx";

            string rutaSalida =
                Path.Combine(
                    carpetaReporte,
                    nombreReporte);

            // ========================================================
            // 6. EVITAR SOBRESCRIBIR
            // ========================================================

            rutaSalida =
                ObtenerRutaDisponible(rutaSalida);

            // ========================================================
            // 7. GENERAR REPORTE
            // ========================================================

            CambiarEstado(
                "Generando reporte Excel...");

            await Task.Run(() =>
                ExportadorExcel.GenerarReporte(
                    rutaPlantilla,
                    rutaSalida,
                    registros,
                    nombreArchivoOrigen));

            // ========================================================
            // 8. ABRIR EXCEL AUTOMÁTICAMENTE
            // ========================================================

            Process.Start(
                new ProcessStartInfo
                {
                    FileName = rutaSalida,
                    UseShellExecute = true
                });

            // ========================================================
            // 9. ACTUALIZAR ESTADO
            // ========================================================

            CambiarEstado(
                $"Reporte generado correctamente. " +
                $"{registros.Count} registro(s) exportado(s).");

            // ========================================================
            // 10. MENSAJE FINAL
            // ========================================================

            MessageBox.Show(
                this,
                $"Reporte generado correctamente.\n\n" +
                $"Archivo de origen: {nombreArchivoOrigen}\n" +
                $"Registros exportados: {registros.Count}\n\n" +
                $"Reporte:\n{Path.GetFileName(rutaSalida)}\n\n" +
                $"Ubicación:\n{rutaSalida}",
                Text,
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);
        }
        catch (Exception ex)
        {
            MostrarError(
                "No fue posible generar el reporte.\n\n" +
                ex.Message);
        }
    }

    // ============================================================
    // OBTENER UNA RUTA DISPONIBLE
    // ============================================================

    private static string ObtenerRutaDisponible(
        string rutaOriginal)
    {
        if (!File.Exists(rutaOriginal))
        {
            return rutaOriginal;
        }

        string directorio =
            Path.GetDirectoryName(rutaOriginal)!;

        string nombre =
            Path.GetFileNameWithoutExtension(
                rutaOriginal);

        string extension =
            Path.GetExtension(rutaOriginal);

        int contador = 1;

        string nuevaRuta;

        do
        {
            nuevaRuta =
                Path.Combine(
                    directorio,
                    $"{nombre}({contador}){extension}");

            contador++;

        } while (File.Exists(nuevaRuta));

        return nuevaRuta;
    }

    // ============================================================
    // BITÁCORA
    // ============================================================

    private void BitacoraButton_Click(
        object? sender,
        EventArgs e)
    {
        if (!DatabaseConnection.IsConfigured)
        {
            MostrarError(
                "No hay una conexión configurada " +
                "para consultar la bitácora.");

            return;
        }

        using BitacoraForm bitacora =
            new();

        bitacora.ShowDialog(this);
    }

    // ============================================================
    // VALIDACIÓN DEL ARCHIVO
    // ============================================================

    private void NombreArchivoTextBox_Validating(
        object? sender,
        System.ComponentModel.CancelEventArgs e)
    {
        e.Cancel =
            !ValidarNombreArchivo();
    }

    private bool ValidarNombreArchivo()
    {
        string nombreArchivo =
            _nombreArchivoTextBox.Text.Trim();

        if (_catalogoBLL.TryValidarArchivo(
            nombreArchivo,
            out string mensaje))
        {
            _errores.SetError(
                _nombreArchivoTextBox,
                string.Empty);

            return true;
        }

        _errores.SetError(
            _nombreArchivoTextBox,
            mensaje);

        CambiarEstado(mensaje);

        return false;
    }

    // ============================================================
    // VALIDACIÓN DE REGISTROS
    // ============================================================

    private bool ValidarRegistros(
        IReadOnlyCollection<RegistroCatalogo> registros)
    {
        if (registros.Count == 0)
        {
            MostrarError(
                "Debe cargar o ingresar al menos un registro " +
                "antes de guardar.");

            return false;
        }

        if (registros.Any(
            registro =>
                string.IsNullOrWhiteSpace(
                    registro.Codigo)))
        {
            MostrarError(
                "Cada registro debe tener un Código.");

            return false;
        }

        return true;
    }

    // ============================================================
    // MOSTRAR REGISTROS EN DATAGRIDVIEW
    // ============================================================

    private void MostrarRegistros(
        IEnumerable<RegistroCatalogo> registros)
    {
        _catalogoGrid.Rows.Clear();

        foreach (RegistroCatalogo registro in registros)
        {
            _catalogoGrid.Rows.Add(
                registro.ComoValores());
        }
    }

    // ============================================================
    // OBTENER REGISTROS DEL DATAGRIDVIEW
    // ============================================================

    private List<RegistroCatalogo> ObtenerRegistros()
    {
        return _catalogoGrid.Rows
            .Cast<DataGridViewRow>()
            .Where(fila => !fila.IsNewRow)
            .Select(
                fila =>
                    new RegistroCatalogo(
                        ValorCelda(
                            fila,
                            "codigo"),

                        ValorCelda(
                            fila,
                            "nombre"),

                        ValorCelda(
                            fila,
                            "departamento"),

                        ValorCelda(
                            fila,
                            "municipio"),

                        ValorCelda(
                            fila,
                            "direccion")))
            .Where(
                registro =>
                    !registro.EstaVacio)
            .ToList();
    }

    // ============================================================
    // OBTENER VALOR DE UNA CELDA
    // ============================================================

    private static string ValorCelda(
        DataGridViewRow fila,
        string columna)
    {
        return fila.Cells[columna]
            .Value?
            .ToString()?
            .Trim()
            ?? string.Empty;
    }

    // ============================================================
    // LIMPIAR FORMULARIO
    // ============================================================

    private void LimpiarFormularioSinConfirmacion()
    {
        _nombreArchivoTextBox.Clear();

        _errores.SetError(
            _nombreArchivoTextBox,
            string.Empty);

        _catalogoGrid.Rows.Clear();

        _catalogoCargado = null;

        CambiarEstado(
            "Carga finalizada. El formulario está listo " +
            "para un nuevo archivo.");

        _nombreArchivoTextBox.Focus();
    }

    // ============================================================
    // CAMBIAR ESTADO
    // ============================================================

    private void CambiarEstado(
        string mensaje)
    {
        _estadoLabel.Text =
            mensaje;
    }

    // ============================================================
    // MOSTRAR ERROR
    // ============================================================

    private void MostrarError(
        string mensaje)
    {
        CambiarEstado(mensaje);

        MessageBox.Show(
            this,
            mensaje,
            Text,
            MessageBoxButtons.OK,
            MessageBoxIcon.Warning);
    }
}