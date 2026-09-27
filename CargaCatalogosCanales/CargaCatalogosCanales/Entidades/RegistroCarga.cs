namespace CargaCatalogosCanales.Entidades;

public sealed record RegistroCarga(
    string NombreArchivo,
    string TipoArchivo,
    string TablaDestino,
    DateTime FechaProceso);

public sealed record ResultadoCarga(bool Exitoso, int CantidadRegistros, string Mensaje);

public sealed record RegistroBitacora(
    string NombreArchivo,
    string TipoArchivo,
    string TablaDestino,
    DateTime FechaInicio,
    DateTime? FechaFin,
    int CantidadRegistros,
    string Estado,
    string Mensaje);
