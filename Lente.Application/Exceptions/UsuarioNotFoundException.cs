using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lente.Application.Exceptions
{
    
        public class UsuarioNotFoundException : Exception
        {
            public UsuarioNotFoundException()
                : base("Usuário não encontrado.")
            {
            }

            public UsuarioNotFoundException(string message)
                : base(message)
            {
            }

            public UsuarioNotFoundException(string message, Exception innerException)
                : base(message, innerException)
            {
            }

            //  Novo construtor que aceita ID como int
            public UsuarioNotFoundException(int id)
                : base($"Usuário com ID {id} não encontrado.")
            {
            }
        }
    }

