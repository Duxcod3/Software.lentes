using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lente.Application.Exceptions
{
    public class LenteNotFoundException : Exception
    {
        public LenteNotFoundException()
           : base("Lente não encontrada.")
        {
        }

        public LenteNotFoundException(string message)
            : base(message)
        {
        }

        public LenteNotFoundException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        // Novo construtor que aceita ID como int
        public LenteNotFoundException(int id)
            : base($"Lente com ID {id} não encontrada.")
        {
        }
    }
}
    

