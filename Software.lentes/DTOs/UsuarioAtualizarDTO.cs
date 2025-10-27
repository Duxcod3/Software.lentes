namespace Software.lentes.DTOs
{
    public class UsuarioAtualizarDTO
    {
        public int Id { get; set; }
        public string NomeDeUsuario { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; }

        public string Perfil { get; set; }
    }
}
