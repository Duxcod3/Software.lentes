using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lente.Domain.Entities
{
    public class LenteModelo
    {
        public int Id { get; set; }
        public double Horizontal { get; set; }  // largura da lente em mm
        public double Vertical { get; set; }    // altura da lente em mm
        public double Diagonal { get; set; }    // diagonal maior da lente em mm


        public int UsuarioId { get; set; }
        public Usuario Usuario { get; set; }



    }
}
