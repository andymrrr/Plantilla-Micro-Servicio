using PlantillaMicroServicio.DAL.Modelo;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace PlantillaMicroServicio.DAL.Context
{
    public class __DbContextClassName__ : DbContext
    {
        // TODO: Aqui define tus datasets

        public __DbContextClassName__(DbContextOptions<__DbContextClassName__> options) : base(options){}
        public DbSet<Ejemplo> Ejemplos { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
          
        }
    }
}
