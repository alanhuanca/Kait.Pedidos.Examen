using Kaits.Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kaits.Aplicacion.Productos
{
    public interface IProductoServicio
    {
        Task<List<Producto>> ListarTodosAsync();
        Task<Producto> ObtenerPorCodigoAsync(int id);
        Task<int> CrearAsync(Producto entity);
        Task ActualizarAsync(Producto entity);
        Task EliminarAsync(Producto entity);
    }
}
