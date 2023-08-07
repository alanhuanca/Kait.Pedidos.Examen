using Kaits.Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kaits.Aplicacion.Pedidos
{
    public interface IPedidoServicio
    { 
        Task<List<Pedido>> ListarTodosAsync();
        Task<Pedido> ObtenerPorCodigoAsync(int id);
        Task<int> CrearAsync(Pedido entity);
        Task ActualizarAsync(Pedido entity);
        Task EliminarAsync(Pedido entity);
    }
}
