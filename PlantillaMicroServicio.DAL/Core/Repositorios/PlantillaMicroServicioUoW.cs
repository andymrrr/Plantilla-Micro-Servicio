
using Artes.Datos.Datos.Context;
using Artes.Datos.Datos.Interfaz;
using Artes.Datos.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace Artes.Datos.Datos.Repositorios
{
    public class ArteUoW : IArteUoW
    {
        private ContextArte _context { get; }

        // Repositorios
        public IRepositorio<Arte> Arte { get;  set; }
        public IRepositorioArteSubCategoria ArteSubCategoria { get;  set; }
        public IRepositorioArteTag ArteTag { get;  set; }
        public IRepositorio<Categoria> Categoria { get;  set; }
        public IRepositorio<Comentario> Comentario { get;  set; }
        public IRepositorio<MetodoPago> MetodoPago { get;  set; }
        public IRepositorio<Notificacion> Notificacion { get;  set; }
        public IRepositorio<Rol> Rol { get;  set; }
        public IRepositorio<SubCategoria> SubCategoria { get;  set; }
        public IRepositorio<Tag> Tag { get;  set; }
        public IRepositorio<Usuario> Usuario { get;  set; }
        public IRepositorio<LogError> LogError { get; set; }
        public IRepositorio<UsuarioRol> UsuarioRol { get; set; }
        public IRepositorio<DetalleFactura> DetalleFactura { get; set; }
        public IRepositorioImagen Imagen { get; set; }
        public IRepositorioFactura Factura { get; set; }



        public ArteUoW(ContextArte context)
        {
            _context = context;

            Arte = new Repositorio<Arte>(context);
            ArteSubCategoria = new RepositorioArteSubCategoria(context);
            ArteTag = new RepositorioArteTag(context);
            Categoria = new Repositorio<Categoria>(context);
            Comentario = new Repositorio<Comentario>(context);
            MetodoPago = new Repositorio<MetodoPago>(context);
            Notificacion = new Repositorio<Notificacion>(context);
            Rol = new Repositorio<Rol>(context);
            SubCategoria = new Repositorio<SubCategoria>(context);
            Tag = new Repositorio<Tag>(context);
            Usuario = new Repositorio<Usuario>(context);
            UsuarioRol = new Repositorio<UsuarioRol>(context);
            LogError = new Repositorio<LogError>(context);
            Imagen = new RepositorioImagen(context);
            Factura = new RepositorioFactura(context);
            DetalleFactura = new Repositorio<DetalleFactura>(context);
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

