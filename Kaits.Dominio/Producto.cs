using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Kaits.Dominio
{
    public class Producto:EntidadBase
    {
        [Display(Name = "Código Producto")]
        public int idProducto { get; set; }

        [Required]
        [Display(Name = "Descripción")]
        public string Descripcion { get; set; }
        public bool Estado { get; set; }

        [Display(Name = "Fecha auditoria")]
        public DateTime FechaAuditoria { get; set; }

        [Display(Name = "Usuario auditoria")]
        public string UsuarioAuditoria { get; set; }
    }
}
