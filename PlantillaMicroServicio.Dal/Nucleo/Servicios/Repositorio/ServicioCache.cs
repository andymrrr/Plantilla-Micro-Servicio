using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;
using PlantillaMicroServicio.Dal.Modelos;
using PlantillaMicroServicio.Dal.Nucleo.Servicio.Interfaz;

namespace PlantillaMicroServicio.Dal.Nucleo.Servicio.Repositorio
{
    public class ServicioCache : IServicioCache
    {
        private readonly IMemoryCache _cache;
        private readonly TimeSpan _expiracionPorDefecto;
       

        public ServicioCache(IMemoryCache cache, IOptions<ConfiguracionCache> opciones)
        {
            _cache = cache;
            _expiracionPorDefecto = TimeSpan.FromSeconds(opciones.Value.TiempoExpiracion > 0
                ? opciones.Value.TiempoExpiracion
                : 300);
        }  
        public Task<T?> ObtenerAsync<T>(string clave) where T : class
        {
           
                if (_cache.TryGetValue(clave, out T valor))
                {
                   
                    return Task.FromResult(valor);
                }   
                return Task.FromResult<T?>(null);
           
        }
        public Task GuardarAsync<T>(string clave, T valor, TimeSpan? expiracion = null)
        {
           
                var opciones = new MemoryCacheEntryOptions
                {
                    AbsoluteExpirationRelativeToNow = expiracion ?? _expiracionPorDefecto
                };

                _cache.Set(clave, valor, opciones);
             
            return Task.CompletedTask;
        }

        public Task EliminarAsync(string clave)
        {
           
                _cache.Remove(clave);
            
            return Task.CompletedTask;
        }
    }


}
