using System.Reflection;
using System.Text.Json;
using System.Text.Json.Serialization;
using PlantillaMicroServicio.Infrastructure.Logging.Attributes;

namespace PlantillaMicroServicio.Infrastructure.Logging.Utilities
{
    /// <summary>
    /// Clase para excluir propiedades del logging basado en atributos
    /// </summary>
    public class ExclusionPropiedadesLog : JsonConverter<object>
    {
        public override bool CanConvert(Type typeToConvert)
        {
            return true;
        }

        public override object Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            throw new NotImplementedException();
        }

        public override void Write(Utf8JsonWriter writer, object value, JsonSerializerOptions options)
        {
            if (value == null)
            {
                writer.WriteNullValue();
                return;
            }

            writer.WriteStartObject();
            var properties = value.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance);

            foreach (var property in properties)
            {
                // Excluir propiedades marcadas con ExcluirDeLogAttribute
                if (property.GetCustomAttribute<ExcluirDeLogAttribute>() != null)
                    continue;

                // Excluir colecciones genéricas
                if (property.PropertyType.IsGenericType &&
                    property.PropertyType.GetGenericTypeDefinition() == typeof(ICollection<>))
                    continue;

                // Excluir arrays de bytes
                if (property.PropertyType == typeof(byte[]))
                    continue;

                var propertyValue = property.GetValue(value);
                if (propertyValue != null)
                {
                    writer.WritePropertyName(property.Name);
                    JsonSerializer.Serialize(writer, propertyValue, options);
                }
            }

            writer.WriteEndObject();
        }
    }
}