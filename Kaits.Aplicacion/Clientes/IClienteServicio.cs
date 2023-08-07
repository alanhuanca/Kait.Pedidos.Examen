using Kaits.Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kaits.Aplicacion.Clientes
{
    public interface IClienteServicio
    {
        Task<List<Cliente>> ListarTodosAsync();
        Task<Cliente> ObtenerPorCodigoAsync(int id);
        Task<int> CrearAsync(Cliente entity);
        Task ActualizarAsync(Cliente entity);
        Task EliminarAsync(Cliente entity);
    }
}
