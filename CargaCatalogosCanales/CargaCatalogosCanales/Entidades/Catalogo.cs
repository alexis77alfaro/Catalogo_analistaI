namespace CargaCatalogosCanales.Entidades;

public sealed record Catalogo(string Nombre, string TablaDestino, string NombreArchivo);

/**
 * clase para representar un registro de catalogo es de cir entidades[Serializable]
 * esta clase se encarga de representar un registro de catalogo, con sus propiedades y metodos
 * 
 */

public sealed record RegistroCatalogo(
    string Codigo,
    string Nombre,
    string Departamento,
    string Municipio,
    string Direccion)
{
    public bool EstaVacio => string.IsNullOrWhiteSpace(Codigo) && string.IsNullOrWhiteSpace(Nombre) &&
                             string.IsNullOrWhiteSpace(Departamento) && string.IsNullOrWhiteSpace(Municipio) &&
                             string.IsNullOrWhiteSpace(Direccion);

    public string[] ComoValores() => [Codigo, Nombre, Departamento, Municipio, Direccion];
}
