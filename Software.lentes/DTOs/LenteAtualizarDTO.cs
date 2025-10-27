namespace Software.lentes.DTOs
{
    public class LenteAtualizarDTO
    {

        public int Id { get; set; }  // O Id é necessário para saber qual lente atualizar.
        public double Horizontal { get; set; }
        public double Vertical { get; set; }
        public double DiagonalMaior { get; set; }
    }
}
    

