using Kaits.Dominio;
using Kaits.Infraestructura.Clientes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kaits.Aplicacion.Clientes
{   
    public class ClienteServicio : IClienteServicio
    {
        private readonly IClienteRepositorio _clienteRepositorio;

        public ClienteServicio(IClienteRepositorio clienteRepositorio)
        {
            _clienteRepositorio= clienteRepositorio;
        }
        public async Task ActualizarAsync(Cliente entity)
        {
            await _clienteRepositorio.ActualizarAsync(entity);
        }

        public async Task<int> CrearAsync(Cliente entity)
        {
            var idCliente = await _clienteRepositorio.CrearAsync(entity);
            return idCliente;
        }

        public async Task EliminarAsync(Cliente entity)
        {
            await _clienteRepositorio.EliminarAsync(entity);
        }

        public async Task<List<Cliente>> ListarTodosAsync()
        {
            return await _clienteRepositorio.ListarTodosAsync();
        }

        public async Task<Cliente> ObtenerPorCodigoAsync(int id)
        {
            return await _clienteRepositorio.ObtenerPorCodigoAsync(id);
        }
    }
}
