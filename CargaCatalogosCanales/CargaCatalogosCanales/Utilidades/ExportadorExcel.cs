using System;
using System.Collections.Generic;
using System.Text;

using ClosedXML.Excel;
using CargaCatalogosCanales.Entidades;

namespace CargaCatalogosCanales.Utilidades;

public static class ExportadorExcel
{
    public static void GenerarReporte(
        string rutaPlantilla,
        string rutaSalida,
        IEnumerable<RegistroCatalogo> registros,
        string nombreArchivoOrigen)
    {
        // ========================================================
        // VALIDAR PLANTILLA
        // ========================================================

        if (!File.Exists(rutaPlantilla))
        {
            throw new FileNotFoundException(
                "No se encontró la plantilla de exportación.",
                rutaPlantilla);
        }

        // ========================================================
        // CREAR CARPETA DE SALIDA
        // ========================================================

        string? directorio =
            Path.GetDirectoryName(rutaSalida);

        if (!string.IsNullOrWhiteSpace(directorio))
        {
            Directory.CreateDirectory(directorio);
        }

        // ========================================================
        // COPIAR PLANTILLA
        // ========================================================

        File.Copy(
            rutaPlantilla,
            rutaSalida,
            true);

        // ========================================================
        // ABRIR ARCHIVO EXCEL
        // ========================================================

        using XLWorkbook workbook =
            new XLWorkbook(rutaSalida);

        // ========================================================
        // VALIDAR HOJA "CANALES"
        // ========================================================

        if (!workbook.Worksheets.Contains("Canales"))
        {
            throw new InvalidOperationException(
                "La plantilla no contiene la hoja 'Canales'.");
        }

        IXLWorksheet hoja =
            workbook.Worksheet("Canales");

        // ========================================================
        // ENCABEZADOS
        // ========================================================

        hoja.Cell(1, 1).Value = "codigo";
        hoja.Cell(1, 2).Value = "nombre";
        hoja.Cell(1, 3).Value = "departamento";
        hoja.Cell(1, 4).Value = "municipio";
        hoja.Cell(1, 5).Value = "direccion";
        hoja.Cell(1, 6).Value = "origen";

        // ========================================================
        // FORMATO DEL ENCABEZADO
        // ========================================================

        IXLRange encabezado =
            hoja.Range("A1:F1");

        encabezado.Style.Font.Bold = true;

        encabezado.Style.Alignment.Horizontal =
            XLAlignmentHorizontalValues.Center;

        encabezado.Style.Alignment.Vertical =
            XLAlignmentVerticalValues.Center;

        // ========================================================
        // LIMPIAR DATOS ANTERIORES
        // ========================================================

        var ultimaFila =
            hoja.LastRowUsed();

        if (ultimaFila != null)
        {
            int numeroUltimaFila =
                ultimaFila.RowNumber();

            if (numeroUltimaFila >= 2)
            {
                hoja.Range(
                    2,
                    1,
                    numeroUltimaFila,
                    6)
                    .Clear();
            }
        }

        // ========================================================
        // ESCRIBIR REGISTROS
        // ========================================================

        int fila = 2;

        foreach (RegistroCatalogo registro in registros)
        {
            // Código
            hoja.Cell(fila, 1).Value =
                registro.Codigo;

            // Nombre
            hoja.Cell(fila, 2).Value =
                registro.Nombre;

            // Departamento
            hoja.Cell(fila, 3).Value =
                registro.Departamento;

            // Municipio
            hoja.Cell(fila, 4).Value =
                registro.Municipio;

            // Dirección
            hoja.Cell(fila, 5).Value =
                $"Departamento de {registro.Departamento}, " +
                $"municipio de {registro.Municipio}";

            // Archivo de origen
            hoja.Cell(fila, 6).Value =
                nombreArchivoOrigen;

            fila++;
        }

        // ========================================================
        // FORMATO DE LAS COLUMNAS
        // ========================================================

        hoja.Columns("A:F")
            .AdjustToContents();

        // Limitar el ancho de la dirección
        if (hoja.Column(5).Width > 60)
        {
            hoja.Column(5).Width = 60;
        }

        // Limitar el ancho del origen
        if (hoja.Column(6).Width > 35)
        {
            hoja.Column(6).Width = 35;
        }

        // ========================================================
        // CONGELAR ENCABEZADO
        // ========================================================

        hoja.SheetView
            .FreezeRows(1);

        // ========================================================
        // ACTIVAR FILTRO
        // ========================================================

        if (fila > 2)
        {
            hoja.Range(
                1,
                1,
                fila - 1,
                6)
                .SetAutoFilter();
        }

        // ========================================================
        // GUARDAR EXCEL
        // ========================================================

        workbook.Save();
    }
}