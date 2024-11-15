/****** Crear la Base de Datos ******/
CREATE DATABASE [jurassic_pharm]
GO

USE [jurassic_pharm];
GO

/****** Tablas ******/
-- Tabla OBRAS_SOCIALES
CREATE TABLE [dbo].[OBRAS_SOCIALES](
    [id_obra_social] INT IDENTITY(1,1) PRIMARY KEY,
    [nombre] VARCHAR(50),
    [id_ciudad] INT
);
GO

-- Tabla RECETAS
CREATE TABLE [dbo].[RECETAS](
    [id_receta] INT IDENTITY(1,1) PRIMARY KEY,
    [id_obra_social] INT,
    [matricula] INT,
    [fecha_vencimiento] DATE,
    [tipo_receta] VARCHAR(50)
);
GO

-- Tabla CLIENTES
CREATE TABLE [dbo].[CLIENTES](
    [id_cliente] INT IDENTITY(1,1) PRIMARY KEY,
    [id_obra_social] INT,
    [id_ciudad] INT,
    [nombre] VARCHAR(50),
    [apellido] VARCHAR(50),
    [correo_electronico] VARCHAR(80),
    [calle] VARCHAR(50),
    [altura] INT
);
GO

-- Tabla TIPOS_SUMINISTRO
CREATE TABLE [dbo].[TIPOS_SUMINISTRO](
    [id_tipo_suministro] INT IDENTITY(1,1) PRIMARY KEY,
    [nombre] VARCHAR(50)
);
GO

-- Tabla SUMINISTROS
CREATE TABLE [dbo].[SUMINISTROS](
    [id_suministro] INT IDENTITY(1,1) PRIMARY KEY,
    [nombre] VARCHAR(50),
    [pre_unitario] INT,
    [id_tipo_suministro] INT,
    [id_tipo_distribucion] INT,
    [id_marca] INT,
    [stock] INT,
    [stock_minimo] INT
);
GO

-- Tabla FACTURAS
CREATE TABLE [dbo].[FACTURAS](
    [nro_factura] INT IDENTITY(1,1) PRIMARY KEY,
    [id_cliente] INT,
    [id_sucursal] INT,
    [fecha] DATE
);
GO

-- Tabla DETALLES_FACTURA
CREATE TABLE [dbo].[DETALLES_FACTURA](
    [id_detalle_factura] INT IDENTITY(1,1) PRIMARY KEY,
    [nro_factura] INT,
    [id_suministro] INT,
    [pre_venta] INT,
    [cantidad] INT
);
GO

-- Tabla CIUDADES
CREATE TABLE [dbo].[CIUDADES](
    [id_ciudad] INT IDENTITY(1,1) PRIMARY KEY,
    [nombre] VARCHAR(50),
    [id_localidad] INT
);
GO

-- Tabla DETALLES_RECETA
CREATE TABLE [dbo].[DETALLES_RECETA](
    [id_detalle_receta] INT IDENTITY(1,1) PRIMARY KEY,
    [id_receta] INT,
    [id_suministro] INT,
    [descripcion] VARCHAR(80),
    [cantidad] INT
);
GO

-- Tabla EMPLEADOS
CREATE TABLE [dbo].[EMPLEADOS](
    [legajo_empleado] INT IDENTITY(1,1) PRIMARY KEY,
    [id_sucursal] INT,
    [id_ciudad] INT,
    [nombre] VARCHAR(50),
    [apellido] VARCHAR(50),
    [calle] VARCHAR(50),
    [altura] INT,
    [correo_electronico] VARCHAR(80),
    [Active] BIT DEFAULT 1,
    [rol] VARCHAR(50),
    [password_empleado] VARCHAR(64)
);
GO

-- Tabla LOCALIDADES
CREATE TABLE [dbo].[LOCALIDADES](
    [id_localidad] INT IDENTITY(1,1) PRIMARY KEY,
    [nombre] VARCHAR(50),
    [id_provincia] INT
);
GO

-- Tabla MARCAS
CREATE TABLE [dbo].[MARCAS](
    [id_marca] INT IDENTITY(1,1) PRIMARY KEY,
    [nombre] VARCHAR(50)
);
GO

-- Tabla MEDICOS
CREATE TABLE [dbo].[MEDICOS](
    [matricula] INT PRIMARY KEY,
    [nombre] VARCHAR(50),
    [apellido] VARCHAR(50),
    [correo_electronico] VARCHAR(80)
);
GO


CREATE VIEW [dbo].[VIEW_FACTURACION_POR_ANIO]
AS
SELECT
    S.nombre AS 'Suministro',
    YEAR(F.fecha) AS 'Año',
    SUM(DF.pre_venta * DF.cantidad) AS 'Total Facturado'
FROM DETALLES_FACTURA DF
JOIN SUMINISTROS S ON DF.id_suministro = S.id_suministro
JOIN FACTURAS F ON DF.nro_factura = F.nro_factura
GROUP BY S.nombre,  YEAR(F.fecha)
GO
-- Tabla PROVEEDORES
CREATE TABLE [dbo].[PROVEEDORES](
    [id_proveedor] INT IDENTITY(1,1) PRIMARY KEY,
    [id_ciudad] INT,
    [razon_social] VARCHAR(50)
);
GO

