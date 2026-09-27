Imports System.Configuration

Namespace Configuracion
    Public NotInheritable Class AppConfig
        Public Shared ReadOnly Property Servidor As String
            Get
                Return ConfigurationManager.AppSettings("Servidor")
            End Get
        End Property

        Public Shared ReadOnly Property Puerto As UInteger
            Get
                Return Convert.ToUInt32(ConfigurationManager.AppSettings("Puerto"))
            End Get
        End Property

        Public Shared ReadOnly Property Usuario As String
            Get
                Return ConfigurationManager.AppSettings("Usuario")
            End Get
        End Property

        Public Shared ReadOnly Property BaseDatos As String
            Get
                Return ConfigurationManager.AppSettings("BaseDatos")
            End Get
        End Property

        Public Shared ReadOnly Property RutaEntrada As String
            Get
                Return ConfigurationManager.AppSettings("RutaEntrada")
            End Get
        End Property

        Public Shared ReadOnly Property RutaPlantillas As String
            Get
                Return ConfigurationManager.AppSettings("RutaPlantillas")
            End Get
        End Property

        Public Shared ReadOnly Property RutaReportes As String
            Get
                Return ConfigurationManager.AppSettings("RutaReportes")
            End Get
        End Property
    End Class
End Namespace
