using Microsoft.EntityFrameworkCore;
using PlantillaMicroServicio.Models;

namespace PlantillaMicroServicio.Dal.Contexto
{
    public class ContextPlantillaMicroServicio : DbContext
    {
        public ContextPlantillaMicroServicio(DbContextOptions<ContextPlantillaMicroServicio> options) : base(options) { }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

           
        }

    }
}