-- Tabla PROVINCIAS
CREATE TABLE [dbo].[PROVINCIAS](
    [id_provincia] INT IDENTITY(1,1) PRIMARY KEY,
    [nombre] VARCHAR(50)
);
GO

-- Tabla STOCKS
CREATE TABLE [dbo].[STOCKS](
    [id_stock] INT IDENTITY(1,1) PRIMARY KEY,
    [id_sucursal] INT,
    [id_proveedor] INT,
    [legajo_empleado] INT,
    [cantidad] INT,
    [fecha] DATE
);
GO

-- Tabla SUCURSALES
CREATE TABLE [dbo].[SUCURSALES](
    [id_sucursal] INT IDENTITY(1,1) PRIMARY KEY,
    [id_ciudad] INT,
    [calle] VARCHAR(50),
    [altura] INT
);
GO

-- Tabla TIPOS_DISTRIBUCION
CREATE TABLE [dbo].[TIPOS_DISTRIBUCION](
    [id_tipo_distribucion] INT IDENTITY(1,1) PRIMARY KEY,
    [descripcion] VARCHAR(50)
);
GO

INSERT INTO [dbo].[PROVINCIAS] (nombre) VALUES
('Buenos Aires'),
('Córdoba'),
('Santa Fe'),
('Mendoza'),
('Salta');

-- Insertar datos en LOCALIDADES
INSERT INTO [dbo].[LOCALIDADES] (nombre, id_provincia) VALUES
('La Plata', 1),
('Córdoba Capital', 2),
('Rosario', 3),
('Mendoza Capital', 4),
('Salta Capital', 5);

-- Insertar datos en CIUDADES
INSERT INTO [dbo].[CIUDADES] (nombre, id_localidad) VALUES
('La Plata', 1),
('Córdoba', 2),
('Rosario', 3),
('Mendoza', 4),
('Salta', 5);

-- Insertar datos en TIPOS_DISTRIBUCION
INSERT INTO [dbo].[TIPOS_DISTRIBUCION] (descripcion) VALUES
('Venta en mostrador'),
('Entrega a domicilio'),
('Venta online'),
('Venta mayorista'),
('Venta minorista');

-- Insertar datos en TIPOS_SUMINISTRO
INSERT INTO [dbo].[TIPOS_SUMINISTRO] (nombre) VALUES
('Medicamentos'),
('Suplementos'),
('Equipos médicos'),
('Productos de higiene'),
('Cosméticos');

-- Insertar datos en MARCAS
INSERT INTO [dbo].[MARCAS] (nombre) VALUES
('Laboratorios Bagó'),
('Laboratorios Roemmers'),
('Laboratorios Elea'),
('Laboratorios Richmond'),
('Laboratorios Raffo');

-- Insertar datos en OBRAS_SOCIALES
INSERT INTO [dbo].[OBRAS_SOCIALES] (nombre, id_ciudad) VALUES
('OSDE', 1),
('Swiss Medical', 2),
('Galeno', 3),
('Medifé', 4),
('Omint', 5);

-- Insertar datos en SUCURSALES
INSERT INTO [dbo].[SUCURSALES] (id_ciudad, calle, altura) VALUES
(1, 'Calle 50', 123),
(2, 'Avenida Colón', 456),
(3, 'Bulevar Oroño', 789),
(4, 'Avenida San Martín', 101),
(5, 'Calle Balcarce', 202);

-- Insertar datos en PROVEEDORES
INSERT INTO [dbo].[PROVEEDORES] (id_ciudad, razon_social) VALUES
(1, 'Distribuidora Farmacéutica La Plata S.A.'),
(2, 'Córdoba Medicamentos S.R.L.'),
(3, 'Rosario Salud S.A.'),
(4, 'Mendoza Farma S.A.'),
(5, 'Salta Distribuciones S.R.L.');

-- Insertar datos en MEDICOS
INSERT INTO [dbo].[MEDICOS] (matricula, nombre, apellido, correo_electronico) VALUES
(12345, 'Juan', 'Pérez', 'juan.perez@ejemplo.com'),
(23456, 'María', 'González', 'maria.gonzalez@ejemplo.com'),
(34567, 'Carlos', 'López', 'carlos.lopez@ejemplo.com'),
(45678, 'Ana', 'Martínez', 'ana.martinez@ejemplo.com'),
(56789, 'Luis', 'Fernández', 'luis.fernandez@ejemplo.com');

