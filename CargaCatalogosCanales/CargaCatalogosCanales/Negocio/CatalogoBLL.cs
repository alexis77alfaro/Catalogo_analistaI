using CargaCatalogosCanales.Configuracion;
using CargaCatalogosCanales.Datos;
using CargaCatalogosCanales.Entidades;
using CargaCatalogosCanales.Utilidades;

namespace CargaCatalogosCanales.Negocio;

/**
 * clase para manejar la logica de negocio de los catalogos
 * esta clase se encarga de manejar la logica de negocio de los catalogos, como validar, cargar, guardar y consultar
 *  
 */

internal sealed class CatalogoBLL
{
    private static readonly Catalogo[] Catalogos =
    [
        new("Agencias", "catalogos_edenilson_guevara_cat_agencias", "cat_agencias.xlsx"),
        new("ATMs", "catalogos_edenilson_guevara_cat_atms", "cat_atms.xlsx"),
        new("Corresponsales", "catalogos_edenilson_guevara_cat_corresponsales", "cat_corresponsales.csv"),
        new("Kioskos", "catalogos_edenilson_guevara_cat_kioskos", "cat_kioskos.txt")
    ];

    private readonly CatalogoDAL _catalogoDAL = new();
    private readonly BitacoraBLL _bitacoraBLL = new();

    public IReadOnlyList<Catalogo> ObtenerCatalogos() => Catalogos;

    public bool TryValidarArchivo(string nombreArchivo, out string mensaje)
    {
        mensaje = string.Empty;
        if (string.IsNullOrWhiteSpace(nombreArchivo))
        {
            mensaje = "Ingrese el nombre del archivo.";
            return false;
        }

        if (!string.Equals(Path.GetFileName(nombreArchivo), nombreArchivo, StringComparison.Ordinal))
        {
            mensaje = "Ingrese solo el nombre del archivo, sin ruta.";
            return false;
        }

        if (ObtenerCatalogoOpcional(nombreArchivo) is null)
        {
            mensaje = "El nombre debe iniciar con cat_agencias, cat_atms, cat_corresponsales o cat_kioskos.";
            return false;
        }

        string extension = Path.GetExtension(nombreArchivo);
        if (!new[] { ".csv", ".txt", ".xlsx" }.Contains(extension, StringComparer.OrdinalIgnoreCase))
        {
            mensaje = "El archivo debe tener extensión CSV, TXT o XLSX.";
            return false;
        }

        string rutaArchivo = AppConfig.ObtenerRutaEntrada(nombreArchivo);
        if (!File.Exists(rutaArchivo))
        {
            mensaje = $"No se encontró {nombreArchivo} en C:\\subir_archivos.";
            return false;
        }

        return true;
    }

    public (Catalogo Catalogo, List<RegistroCatalogo> Registros) CargarArchivo(string nombreArchivo)
    {
        if (!TryValidarArchivo(nombreArchivo, out string mensaje))
        {
            throw new InvalidOperationException(mensaje);
        }

        Catalogo catalogo = ObtenerCatalogo(nombreArchivo);
        string rutaArchivo = AppConfig.ObtenerRutaEntrada(nombreArchivo);
        ArchivoHelper.ValidarArchivo(rutaArchivo);
        return (catalogo, LectorArchivoCatalogo.Leer(rutaArchivo));
    }

    public ResultadoCarga Guardar(Catalogo catalogo, string nombreArchivo, IReadOnlyCollection<RegistroCatalogo> registros)
    {
        ValidarRegistros(registros);
        RegistroCarga carga = new(nombreArchivo, Path.GetExtension(nombreArchivo).TrimStart('.').ToUpperInvariant(), catalogo.TablaDestino, ObtenerFechaProceso());
        long idEjecucion = _bitacoraBLL.RegistrarInicio(carga);

        try
        {
            int cantidad = _catalogoDAL.ReemplazarCatalogo(catalogo, registros, carga.FechaProceso);
            const string mensaje = "Carga finalizada correctamente.";
            _bitacoraBLL.RegistrarFinalizacion(idEjecucion, carga, cantidad, true, mensaje);
            return new ResultadoCarga(true, cantidad, mensaje);
        }
        catch (Exception exception)
        {
            _bitacoraBLL.RegistrarFinalizacion(idEjecucion, carga, 0, false, exception.ToString());
            throw new InvalidOperationException("No fue posible guardar los datos. Revise la bitácora para más detalle.", exception);
        }
    }

    public List<RegistroCatalogo> Consultar(Catalogo catalogo) => _catalogoDAL.Consultar(catalogo);

    public int Exportar(Catalogo catalogo, string rutaArchivo)
    {
        List<RegistroCatalogo> registros = _catalogoDAL.Consultar(catalogo);
        ExportadorCsv.Exportar(rutaArchivo, registros);
        return registros.Count;
    }

    private static Catalogo ObtenerCatalogo(string nombreArchivo)
    {
        return ObtenerCatalogoOpcional(nombreArchivo)
            ?? throw new InvalidOperationException("El nombre debe iniciar con cat_agencias, cat_atms, cat_corresponsales o cat_kioskos.");
    }

    private static Catalogo? ObtenerCatalogoOpcional(string nombreArchivo)
    {
        string nombreSinExtension = Path.GetFileNameWithoutExtension(nombreArchivo);
        return Catalogos.FirstOrDefault(catalogo =>
            Path.GetFileNameWithoutExtension(catalogo.NombreArchivo).Equals(nombreSinExtension, StringComparison.OrdinalIgnoreCase));
    }

    private static void ValidarRegistros(IReadOnlyCollection<RegistroCatalogo> registros)
    {
        if (registros.Count == 0)
        {
            throw new InvalidOperationException("No hay registros para guardar.");
        }

        if (registros.Any(registro => string.IsNullOrWhiteSpace(registro.Codigo)))
        {
            throw new InvalidOperationException("Todos los registros deben incluir Código.");
        }
    }

    private static DateTime ObtenerFechaProceso()
    {
        DateTime primerDiaMesActual = new(DateTime.Today.Year, DateTime.Today.Month, 1);
        return primerDiaMesActual.AddDays(-1);
    }
}
