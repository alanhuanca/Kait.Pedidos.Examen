USE Kaits
GO

IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='Productos' and xtype='U')
BEGIN
	CREATE TABLE Productos(
		idProducto			INT IDENTITY(1,1) NOT NULL PRIMARY KEY, 
		Descripcion			VARCHAR(100) NOT NULL,
		Estado				BIT NOT NULL,
		FechaAuditoria		DATETIME NOT NULL,
		UsuarioAuditoria	VARCHAR(100) NOT NULL
	)
END
GO

IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='Clientes' and xtype='U')
BEGIN
	CREATE TABLE Clientes(
		idCliente			INT IDENTITY(1,1) NOT NULL PRIMARY KEY, 
		NombreApellido		VARCHAR(450) NOT NULL,
		DNI					VARCHAR(8) NOT NULL,
		Estado				BIT NOT NULL,
		FechaAuditoria		DATETIME NOT NULL,
		UsuarioAuditoria	VARCHAR(100) NOT NULL
	)
END
GO

IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='Pedidos' and xtype='U')
BEGIN
	CREATE TABLE Pedidos(
		idPedido			INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
		FechaOrden			DATE NOT NULL,
		idCliente			INT NOT NULL,
		PrecioTotal			DECIMAL(14,2) NOT NULL,
		FechaAuditoria		DATETIME NOT NULL,
		UsuarioAuditoria	VARCHAR(100) NOT NULL,
		FOREIGN KEY (idCliente) REFERENCES Clientes(idCliente),
	)
END
GO

IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='PedidosDetalle' and xtype='U')
BEGIN
	CREATE TABLE PedidosDetalle(
		idPedidoDetalle		INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
		idPedido			INT NOT NULL,
		idProducto			INT NOT NULL,
		DescripcionProducto VARCHAR(100) NOT NULL,
		Cantidad			DECIMAL(14,2) NOT NULL,
		PrecioUnitario		DECIMAL(14,2) NOT NULL,
		SubTotal			DECIMAL(14,2) NOT NULL,
		FOREIGN KEY (idProducto) REFERENCES Productos(idProducto),
		FOREIGN KEY (idPedido) REFERENCES Pedidos(idPedido),
	)
END
GO
