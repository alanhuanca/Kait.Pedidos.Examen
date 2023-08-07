  USE Kaits
  GO

/*========== PRODUCTOS =====================*/

CREATE OR ALTER PROCEDURE listarProductos_sp
	
AS

BEGIN
	SELECT 
		idProducto,
		Descripcion,
		Estado,
		FechaAuditoria,
		UsuarioAuditoria
	FROM Productos 
END 
GO

CREATE OR ALTER PROCEDURE obtenerProducto_sp
	@idProducto INT
AS

BEGIN
	SELECT 
		idProducto,
		Descripcion,
		Estado,
		FechaAuditoria,
		UsuarioAuditoria
	FROM Productos 
	WHERE idProducto=@idProducto
END 
GO

CREATE OR ALTER PROCEDURE insertarProducto_sp
	@Descripcion		VARCHAR(100),
	@Estado				BIT,
	@FechaAuditoria		DATETIME,
	@UsuarioAuditoria	VARCHAR(100)
AS

BEGIN
	INSERT Productos (Descripcion,Estado,FechaAuditoria,UsuarioAuditoria)
	VALUES (@Descripcion,@Estado,@FechaAuditoria,@UsuarioAuditoria)

	DECLARE @idProducto INT=0
	SET @idProducto=@@IDENTITY

	SELECT @idProducto AS idProducto
END 
GO

CREATE OR ALTER PROCEDURE actualizarProducto_sp
	@idProducto			INT,
	@Descripcion		VARCHAR(100),
	@Estado				BIT,
	@FechaAuditoria		DATETIME,
	@UsuarioAuditoria	VARCHAR(100)
AS

BEGIN
	UPDATE Productos 
	SET Descripcion=@Descripcion,
		Estado=@Estado
	WHERE idProducto=@idProducto
END 
GO

CREATE OR ALTER PROCEDURE eliminarProducto_sp
	@idProducto			INT
AS

BEGIN
	UPDATE Productos 
	SET Estado=0
	WHERE idProducto=@idProducto
END 
GO



/*============= CLIENTES =======================*/

CREATE OR ALTER PROCEDURE listarClientes_sp
	
AS

BEGIN
	SELECT 
		idCliente,
		NombreApellido,
		DNI,
		Estado,
		FechaAuditoria,
		UsuarioAuditoria
	FROM Clientes 
END 
GO

CREATE OR ALTER PROCEDURE obtenerCliente_sp
	@idCiente INT
AS

BEGIN
	SELECT 
		idCliente,
		NombreApellido,
		DNI,
		Estado,
		FechaAuditoria,
		UsuarioAuditoria
	FROM Clientes 
	WHERE idCliente=@idCiente
END 
GO

CREATE OR ALTER PROCEDURE insertarCliente_sp
	@NombreApellido		VARCHAR(100),
	@DNI				VARCHAR(8),
	@Estado				BIT,
	@FechaAuditoria		DATETIME,
	@UsuarioAuditoria	VARCHAR(100)
AS

BEGIN
	INSERT Clientes (NombreApellido,DNI,Estado,FechaAuditoria,UsuarioAuditoria)
	VALUES (@NombreApellido,@DNI,@Estado,@FechaAuditoria,@UsuarioAuditoria)
END 
GO

CREATE OR ALTER PROCEDURE actualizarCliente_sp
	@idCliente			INT,
	@NombreApellido		VARCHAR(100),
	@DNI				VARCHAR(8),
	@Estado				BIT,
	@FechaAuditoria		DATETIME,
	@UsuarioAuditoria	VARCHAR(100)
AS

BEGIN
	UPDATE Clientes 
	SET NombreApellido=@NombreApellido,
		DNI=@DNI,
		Estado=@Estado
	WHERE idCliente=@idCliente
END 
GO

CREATE OR ALTER PROCEDURE eliminarCliente_sp
	@idCliente			INT
AS

BEGIN
	UPDATE Clientes 
	SET Estado=0
	WHERE idCliente=@idCliente
END 
GO


/*============= PEDIDOS =======================*/

CREATE OR ALTER PROCEDURE listarPedidos_sp
	
AS

BEGIN
	SELECT 
		A.idPedido,
		A.FechaOrden,
		A.idCliente,
		A.PrecioTotal,
		A.FechaAuditoria,
		A.UsuarioAuditoria,
		B.NombreApellido,
		B.DNI
	FROM Pedidos A
	INNER JOIN Clientes B
		ON A.idCliente=B.idCliente
END 
GO

CREATE OR ALTER PROCEDURE obtenerPedido_sp
	@idPedido INT
AS

BEGIN
	SELECT 
		A.idPedido,
		A.FechaOrden,
		A.idCliente,
		A.PrecioTotal,
		A.FechaAuditoria,
		A.UsuarioAuditoria,
		B.NombreApellido,
		B.DNI
	FROM Pedidos A
	INNER JOIN Clientes B
		ON A.idCliente=B.idCliente
	WHERE A.idPedido=@idPedido
