-- ============================================================================
-- Prueba tecnica Analista I - Carga de Catalogos de Canales CONTOSO
-- Motor: MySQL 8.4 LTS
-- Compatible con: DBeaver, MySQL Workbench y cliente mysql
-- Version: V1
--
-- Este script es idempotente: puede ejecutarse mas de una vez sin eliminar
-- catalogos ni bitacoras existentes. No crea usuarios ni modifica privilegios.
-- ============================================================================

CREATE SCHEMA IF NOT EXISTS catalogos
    DEFAULT CHARACTER SET utf8mb4
    DEFAULT COLLATE utf8mb4_unicode_ci;

CREATE SCHEMA IF NOT EXISTS bitacora
    DEFAULT CHARACTER SET utf8mb4
    DEFAULT COLLATE utf8mb4_unicode_ci;

-- ============================================================================
-- Esquema catalogos
-- ============================================================================

USE catalogos;

CREATE TABLE IF NOT EXISTS catalogos_edenilson_guevara_cat_agencias (
    id BIGINT NOT NULL AUTO_INCREMENT COMMENT 'Identificador unico del registro',
    codigo VARCHAR(50) NOT NULL COMMENT 'Codigo de la agencia',
    nombre VARCHAR(150) NOT NULL COMMENT 'Nombre de la agencia',
    departamento VARCHAR(100) NULL COMMENT 'Departamento',
    municipio VARCHAR(100) NULL COMMENT 'Municipio',
    direccion VARCHAR(500) NULL COMMENT 'Direccion de la agencia',
    fecha_proceso TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP COMMENT 'Fecha de proceso de la carga',
    PRIMARY KEY (id),
    INDEX idx_agencias_codigo (codigo),
    INDEX idx_agencias_fecha_proceso (fecha_proceso)
) ENGINE = InnoDB COMMENT = 'Catalogo de agencias';

CREATE TABLE IF NOT EXISTS catalogos_edenilson_guevara_cat_atms (
    id BIGINT NOT NULL AUTO_INCREMENT COMMENT 'Identificador unico del registro',
    codigo VARCHAR(50) NOT NULL COMMENT 'Codigo del ATM',
    nombre VARCHAR(150) NULL COMMENT 'Nombre o descripcion del ATM',
    departamento VARCHAR(100) NULL COMMENT 'Departamento',
    municipio VARCHAR(100) NULL COMMENT 'Municipio',
    direccion VARCHAR(500) NULL COMMENT 'Direccion del ATM',
    fecha_proceso TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP COMMENT 'Fecha de proceso de la carga',
    PRIMARY KEY (id),
    INDEX idx_atms_codigo (codigo),
    INDEX idx_atms_fecha_proceso (fecha_proceso)
) ENGINE = InnoDB COMMENT = 'Catalogo de cajeros automaticos';

CREATE TABLE IF NOT EXISTS catalogos_edenilson_guevara_cat_corresponsales (
    id BIGINT NOT NULL AUTO_INCREMENT COMMENT 'Identificador unico del registro',
    codigo VARCHAR(50) NOT NULL COMMENT 'Codigo del corresponsal',
    nombre VARCHAR(150) NOT NULL COMMENT 'Nombre del corresponsal',
    departamento VARCHAR(100) NULL COMMENT 'Departamento',
    municipio VARCHAR(100) NULL COMMENT 'Municipio',
    direccion VARCHAR(500) NULL COMMENT 'Direccion del corresponsal',
    fecha_proceso TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP COMMENT 'Fecha de proceso de la carga',
    PRIMARY KEY (id),
    INDEX idx_corresponsales_codigo (codigo),
    INDEX idx_corresponsales_fecha_proceso (fecha_proceso)
) ENGINE = InnoDB COMMENT = 'Catalogo de corresponsales';

