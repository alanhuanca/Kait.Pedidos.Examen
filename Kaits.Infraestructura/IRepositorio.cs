using Kaits.Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kaits.Infraestructura
{
    public interface IRepositorio<T> where T : EntidadBase
    {
        Task<List<T>> ListarTodosAsync();
        Task<T> ObtenerPorCodigoAsync(int id);
        Task<int> CrearAsync(T entity);
        Task ActualizarAsync(T entity);
        Task EliminarAsync(T entity);
    }
}
