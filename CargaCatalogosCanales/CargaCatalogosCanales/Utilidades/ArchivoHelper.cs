namespace CargaCatalogosCanales.Utilidades;

internal static class ArchivoHelper

/**
 * clase para validar archivos de catalogos
 * esta clase se encarga de validar los archivos de catalogos, ya sea en formato excel o delimitado
 * 
 */
{
    private static readonly HashSet<string> ExtensionesPermitidas =
        new(StringComparer.OrdinalIgnoreCase) { ".csv", ".txt", ".xlsx" };

    public static void ValidarArchivo(string rutaArchivo)
    {
        if (!File.Exists(rutaArchivo))
        {
            throw new FileNotFoundException("El archivo no existe en la carpeta configurada.");
        }

        if (!ExtensionesPermitidas.Contains(Path.GetExtension(rutaArchivo)))
        {
            throw new InvalidOperationException("El archivo debe ser CSV, XLSX o TXT.");
        }
    }
}
