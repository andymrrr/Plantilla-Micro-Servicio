namespace PlantillaMicroServicio.Models.Configuracion
{
    public class ConfiguracionCors
    {
        public string[] OrigenesPermitidos { get; set; } = new[] { "http://localhost:3000" };
        public bool PermitirCredenciales { get; set; } = true;
        public string[] MetodosPermitidos { get; set; } = new[] { "GET", "POST", "PUT", "DELETE", "OPTIONS" };
        public string[] HeadersPermitidos { get; set; } = new[] { "Content-Type", "Authorization", "X-Requested-With" };
    }
}