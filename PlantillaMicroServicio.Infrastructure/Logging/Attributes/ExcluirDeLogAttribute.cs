namespace PlantillaMicroServicio.Infrastructure.Logging.Attributes
{
    /// <summary>
    /// Atributo para excluir propiedades del logging
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class ExcluirDeLogAttribute : Attribute
    {
        public ExcluirDeLogAttribute() { }
    }
}