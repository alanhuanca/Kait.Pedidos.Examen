using Kaits.Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kaits.Infraestructura.Pedidos
{
    public interface IPedidoRepositorio : IRepositorio<Pedido>
    {
        Task<List<PedidoDetalle>> ObtenerDetallePedidoAsync(int id);
    }
}
