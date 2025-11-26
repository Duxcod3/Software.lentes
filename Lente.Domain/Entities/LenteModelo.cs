using System;

namespace Lente.Domain.Entities
{
    public class LenteModelo
    {
        public int Id { get; set; }

        public double Horizontal { get; set; }  // Largura da lente em mm
        public double Vertical { get; set; }    // Altura da lente em mm

        // ❌ A diagonal maior agora é opcional — depende da nuvem de pontos no RXUniverse
        public double? DiagonalMaior { get; set; }   // Deixa opcional (nullable)

        public double Ponte { get; set; }       // Ponte da lente (mm)

        public string Lado { get; set; }        // "Esquerda" ou "Direita"

        //  Novo campo para número de Job (OS)
        public string Job { get; set; }          // Ex: "OS12345"

        // Relacionamento com o usuário
        public int UsuarioId { get; set; }
        public Usuario Usuario { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow; // Data de criação
    }
}
