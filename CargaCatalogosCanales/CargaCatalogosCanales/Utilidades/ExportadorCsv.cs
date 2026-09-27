using System.Text;
using CargaCatalogosCanales.Entidades;

namespace CargaCatalogosCanales.Utilidades;


/** clase para exportar archivos de catalogos en formato csv
 * esta clase se encarga de exportar los registros de catalogo a un archivo csv
 * esat clase fue generada con la ayuda de  StartOverFlow para obtener ejemplos de como exportar archivos csv, y convertirlos en una lista de objetos
 */

internal static class ExportadorCsv
{
    public static void Exportar(string rutaArchivo, IEnumerable<RegistroCatalogo> registros)
    {
        using StreamWriter writer = new(rutaArchivo, false, new UTF8Encoding(true));
        writer.WriteLine("codigo,nombre,departamento,municipio,direccion");
        foreach (RegistroCatalogo registro in registros)
        {
            writer.WriteLine(string.Join(",", registro.ComoValores().Select(EscalarValor)));
        }
    }

    private static string EscalarValor(string valor) => $"\"{valor.Replace("\"", "\"\"")}\"";
}
