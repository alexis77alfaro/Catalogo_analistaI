Namespace Entidades
    Public Class Catalogo
        Public Property Nombre As String
        Public Property TablaDestino As String
        Public Property NombreArchivo As String
    End Class

    Public Class RegistroCatalogo
        Public Property Codigo As String
        Public Property Nombre As String
        Public Property Departamento As String
        Public Property Municipio As String
        Public Property Direccion As String
    End Class

    Public Class RegistroCarga
        Public Property NombreArchivo As String
        Public Property TipoArchivo As String
        Public Property TablaDestino As String
        Public Property FechaProceso As DateTime
    End Class

    Public Class ResultadoCarga
        Public Property Exitoso As Boolean
        Public Property CantidadRegistros As Integer
        Public Property Mensaje As String
    End Class

    Public Class RegistroBitacora
        Public Property NombreArchivo As String
        Public Property TipoArchivo As String
        Public Property TablaDestino As String
        Public Property FechaInicio As DateTime
        Public Property FechaFin As Nullable(Of DateTime)
        Public Property CantidadRegistros As Integer
        Public Property Estado As String
        Public Property Mensaje As String
    End Class

    Public Class ResultadoLectura
        Public Property Catalogo As Catalogo
        Public Property Registros As List(Of RegistroCatalogo)
    End Class
End Namespace
