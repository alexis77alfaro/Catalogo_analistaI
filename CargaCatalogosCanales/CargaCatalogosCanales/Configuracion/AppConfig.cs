namespace CargaCatalogosCanales.Configuracion;



/* esta clase se encarga de almacenar las rutas de los archivos de entrada, plantillas y reportes
 * 
 * aca se definen las rutas de los archivos de entrada, plantillas y reportes, para que puedan ser utilizadas en toda la aplicacion
 * 
 */
internal static class AppConfig
{
    public const string RutaArchivosEntrada = @"C:\subir_archivos";
    public const string RutaPlantillas = @"C:\plantillas_exportacion";
    public const string RutaReportes = @"C:\Reportes_canales";

    public static string ObtenerRutaEntrada(string nombreArchivo)
    {
        if (string.IsNullOrWhiteSpace(nombreArchivo) ||
            !string.Equals(Path.GetFileName(nombreArchivo), nombreArchivo, StringComparison.Ordinal))
        {
            throw new ArgumentException("Escriba únicamente el nombre del archivo.");
        }

        return Path.Combine(RutaArchivosEntrada, nombreArchivo);
    }
}
