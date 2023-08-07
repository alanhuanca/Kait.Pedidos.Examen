using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Kaits.Dominio
{
    public class Cliente:EntidadBase
    {
        [Display(Name = "Código Cliente")]
        public int idCliente { get; set; }

        [Required]
        [Display(Name = "Nombres y Apellidos")]
        public string NombreApellido { get; set; }

        [Required]
        public string DNI { get; set; }

        public bool Estado { get; set; }

        [Display(Name = "Fecha auditoria")]
        public DateTime FechaAuditoria { get; set; }

        [Display(Name = "Usuario auditoria")]
        public string UsuarioAuditoria { get; set; }
    }
}
