using PlantillaMicroServicio.Models;
using System.Security.Claims;

namespace PlantillaMicroServicio.Infrastructure.Authentication
{
    public interface IJwtService
    {
        string? CrearToken(Usuario usuario, IList<string>? roles);
        string? RenovarToken(string tokenActual);
        bool ValidarToken(string token);
        ClaimsPrincipal? ObtenerClaimsDelToken(string token);
        string ObtenerUsuarioSesion();
        DateTime? ObtenerFechaExpiracion(string token);
    }
}