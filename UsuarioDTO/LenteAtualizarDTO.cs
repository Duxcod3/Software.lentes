using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lente.DTOs
{
    public class LenteAtualizarDTO
    {
        public int Id { get; set; }  // O Id é necessário para saber qual lente atualizar.
        public double Horizontal { get; set; }
        public double Vertical { get; set; }
        public double DiagonalMaior { get; set; }

        public double Ponte { get; set; } // Novo campo para ponte da lente

        public string Lado { get; set; } // "Esquerda" ou "Direita"
    }
}