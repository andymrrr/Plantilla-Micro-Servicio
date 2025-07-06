namespace PlantillaMicroServicio.Models
{
    public class Usuario
    {
        public int UsuarioID { get; set; }
        public string NombreUsuario { get; set; }
        public string Email { get; set; }
        public string Contraseña { get; set; }

        public string NombreCompleto { get; set; }
        public DateTime? FechaNacimiento { get; set; }
        public string? FotoPerfil { get; set; }
        public string? ImagenPortada { get; set; }
        public string? AcercaDe { get; set; }
        public string? FotoId { get; set; }
    }
}
