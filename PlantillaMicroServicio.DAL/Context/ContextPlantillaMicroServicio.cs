using PlantillaMicroServicio.DAL.Modelo;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace PlantillaMicroServicio.DAL.Context
{
    public class ContextPlantillaMicroServicio : DbContext
    {
        // TODO: Aqui define tus datasets

        public ContextPlantillaMicroServicio(DbContextOptions<ContextPlantillaMicroServicio> options) : base(options){}
        public DbSet<Ejemplo> Ejemplos { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
          
        }
    }
}
