using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Kaits.Dominio
{
    public class Pedido:EntidadBase
    {
        [Display(Name = "Número de Orden")]
        public int idPedido { get; set; }

        [Required]
        [Display(Name = "Fecha Orden")]
        public DateTime FechaOrden { get; set; }


        [Required]
        [Display(Name = "Cliente")]
        public int idCliente { get; set; }

        [Display(Name = "Precio Total")]
        public decimal PrecioTotal { get; set; }

        [Display(Name = "Fecha auditoria")]
        public DateTime FechaAuditoria { get; set; }

        [Display(Name = "Usuario auditoria")]
        public string UsuarioAuditoria { get; set; }
        public List<PedidoDetalle> detalle { get; set; }

        [Display(Name = "Cliente")]
        public string NombreApellido { get; set; }
    }

    public class PedidoDetalle
    {
        public int idPedidoDetalle { get; set; }
        public int idPedido { get; set; }

        [Display(Name = "Producto")]
        public int idProducto { get; set; }

        [Display(Name = "Descripción de Producto")]
        public string DescripcionProducto { get; set; }
        public decimal Cantidad { get; set; }

        [Display(Name = "Precio Unitario")]
        public decimal PrecioUnitario { get; set; }
        public decimal SubTotal { get; set; }
    }
}
