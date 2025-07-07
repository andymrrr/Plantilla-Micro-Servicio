namespace PlantillaMicroServicio.Models.Configuracion
{
    public class ConfiguracionJWT
    {
        public string? Llave { get; set; }
        public string? Asunto { get; set; }
        public string? Audiencia { get; set; }
        public int DuracionMinuto { get; set; } = 60;
        public TimeSpan TiempoExpira => TimeSpan.FromMinutes(DuracionMinuto);
        public int ClockSkewMinutos { get; set; } = 2;
        public bool ValidarIssuer { get; set; } = true;
        public bool ValidarAudience { get; set; } = true;
        public bool RequerirExpirationTime { get; set; } = true;
        public bool ValidarLifetime { get; set; } = true;
    }
}
