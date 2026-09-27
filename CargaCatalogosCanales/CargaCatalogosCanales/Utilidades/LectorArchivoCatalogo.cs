using System.Text;
using CargaCatalogosCanales.Entidades;
using ExcelDataReader;

namespace CargaCatalogosCanales.Utilidades;


/*
 * clase para leer archivos de catalogos, ya sea en formato excel o delimitado
 * esta clase se encarga de leer los archivos de catalogos y convertirlos en una lista de registros de catalogo
 * esat clase fue generada con la ayuda de ia para obtener ejemplos de como leer archivos de excel y delimitados, y convertirlos en una lista de objetos
 */

internal static class LectorArchivoCatalogo
{
    public static List<RegistroCatalogo> Leer(string rutaArchivo)
    {
        return Path.GetExtension(rutaArchivo).Equals(".xlsx", StringComparison.OrdinalIgnoreCase)
            ? LeerExcel(rutaArchivo)
            : LeerDelimitado(rutaArchivo);
    }

    private static List<RegistroCatalogo> LeerExcel(string rutaArchivo)
    {
        Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
        using FileStream stream = File.Open(rutaArchivo, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
        using IExcelDataReader reader = ExcelReaderFactory.CreateReader(stream);
        List<string[]> filas = [];

        while (reader.Read())
        {
            filas.Add(Enumerable.Range(0, Math.Min(5, reader.FieldCount))
                .Select(indice => reader.GetValue(indice)?.ToString()?.Trim() ?? string.Empty)
                .ToArray());
        }

        return ConvertirFilas(filas);
    }

    private static List<RegistroCatalogo> LeerDelimitado(string rutaArchivo)
    {
        List<string[]> filas = [];
        foreach (string linea in File.ReadLines(rutaArchivo))
        {
            if (!string.IsNullOrWhiteSpace(linea))
            {
                filas.Add(SepararLinea(linea, DetectarSeparador(linea)).Take(5).ToArray());
            }
        }

        return ConvertirFilas(filas);
    }

    private static List<RegistroCatalogo> ConvertirFilas(List<string[]> filas)
    {
        if (filas.Count > 0 && filas[0].Any(valor => valor.Equals("codigo", StringComparison.OrdinalIgnoreCase)))
        {
            filas.RemoveAt(0);
        }

        return filas.Select(CrearRegistro).Where(registro => !registro.EstaVacio).ToList();
    }

    private static RegistroCatalogo CrearRegistro(string[] origen)
    {
        string[] valores = new string[5];
        Array.Copy(origen, valores, Math.Min(origen.Length, valores.Length));
        return new RegistroCatalogo(valores[0] ?? string.Empty, valores[1] ?? string.Empty, valores[2] ?? string.Empty,
            valores[3] ?? string.Empty, valores[4] ?? string.Empty);
    }

    private static char DetectarSeparador(string linea)
    {
        return new[] { ';', ',', '\t', '|' }.OrderByDescending(separador => linea.Count(caracter => caracter == separador)).First();
    }

    private static List<string> SepararLinea(string linea, char separador)
    {
        List<string> valores = [];
        StringBuilder valor = new();
        bool entreComillas = false;

        for (int indice = 0; indice < linea.Length; indice++)
        {
            char caracter = linea[indice];
            if (caracter == '"')
            {
                if (entreComillas && indice + 1 < linea.Length && linea[indice + 1] == '"')
                {
                    valor.Append(caracter);
                    indice++;
                }
                else
                {
                    entreComillas = !entreComillas;
                }
            }
            else if (caracter == separador && !entreComillas)
            {
                valores.Add(valor.ToString().Trim());
                valor.Clear();
            }
            else
            {
                valor.Append(caracter);
            }
        }

        valores.Add(valor.ToString().Trim());
        return valores;
    }
}
