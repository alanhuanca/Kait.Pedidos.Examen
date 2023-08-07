using Kaits.Dominio;
using Kaits.Infraestructura.Productos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kaits.Aplicacion.Productos
{
    public class ProductoServicio : IProductoServicio
    {
        private readonly IProductoRepositorio _productoRepositorio;

        public ProductoServicio(IProductoRepositorio productoRepositorio)
        {
            _productoRepositorio= productoRepositorio;
        }
        public async Task ActualizarAsync(Producto entity)
        {
           await _productoRepositorio.ActualizarAsync(entity);
        }

        public async Task<int> CrearAsync(Producto entity)
        {
            var idProducto= await _productoRepositorio.CrearAsync(entity);
            return idProducto;
        }

        public async Task EliminarAsync(Producto entity)
        {
            await _productoRepositorio.EliminarAsync(entity);
        }

        public async Task<List<Producto>> ListarTodosAsync()
        {
            return await _productoRepositorio.ListarTodosAsync();
        }

        public async Task<Producto> ObtenerPorCodigoAsync(int id)
        {
            return await _productoRepositorio.ObtenerPorCodigoAsync(id);
        }
    }
}
