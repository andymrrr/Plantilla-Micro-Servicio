using PlantillaMicroServicio.Models;

namespace PlantillaMicroServicio.Infrastructure.Authentication
{
    public interface IAutenticacion
    {
        string ObtenerUsuarioSesion();
        string? CrearToken(Usuario usuario, IList<string>? roles);
    }
}