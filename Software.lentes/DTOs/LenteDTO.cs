namespace Software.lentes.DTOs
{
    public class LenteDTO
    {
        public int Id { get; set; }
        public double Horizontal { get; set; }
        public double Vertical { get; set; }
        public double DiagonalMaior { get; set; }
        public int UsuarioId { get; set; }
    }
}