﻿namespace PlantillaMicroServicio.Aplication.Servicios.Cache
{
    public interface ICacheService
    {
        Task<T?> ObtenerAsync<T>(string clave) where T : class;
        Task GuardarAsync<T>(string clave, T valor, TimeSpan? expiracion = null);
        Task EliminarAsync(string clave);
    }

}
