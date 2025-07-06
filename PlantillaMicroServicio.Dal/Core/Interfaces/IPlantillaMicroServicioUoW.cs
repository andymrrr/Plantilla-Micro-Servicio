namespace PlantillaMicroServicio.Dal.Core.Interfaces
{
    public interface IPlantillaMicroServicioUoW : IDisposable
    {
        void GuardarCambios();
        Task GuardarCambiosAsync();

        Task BeginAsync();
        Task CommitAsync();
        Task RollbackAsync();
    }
}