CREATE TABLE IF NOT EXISTS catalogos_edenilson_guevara_cat_kioskos (
    id BIGINT NOT NULL AUTO_INCREMENT COMMENT 'Identificador unico del registro',
    codigo VARCHAR(50) NOT NULL COMMENT 'Codigo del kiosko',
    nombre VARCHAR(150) NULL COMMENT 'Nombre o descripcion del kiosko',
    departamento VARCHAR(100) NULL COMMENT 'Departamento',
    municipio VARCHAR(100) NULL COMMENT 'Municipio',
    direccion VARCHAR(500) NULL COMMENT 'Direccion del kiosko',
    fecha_proceso TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP COMMENT 'Fecha de proceso de la carga',
    PRIMARY KEY (id),
    INDEX idx_kioskos_codigo (codigo),
    INDEX idx_kioskos_fecha_proceso (fecha_proceso)
) ENGINE = InnoDB COMMENT = 'Catalogo de kioskos';

-- ============================================================================
-- Esquema bitacora
-- ============================================================================

USE bitacora;

CREATE TABLE IF NOT EXISTS bitacora_edenilson_guevara_log_ejecucion (
    id_ejecucion BIGINT NOT NULL AUTO_INCREMENT COMMENT 'Identificador de la ejecucion',
    nombre_archivo VARCHAR(255) NOT NULL COMMENT 'Nombre del archivo procesado',
    tipo_archivo VARCHAR(20) NOT NULL COMMENT 'Tipo de archivo: CSV, XLSX, TXT o MANUAL',
    tabla_destino VARCHAR(255) NULL COMMENT 'Tabla catalogo afectada',
    fecha_inicio TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP COMMENT 'Inicio de la ejecucion',
    fecha_fin TIMESTAMP NULL COMMENT 'Fin de la ejecucion',
    cantidad_registros INT NOT NULL DEFAULT 0 COMMENT 'Cantidad de registros procesados',
    estado VARCHAR(20) NOT NULL COMMENT 'EN_PROCESO, COMPLETADO o ERROR',
    mensaje VARCHAR(1000) NULL COMMENT 'Detalle funcional o tecnico de la ejecucion',
    PRIMARY KEY (id_ejecucion),
    INDEX idx_log_ejecucion_fecha_inicio (fecha_inicio),
    INDEX idx_log_ejecucion_estado (estado)
) ENGINE = InnoDB COMMENT = 'Bitacora de ejecuciones de carga';

CREATE TABLE IF NOT EXISTS bitacora_edenilson_guevara_control_cargas_diarias (
    id_control BIGINT NOT NULL AUTO_INCREMENT COMMENT 'Identificador del control',
    nombre_archivo VARCHAR(255) NOT NULL COMMENT 'Nombre del archivo procesado',
    tabla_destino VARCHAR(255) NOT NULL COMMENT 'Tabla catalogo afectada',
    fecha_proceso DATE NOT NULL COMMENT 'Ultimo dia del mes anterior',
    fecha_carga TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP COMMENT 'Fecha y hora de la carga',
    cantidad_registros INT NOT NULL DEFAULT 0 COMMENT 'Cantidad de registros cargados',
    estado VARCHAR(20) NOT NULL COMMENT 'Estado final de la carga',
    mensaje VARCHAR(1000) NULL COMMENT 'Mensaje adicional',
    PRIMARY KEY (id_control),
    INDEX idx_control_cargas_fecha_proceso (fecha_proceso),
    INDEX idx_control_cargas_tabla_destino (tabla_destino),
    INDEX idx_control_cargas_estado (estado)
) ENGINE = InnoDB COMMENT = 'Control diario de cargas de catalogos';

-- ============================================================================
-- Verificacion posterior a la ejecucion
-- ============================================================================

SELECT schema_name AS esquema
FROM information_schema.schemata
WHERE schema_name IN ('catalogos', 'bitacora')
ORDER BY schema_name;

SELECT table_schema AS esquema, table_name AS tabla
FROM information_schema.tables
WHERE table_schema IN ('catalogos', 'bitacora')
ORDER BY table_schema, table_name;


select * from  catalogos_edenilson_guevara_cat_atms cegca 
