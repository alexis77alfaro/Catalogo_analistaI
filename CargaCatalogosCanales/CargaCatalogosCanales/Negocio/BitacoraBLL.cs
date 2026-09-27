using CargaCatalogosCanales.Datos;
using CargaCatalogosCanales.Entidades;

namespace CargaCatalogosCanales.Negocio;

internal sealed class BitacoraBLL
{
    private readonly BitacoraDAL _bitacoraDAL = new();

    public long RegistrarInicio(RegistroCarga carga) => _bitacoraDAL.RegistrarInicio(carga);

    public List<RegistroBitacora> ConsultarRecientes() => _bitacoraDAL.ConsultarRecientes();

    public void RegistrarFinalizacion(long idEjecucion, RegistroCarga carga, int cantidad, bool exitoso, string mensaje)
    {
        _bitacoraDAL.RegistrarFinalizacion(idEjecucion, carga, cantidad, exitoso, mensaje);
    }
}
