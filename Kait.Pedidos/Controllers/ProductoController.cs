using Kaits.Aplicacion.Productos;
using Kaits.Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Kait.Pedidos.Controllers
{
    public class ProductoController : Controller
    {
        private readonly IProductoServicio _productoServicio;
        public ProductoController(IProductoServicio productoServicio)
        {
            _productoServicio= productoServicio;
        }
         
        public async Task<ActionResult> Index()
        {
            var lista=await _productoServicio.ListarTodosAsync();
            return View(lista);
        }

        
        public async Task<ActionResult> Details(int? id)
        {

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Producto producto = await _productoServicio.ObtenerPorCodigoAsync(id.Value);
            if (producto == null)
            {
                return HttpNotFound();
            } 

            return View(producto); 
        }
         
        public ActionResult Create()
        { 
            return View();
        }
         
        [HttpPost]
        public async Task<ActionResult> Create([Bind(Include = "Descripcion,Estado")] Producto producto)
        {
            try
            { 
                if (ModelState.IsValid)
                {
                    producto.FechaAuditoria = DateTime.Now;
                    producto.UsuarioAuditoria = "userkaits"; 
                    var idProducto= await _productoServicio.CrearAsync(producto);
                    producto.idProducto=idProducto;
                    return RedirectToAction("Index");
                } 

                return View(producto);
            }
            catch
            {
                return View();
            }
        }
         
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Producto producto = await _productoServicio.ObtenerPorCodigoAsync(id.Value);
            if (producto == null)
            {
                return HttpNotFound();
            } 
            return View(producto); 
        }
         
        [HttpPost]
        public async Task<ActionResult> Edit([Bind(Include = "idProducto,Descripcion,Estado")] Producto producto)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    producto.FechaAuditoria=DateTime.Now;
                    producto.UsuarioAuditoria = "userKaits";

                    await _productoServicio.ActualizarAsync(producto);
                    return RedirectToAction("Index");
                }
                return View(producto); 
            }
            catch
            {
                return View();
            }
        }
         
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Producto producto = await _productoServicio.ObtenerPorCodigoAsync(id.Value);
            if (producto == null)
            {
                return HttpNotFound();
            } 
            return View(producto);
        }
         
        [HttpPost]
        public async Task<ActionResult> Delete(int id)
        {
            try
            { 
                await _productoServicio.EliminarAsync(new Producto { idProducto = id });
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
         
    }
}
