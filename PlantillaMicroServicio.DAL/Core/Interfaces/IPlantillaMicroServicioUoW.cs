using PlantillaMicroServicio.DAL.Modelo;

namespace PlantillaMicroServicio.DAL.Core.Interfaces
{
    public interface IPlantillaMicroServicioUoW
    {
        IRepositorio<Ejemplo> Ejemplo { get; set; }


        void GuardarCambios();
        Task GuardarCambiosAsync();

        Task BeginAsync();
        Task CommitAsync();
        Task RollbackAsync();

    }
}
