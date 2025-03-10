﻿using PlantillaMicroServicio.Modelos;


namespace PlantillaMicroServicio.Dal.Nucleo.Interfaces
{
    public interface IPlantillaMicroServicioUoW : IDisposable
    {

        IRepositorio<Libro> Libros { get; set; }
       


        void GuardarCambios();
        Task GuardarCambiosAsync();

        Task BeginAsync();
        Task CommitAsync();
        Task RollbackAsync();
    }
}
