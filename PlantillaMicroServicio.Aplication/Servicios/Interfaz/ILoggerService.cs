﻿namespace PlantillaMicroServicio.Aplication.Servicios.Interfaz
{
    public interface ILoggerService
    {
        void LogInformation(string message);
        void LogWarning(string message);
        void LogError(string message, Exception? ex = null);
        void LogRequestWithParams(string endpoint, object parameters);

    }
}
