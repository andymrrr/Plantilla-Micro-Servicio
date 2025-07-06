using PlantillaMicroServicio.Core.Repositorios;
using PlantillaMicroServicio.Dal.Contexto;
using PlantillaMicroServicio.Dal.Core.Interfaces;
using PlantillaMicroServicio.Models;

namespace PlantillaMicroServicio.Dal.Core.Repositorios
{
    public class PlantillaMicroServicioUoW : IPlantillaMicroServicioUoW
    {
        private ContextPlantillaMicroServicio _context { get; }
        public IRepositorio<Libro> Libros { get; set; }
      



        public PlantillaMicroServicioUoW(ContextPlantillaMicroServicio context)
        {
            _context = context;

            Libros = new Repositorio<Libro>(context);
          
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
