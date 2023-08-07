using Kaits.Dominio;
using Kaits.Infraestructura.Pedidos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kaits.Aplicacion.Pedidos
{
    public class PedidoServicio : IPedidoServicio
    {
        private readonly IPedidoRepositorio _pedidoRepositorio;
        public PedidoServicio(IPedidoRepositorio pedidoRepositorio)
        {
            _pedidoRepositorio=pedidoRepositorio;
        }
        public Task ActualizarAsync(Pedido entity)
        {
            throw new NotImplementedException();
        }

        public async Task<int> CrearAsync(Pedido entity)
        {
             var idPedido= await _pedidoRepositorio.CrearAsync(entity);

            return idPedido;
        }

        public Task EliminarAsync(Pedido entity)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Pedido>> ListarTodosAsync()
        {
           return await _pedidoRepositorio.ListarTodosAsync();
        }

        public async Task<Pedido> ObtenerPorCodigoAsync(int id)
        {
            var pedido=await _pedidoRepositorio.ObtenerPorCodigoAsync(id);
            var pedidoDetalle = await _pedidoRepositorio.ObtenerDetallePedidoAsync(id);
            pedido.detalle=pedidoDetalle;
            return pedido;
        }
    }
}
