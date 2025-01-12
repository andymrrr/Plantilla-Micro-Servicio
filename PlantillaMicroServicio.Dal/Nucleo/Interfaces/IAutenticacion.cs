using PlantillaMicroServicio.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlantillaMicroServicio.Dal.Nucleo.Interfaces
{
    public interface IAutenticacion
    {
        string ObtenerUsuarioSesion();
        string? CrearToken(Usuario usuario, IList<string>? roles);
    }
}
