using System;

namespace Lente.DTOs
{
    public class LenteAtualizarDTO
    {
        public int Id { get; set; }  // O Id é necessário para saber qual lente atualizar.
        public double Horizontal { get; set; }
        public double Vertical { get; set; }
        public double? DiagonalMaior { get; set; }  // Nullable, conforme você já colocou

        public double Ponte { get; set; } // Novo campo para ponte da lente

        public string Lado { get; set; } // "Esquerda" ou "Direita"

        public string Job { get; set; } // Novo campo para número de job / OS
    }
}
