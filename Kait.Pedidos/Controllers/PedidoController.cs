using Kaits.Aplicacion.Pedidos;
using Kaits.Aplicacion.Productos;
using Kaits.Dominio;
using Kaits.Infraestructura.Clientes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Kait.Pedidos.Controllers
{
    public class PedidoController : Controller
    {
        private readonly IPedidoServicio _pedidoServicio;
        private readonly IClienteRepositorio _clienteRepositorio;
        private readonly IProductoServicio _productoServicio;
        public PedidoController(
            IPedidoServicio pedidoServicio, 
            IClienteRepositorio clienteRepositorio,
            IProductoServicio productoServicio)
        {
            _pedidoServicio=pedidoServicio;
            _clienteRepositorio=clienteRepositorio;
            _productoServicio=productoServicio;
        }
         
        public async Task<ActionResult> Index()
        {
            var lista = await _pedidoServicio.ListarTodosAsync();
            return View(lista);
        } 

        public async Task<ActionResult> Details(int? id)
        {

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Pedido pedido = await _pedidoServicio.ObtenerPorCodigoAsync(id.Value);
            if (pedido == null)
            {
                return HttpNotFound();
            }
            var listaCliente = await _clienteRepositorio.ListarTodosAsync();
            ViewBag.idCliente = new SelectList(listaCliente, "idCliente", "NombreApellido", pedido.idCliente);

            return View(pedido);
        }

        public async Task<ActionResult> Create()
        {
            var listaCliente = await _clienteRepositorio.ListarTodosAsync();
            ViewBag.idCliente = new SelectList(listaCliente, "idCliente", "NombreApellido");

            var listaProducto= await _productoServicio.ListarTodosAsync();
            ViewBag.idProducto = new SelectList(listaProducto, "idProducto", "Descripcion");
            return View();
        }


        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
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
            Pedido pedido = await _pedidoServicio.ObtenerPorCodigoAsync(id.Value);
            if (pedido == null)
            {
                return HttpNotFound();
            }
            var listaCliente = await _clienteRepositorio.ListarTodosAsync();
            ViewBag.idCliente = new SelectList(listaCliente, "idCliente", "NombreApellido", pedido.idCliente);

            return View(pedido);
        }
 
        [HttpPost]
        public async Task<ActionResult> Edit(int id, FormCollection collection)
        {
            try
            {
                var listaCliente = await _clienteRepositorio.ListarTodosAsync();
                ViewBag.idCliente = new SelectList(listaCliente, "idCliente", "NombreApellido" );


                return RedirectToAction("Index");
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
            Pedido pedido = await _pedidoServicio.ObtenerPorCodigoAsync(id.Value);
            if (pedido == null)
            {
                return HttpNotFound();
            }
            var listaCliente = await _clienteRepositorio.ListarTodosAsync();
            ViewBag.idCliente = new SelectList(listaCliente, "idCliente", "NombreApellido", pedido.idCliente);

            return View(pedido);
        }
 
        [HttpPost]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                // TODO: Add delete logic here 

                await _pedidoServicio.EliminarAsync(new Pedido { idPedido = id });
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        public async Task<ActionResult> SaveOrder(
            int? idCliente,  
            DateTime? fechaOrden, 
            PedidoDetalle[] detalle)
        {
            string result = "Error";
            if (idCliente != null  && detalle != null)
            {
                #region Agregar pedido

                Pedido pedido = new Pedido();
                pedido.idCliente = idCliente.Value;
                pedido.detalle = detalle.ToList();
                pedido.FechaOrden = fechaOrden.Value;
                pedido.FechaAuditoria=DateTime.Now;
                pedido.UsuarioAuditoria = "userKaits";
                pedido.PrecioTotal = detalle.Sum(x => x.SubTotal); 

                await _pedidoServicio.CrearAsync(pedido);
                 
                #endregion
                  

                result = pedido.idPedido.ToString();
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }
    }
}
