using Kaits.Aplicacion.Clientes;
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
    public class ClienteController : Controller
    {
        private readonly IClienteServicio _clienteServicio;
        public ClienteController(IClienteServicio clienteServicio)
        {
            _clienteServicio= clienteServicio;
        } 

        public async Task<ActionResult> Index()
        {
            var lista = await _clienteServicio.ListarTodosAsync();
            return View(lista);
        }
         
        public async Task<ActionResult> Details(int? id)
        {

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cliente cliente = await _clienteServicio.ObtenerPorCodigoAsync(id.Value);
            if (cliente == null)
            {
                return HttpNotFound();
            }
            return View(cliente);
        }
         
        public ActionResult Create()
        {
            return View();
        }
         
        [HttpPost]
        public async Task<ActionResult> Create([Bind(Include = "NombreApellido,DNI,Estado")] Cliente cliente)
        {
            try
            { 
                if (ModelState.IsValid)
                {
                    cliente.FechaAuditoria = DateTime.Now;
                    cliente.UsuarioAuditoria = "userkaits";
                    var idCliente = await _clienteServicio.CrearAsync(cliente);
                    cliente.idCliente = idCliente;
                    return RedirectToAction("Index");
                }                

                return View(cliente);
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
            Cliente cliente = await _clienteServicio.ObtenerPorCodigoAsync(id.Value);
            if (cliente == null)
            {
                return HttpNotFound();
            } 
            return View(cliente);
        }


        [HttpPost]
        public async Task<ActionResult> Edit([Bind(Include = "idCliente,NombreApellido,DNI,Estado")] Cliente cliente)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    cliente.FechaAuditoria = DateTime.Now;
                    cliente.UsuarioAuditoria = "userKaits";

                    await _clienteServicio.ActualizarAsync(cliente);
                    return RedirectToAction("Index");
                }
                return View(cliente);
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
            Cliente cliente = await _clienteServicio.ObtenerPorCodigoAsync(id.Value);
            if (cliente == null)
            {
                return HttpNotFound();
            } 
            return View(cliente);
        }
         
        [HttpPost]
        public async Task<ActionResult> Delete(int id)
        {
            try
            { 
                await _clienteServicio.EliminarAsync(new Cliente { idCliente = id });
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
