using Dapper;
using Kaits.Dominio;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text; 
using System.Threading.Tasks;

namespace Kaits.Infraestructura.Pedidos
{
    public class PedidoRepositorio : RepositorioBase, IPedidoRepositorio
    {
        public Task ActualizarAsync(Pedido entity)
        {
            throw new NotImplementedException();
        }

        public async Task<int> CrearAsync(Pedido entity)
        {
            try
            {
                var query = $@"insertarPedido_sp";

                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("FechaOrden", entity.FechaOrden);
                parameters.Add("idCliente", entity.idCliente);
                parameters.Add("PrecioTotal", entity.PrecioTotal);
                parameters.Add("FechaAuditoria", entity.FechaAuditoria);
                parameters.Add("UsuarioAuditoria", entity.UsuarioAuditoria);
                var detalle= JsonConvert.SerializeObject(entity.detalle);
                parameters.Add("detalle", detalle);

                using (var connection = CreateConnection())
                {
                    var idPedido = await connection.ExecuteScalarAsync<int>(query, parameters, commandType: CommandType.StoredProcedure);
                    return idPedido;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }

        public Task EliminarAsync(Pedido entity)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Pedido>> ListarTodosAsync()
        {
            try
            {
                var query = $@"listarPedidos_sp";

                using (var connection = CreateConnection())
                {
                    var lista = await connection.QueryAsync<Pedido>(query, commandType: CommandType.StoredProcedure);
                    return lista.ToList();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }

        public async Task<List<PedidoDetalle>> ObtenerDetallePedidoAsync(int id)
        {
            try
            {
                var query = $@"obtenerPedidoDetalle_sp";
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("idPedido", id);

                using (var connection = CreateConnection())
                {
                    var lista = await connection.QueryAsync<PedidoDetalle>(query, parameters, commandType: CommandType.StoredProcedure);
                    return lista.ToList();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }

        public async Task<Pedido> ObtenerPorCodigoAsync(int id)
        {
            try
            {
                var query = $@"obtenerPedido_sp";
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("idPedido", id);

                using (var connection = CreateConnection())
                {
                    var lista = await connection.QueryAsync<Pedido>(query, parameters, commandType: CommandType.StoredProcedure);
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
