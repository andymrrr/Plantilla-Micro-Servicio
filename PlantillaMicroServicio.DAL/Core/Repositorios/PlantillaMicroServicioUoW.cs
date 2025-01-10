using PlantillaMicroServicio.DAL.Context;
using PlantillaMicroServicio.DAL.Core.Interfaces;
using PlantillaMicroServicio.DAL.Core.Repositorios;
using PlantillaMicroServicio.DAL.Modelo;

namespace Artes.Datos.Datos.Repositorios
{
    public class PlantillaMicroServicioUoW : IPlantillaMicroServicioUoW
    {
        private ContextPlantillaMicroServicio _context { get; }

        // Repositorios
        public IRepositorio<Ejemplo> Ejemplo { get;  set; }
      
        


        public PlantillaMicroServicioUoW(ContextPlantillaMicroServicio context)
        {
            _context = context;

            Ejemplo = new Repositorio<Ejemplo>(context);
           
        }

        
        public void GuardarCambios()
        {
            _context.SaveChanges();
        }

        public async Task GuardarCambiosAsync()
        {
            await _context.SaveChangesAsync();
        }

        // Método para liberar recursos
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