-- Insertar datos en CLIENTES
INSERT INTO [dbo].[CLIENTES] (id_obra_social, id_ciudad, nombre, apellido, correo_electronico, calle, altura) VALUES
(1, 1, 'Pedro', 'Gómez', 'pedro.gomez@ejemplo.com', 'Calle 60', 150),
(2, 2, 'Lucía', 'Ramírez', 'lucia.ramirez@ejemplo.com', 'Avenida General Paz', 250),
(3, 3, 'Martín', 'Sánchez', 'martin.sanchez@ejemplo.com', 'Calle Córdoba', 350),
(4, 4, 'Sofía', 'Díaz', 'sofia.diaz@ejemplo.com', 'Avenida Las Heras', 450),
(5, 5, 'Javier', 'Morales', 'javier.morales@ejemplo.com', 'Calle Mitre', 550);

-- Insertar datos en EMPLEADOS
INSERT INTO [dbo].[EMPLEADOS] (id_sucursal, id_ciudad, nombre, apellido, calle, altura, correo_electronico, rol, password_empleado) VALUES
(1, 1, 'Laura', 'Castro', 'Calle 70', 160, 'laura.castro@ejemplo.com', 'CAJERO', 'password1'),
(2, 2, 'Diego', 'Rojas', 'Avenida Vélez Sarsfield', 260, 'diego.rojas@ejemplo.com', 'CAJERO', 'password2'),
(3, 3, 'Carolina', 'Méndez', 'Calle San Juan', 360, 'carolina.mendez@ejemplo.com', 'REPOSITOR', 'password3'),
(4, 4, 'Fernando', 'Herrera', 'Avenida Belgrano', 460, 'fernando.herrera@ejemplo.com', 'REPOSITOR', 'password4'),
(5, 5, 'Natalia', 'Ruiz', 'Calle Güemes', 560, 'natalia.ruiz@ejemplo.com', 'CAJERO', 'password5'),
(5, 5, 'Julian', 'Rinaudo', 'Sarmiento', 922, 'julianrinaudo18@gmail.com', 'ADMIN', 'password5');

-- Insertar datos en SUMINISTROS
INSERT INTO [dbo].[SUMINISTROS] (nombre, pre_unitario, id_tipo_suministro, id_tipo_distribucion, id_marca, stock, stock_minimo) VALUES
('Ibuprofeno 600mg', 500, 1, 1, 1, 100, 10),
('Paracetamol 500mg', 300, 1, 2, 2, 200, 20),
('Termómetro Digital', 1500, 3, 3, 3, 50, 5),
('Alcohol en Gel 500ml', 700, 4, 4, 4, 150, 15),
('Crema Hidratante 200ml', 1200, 5, 5, 5, 80, 8);

-- Insertar datos en RECETAS (continuación)
INSERT INTO [dbo].[RECETAS] (id_obra_social, matricula, fecha_vencimiento, tipo_receta) VALUES
(3, 34567, '2024-12-31', 'Control'),
(4, 45678, '2024-12-31', 'Preventiva'),
(5, 56789, '2024-12-31', 'Paliativa');

-- Insertar datos en DETALLES_RECETA
INSERT INTO [dbo].[DETALLES_RECETA] (id_receta, id_suministro, descripcion, cantidad) VALUES
(1, 1, 'Tomar cada 8 horas', 20),
(2, 2, 'Tomar cada 6 horas', 15),
(3, 3, 'Usar una vez al día', 5),
(4, 4, 'Aplicar en manos', 10),
(5, 5, 'Aplicar en la piel', 8);

-- Insertar datos en FACTURAS
INSERT INTO [dbo].[FACTURAS] (id_cliente, id_sucursal, fecha) VALUES
(1, 1, '2024-11-05'),
(2, 2, '2024-11-05'),
(3, 3, '2024-11-05'),
(4, 4, '2024-11-05'),
(5, 5, '2023-11-05'),
(5, 5, '2023-11-05'),
(5, 5, '2023-11-05'),
(5, 2, '2023-11-05'),
(2, 2, '2023-11-05'),
(2, 1, '2023-11-05'),
(2, 1, '2022-11-05'),
(3, 1, '2022-11-05'),
(3, 3, '2022-11-05'),
(4, 4, '2022-11-05'),
(1, 3, '2022-11-05');

-- Insertar datos en DETALLES_FACTURA
INSERT INTO [dbo].[DETALLES_FACTURA] (nro_factura, id_suministro, pre_venta, cantidad) VALUES
(1, 1, 550, 2),
(2, 2, 330, 3),
(3, 3, 1500, 1),
(4, 4, 700, 2),
(5, 5, 1200, 1),
(6, 2, 1200, 1),
(7, 2, 1200, 1),
(8, 2, 1200, 1),
(9, 3, 1200, 1),
(10, 4, 1200, 1),
(11, 3, 1200, 1),
(12, 1, 1200, 1),
(13, 1, 1200, 1),
(14, 1, 1200, 1),
(15, 2, 1200, 1);

-- Insertar datos en STOCKS
INSERT INTO [dbo].[STOCKS] (id_sucursal, id_proveedor, legajo_empleado, cantidad, fecha) VALUES
(1, 1, 1, 50, '2024-11-01'),
(2, 2, 2, 30, '2024-11-01'),
(3, 3, 3, 20, '2024-11-01'),
(4, 4, 4, 40, '2024-11-01'),
(5, 5, 5, 25, '2024-11-01');
