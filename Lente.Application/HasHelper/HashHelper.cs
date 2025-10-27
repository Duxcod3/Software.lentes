using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lente.Application.HasHelper
{
    public class HashHelper
    {
            public static string GerarHashSenha(string senha)
        {
            using var sha256 = System.Security.Cryptography.SHA256.Create();
            //converte a senha em bytes
            var bytes = System.Text.Encoding.UTF8.GetBytes(senha);

            //Calcula o hash SHA256 do array de bytes da senha.
            var hash = sha256.ComputeHash(bytes);
            return Convert.ToBase64String(hash);
        }

        public static bool ValidarHash(string senha, string hashArmazenado)
        {
            var hashGerado = GerarHashSenha(senha);
            return hashGerado == hashArmazenado;
        }
    }
}


    

