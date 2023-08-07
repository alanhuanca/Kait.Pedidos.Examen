using FluentValidation;
using Kaits.Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Kait.Pedidos.Validation
{
    public class ClienteValidator : AbstractValidator<Cliente>
    {
        public ClienteValidator()
        {
            RuleFor(x => x.DNI).NotEmpty().MinimumLength(8).MaximumLength(8).WithMessage("*Validación de DNI");
            RuleFor(x => x.NombreApellido).NotEmpty().MinimumLength(5).WithMessage("*Validaicón nombres de cliente").Length(6, 10);            
        }
    }
}