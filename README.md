# Carga de Catálogos de Canales - CONTOSO

## 1. Descripción

Aplicación desarrollada para realizar la carga, validación, almacenamiento, control y exportación de catálogos de canales.

La solución permite procesar archivos de diferentes formatos, visualizar la información cargada, almacenarla en una base de datos MySQL, registrar las ejecuciones mediante una bitácora y generar reportes en formato Excel utilizando una plantilla previamente definida.

La aplicación fue desarrollada aplicando separación de responsabilidades, reutilización de código y una estructura orientada a facilitar el mantenimiento y evolución de la solución.

---

# 2. Objetivo

El objetivo de la solución es proporcionar una herramienta que permita gestionar la información correspondiente a los catálogos de canales de manera controlada.

Las principales funcionalidades son:

- Carga de archivos de catálogos.
- Validación de archivos.
- Validación de información.
- Visualización de registros.
- Carga de información hacia MySQL.
- Control de cargas.
- Registro de ejecuciones mediante bitácora.
- Generación de reportes Excel.
- Identificación del archivo de origen.
- Manejo de errores y mensajes al usuario.

---

# 3. Tecnologías utilizadas

La solución utiliza las siguientes tecnologías:

- C#
- Windows Forms
- .NET
- MySQL Community Server
- MySQL Workbench
- MySQL Connector
- ClosedXML
- Visual Studio
- Git / GitHub

La librería ClosedXML es utilizada para la generación y manipulación de archivos Excel.

---

# 4. Requisitos del entorno

Para ejecutar correctamente la solución se requiere:

- Sistema operativo Windows.
- Visual Studio.
- .NET compatible con la solución.
- MySQL Community Server.
- MySQL Workbench.
- MySQL Connector.
- Librería ClosedXML.

Se debe contar con acceso a un servidor MySQL y con los permisos necesarios para crear los esquemas y tablas utilizados por la aplicación.

---

# 5. Configuración de la base de datos

La solución utiliza dos esquemas principales dentro de MySQL: se encuentran ene el repositorio 

6. Configuración de archivos de entrada

Para realizar la carga de los catálogos, la aplicación utiliza una ruta específica en el equipo donde se ejecuta.

La carpeta destinada para almacenar los archivos de entrada es:

C:\subir_archivos\ C:\subir_archivos\cat_corresponsales.csv
C:\subir_archivos\cat_kioskos.txt

Para la generación del reporte Excel se requiere una plantilla previamente definida.

La plantilla debe tener exactamente el siguiente nombre:
PLANTILLA_REPORTE_CANALES.xlsx
