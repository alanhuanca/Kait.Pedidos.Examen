using Dapper;
using Kaits.Dominio;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using static Dapper.SqlMapper;

namespace Kaits.Infraestructura.Productos
{
    public class ProductoRepositorio : RepositorioBase, IProductoRepositorio
    {
        public async Task ActualizarAsync(Producto entity)
        {
            try
            {
                var query = $@"actualizarProducto_sp";

                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("idProducto", entity.idProducto);
                parameters.Add("Descripcion", entity.Descripcion);
                parameters.Add("Estado", entity.Estado);
                parameters.Add("FechaAuditoria", entity.FechaAuditoria);
                parameters.Add("UsuarioAuditoria", entity.UsuarioAuditoria);

                using (var connection = CreateConnection())
                {
                    await connection.ExecuteScalarAsync<int>(query, parameters, commandType: CommandType.StoredProcedure);
                     
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }

        public async Task<int> CrearAsync(Producto entity)
        {
            try
            {
                var query = $@"insertarProducto_sp";

                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("Descripcion", entity.Descripcion);
                parameters.Add("Estado", entity.Estado);
                parameters.Add("FechaAuditoria", entity.FechaAuditoria);
                parameters.Add("UsuarioAuditoria", entity.UsuarioAuditoria); 

                using (var connection = CreateConnection())
                {
                    var  idProducto =await connection.ExecuteScalarAsync<int>(query, parameters, commandType: CommandType.StoredProcedure);
                    return idProducto;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }

        public async Task EliminarAsync(Producto entity)
        {
            try
            {
                var query = $@"eliminarProducto_sp";

                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("idProducto", entity.idProducto); 

                using (var connection = CreateConnection())
                {
                    await connection.ExecuteScalarAsync<int>(query, parameters, commandType: CommandType.StoredProcedure);

                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }

        public async Task<List<Producto>> ListarTodosAsync()
        {
            try
            {
                var query = $@"listarProductos_sp";

                using (var connection = CreateConnection())
                {
                    var lista =await connection.QueryAsync<Producto>(query, commandType: CommandType.StoredProcedure);
                    return lista.ToList();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }
        
        public async Task<Producto> ObtenerPorCodigoAsync(int id)
        {
            try
            {
                var query = $@"obtenerProducto_sp";
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("idProducto", id);

                using (var connection = CreateConnection())
                {
                    var lista = await connection.QueryAsync<Producto>(query, parameters, commandType: CommandType.StoredProcedure);
                    return lista.First();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }

        
    }
}
