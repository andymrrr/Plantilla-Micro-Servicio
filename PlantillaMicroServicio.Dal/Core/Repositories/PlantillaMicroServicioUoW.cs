
using PlantillaMicroServicio.Dal.Contexto;
using PlantillaMicroServicio.Dal.Core.Interfaces;

namespace PlantillaMicroServicio.Dal.Core.Repositories
{
    public class PlantillaMicroServicioUoW : IPlantillaMicroServicioUoW
    {
        private ContextPlantillaMicroServicio _context { get; }
        public PlantillaMicroServicioUoW(ContextPlantillaMicroServicio context)
        {
            _context = context;
          
        }


        public void GuardarCambios()
        {
            _context.SaveChanges();
        }

        public async Task GuardarCambiosAsync()
        {
            await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }

        #region Transacciones
        public async Task BeginAsync()
        {
            await _context.Database.BeginTransactionAsync();
        }

        public async Task CommitAsync()
        {
            await _context.Database.CommitTransactionAsync();
        }

        public async Task RollbackAsync()
        {
            await _context.Database.RollbackTransactionAsync();
        }
        #endregion
    }
}