END 
GO

CREATE OR ALTER PROCEDURE insertarPedido_sp
	@FechaOrden			DATE, 
	@idCliente			INT,
	@PrecioTotal		DECIMAL(14,2), 
	@FechaAuditoria		DATETIME,
	@UsuarioAuditoria	VARCHAR(100),
	@detalle			NVARCHAR(MAX)
AS

BEGIN
	DECLARE @tDetallePedido TABLE
	(	idProducto			INT NOT NULL,
		DescripcionProducto VARCHAR(100) NOT NULL,
		Cantidad			DECIMAL(14,2) NOT NULL,
		PrecioUnitario		DECIMAL(14,2) NOT NULL,
		SubTotal			DECIMAL(14,2) NOT NULL
	)

	INSERT INTO @tDetallePedido  
	 SELECT  
	   *  
	 FROM  
	   OPENJSON(@detalle) WITH (  
	   idProducto INT  'strict $.idProducto',
	   DescripcionProducto   VARCHAR(100)  'strict $.DescripcionProducto',
	   Cantidad   DECIMAL(14,2)  'strict $.Cantidad',
	   PrecioUnitario   DECIMAL(14,2) 'strict $.PrecioUnitario',
	   SubTotal    DECIMAL(14,2) 'strict $.SubTotal'  
	   ); 

	BEGIN TRAN

		INSERT Pedidos (FechaOrden,idCliente,PrecioTotal,FechaAuditoria,UsuarioAuditoria)
		VALUES (@FechaOrden,@idCliente,@PrecioTotal,@FechaAuditoria,@UsuarioAuditoria)
		IF @@ERROR>0
		BEGIN
			ROLLBACK TRAN
			RETURN
		END

		DECLARE @idPedido INT
		SET @idPedido= @@IDENTITY

		INSERT PedidosDetalle(
			idPedido,idProducto,DescripcionProducto,Cantidad,PrecioUnitario,SubTotal)
		SELECT 
			@idPedido,idProducto,DescripcionProducto,Cantidad,PrecioUnitario,SubTotal
		FROM @tDetallePedido	

		IF @@ERROR>0
		BEGIN
			ROLLBACK TRAN
			RETURN
		END

		SELECT @idPedido AS idPedido

	COMMIT TRAN
END 
GO

CREATE OR ALTER PROCEDURE actualizarPedido_sp
	@idPedido			INT,
	@FechaOrden			DATE, 
	@idCliente			INT,
	@PrecioTotal		DECIMAL(14,2), 
	@FechaAuditoria		DATETIME,
	@UsuarioAuditoria	VARCHAR(100),
	@detalle			NVARCHAR(MAX)
AS

BEGIN
	DECLARE @tDetallePedido TABLE
	(	idProducto			INT NOT NULL,
		DescripcionProducto VARCHAR(100) NOT NULL,
		Cantidad			DECIMAL(14,2) NOT NULL,
		PrecioUnitario		DECIMAL(14,2) NOT NULL,
		SubTotal			DECIMAL(14,2) NOT NULL
	)

	INSERT INTO @tDetallePedido  
	 SELECT  
	   *  
	 FROM  
	   OPENJSON(@detalle) WITH (  
	   idProducto INT  'strict $.idProducto',
	   DescripcionProducto   VARCHAR(100)  'strict $.DescripcionProducto',
	   Cantidad   DECIMAL(14,2)  'strict $.Cantidad',
	   PrecioUnitario   DECIMAL(14,2) 'strict $.PrecioUnitario',
	   SubTotal    DECIMAL(14,2) 'strict $.SubTotal'  
	   ); 

	BEGIN TRAN

		UPDATE Pedidos 
			SET FechaOrden=@FechaOrden,
				idCliente=@idCliente,
				PrecioTotal=@PrecioTotal
		WHERE idPedido=@idPedido
		IF @@ERROR>0
		BEGIN
			ROLLBACK TRAN
			RETURN
		END

		DELETE PedidosDetalle WHERE idPedido=@idPedido
		IF @@ERROR>0
		BEGIN
			ROLLBACK TRAN
			RETURN
		END

		INSERT PedidosDetalle(
			idPedido,idProducto,DescripcionProducto,Cantidad,PrecioUnitario,SubTotal)
		SELECT 
			@idPedido,idProducto,DescripcionProducto,Cantidad,PrecioUnitario,SubTotal
		FROM @tDetallePedido	

		IF @@ERROR>0
		BEGIN
			ROLLBACK TRAN
			RETURN
		END

		SELECT @idPedido AS idPedido

	COMMIT TRAN
END 
GO
 

 
CREATE OR ALTER PROCEDURE obtenerPedidoDetalle_sp
	@idPedido INT
AS

BEGIN
	SELECT 
		idPedidoDetalle,
		idPedido,
		idProducto,
		DescripcionProducto,
		Cantidad,
		PrecioUnitario,
		SubTotal
	FROM PedidosDetalle A 
	WHERE A.idPedido=@idPedido
END 
GO