using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanCode.Application.DomainExceptions
{
    public class ValidationException : ApplicationException
    {
        public ValidationException(string mensage, string campo) : base(mensage)
        {
            Campo = campo;
        }
        public string Campo { get; }
    }
}
