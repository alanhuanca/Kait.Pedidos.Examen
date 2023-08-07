using Dapper;
using Kaits.Dominio;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kaits.Infraestructura.Clientes
{
    public class ClienteRepositorio : RepositorioBase, IClienteRepositorio
    {
        public async Task ActualizarAsync(Cliente entity)
        {
            try
            {
                var query = $@"actualizarCliente_sp";

                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("idCliente", entity.idCliente);
                parameters.Add("NombreApellido", entity.NombreApellido);
                parameters.Add("DNI", entity.DNI);
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

        public async Task<int> CrearAsync(Cliente entity)
        {
            try
            {
                var query = $@"insertarCliente_sp";

                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("NombreApellido", entity.NombreApellido);
                parameters.Add("DNI", entity.DNI);
                parameters.Add("Estado", entity.Estado);
                parameters.Add("FechaAuditoria", entity.FechaAuditoria);
                parameters.Add("UsuarioAuditoria", entity.UsuarioAuditoria);

                using (var connection = CreateConnection())
                {
                    var idCliente = await connection.ExecuteScalarAsync<int>(query, parameters, commandType: CommandType.StoredProcedure);
                    return idCliente;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }

        public async Task EliminarAsync(Cliente entity)
        {
            try
            {
                var query = $@"eliminarCliente_sp";

                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("idCliente", entity.idCliente);

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

        public async Task<List<Cliente>> ListarTodosAsync()
        {
            try
            {
                var query = $@"listarClientes_sp";

                using (var connection = CreateConnection())
                {
                    var lista = await connection.QueryAsync<Cliente>(query, commandType: CommandType.StoredProcedure);
                    return lista.ToList();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }

        public async Task<Cliente> ObtenerPorCodigoAsync(int id)
        {
            try
            {
                var query = $@"obtenerCliente_sp";
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("idCiente", id);

                using (var connection = CreateConnection())
                {
                    var lista = await connection.QueryAsync<Cliente>(query, parameters, commandType: CommandType.StoredProcedure);
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
