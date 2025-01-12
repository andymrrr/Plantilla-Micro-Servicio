﻿using Azure;
using PlantillaMicroServicio.Dal.Contexto;
using PlantillaMicroServicio.Dal.Nucleo.Interfaces;
using PlantillaMicroServicio.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlantillaMicroServicio.Dal.Nucleo.Repositorios
{
    public class PlantillaMicroServicioUoW : IPlantillaMicroServicioUoW
    {
        private ContextPlantillaMicroServicio _context { get; }

        // Repositorios
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
