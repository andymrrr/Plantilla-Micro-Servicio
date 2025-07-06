using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Reflection;

namespace PlantillaMicroServicio.Aplication.Servicios.Utilitario
{
    public class ExclusionPropiedadesLog : DefaultContractResolver
    {
        protected override JsonProperty CreateProperty(MemberInfo member, MemberSerialization memberSerialization)
        {
            var propiedad = base.CreateProperty(member, memberSerialization);
            if (propiedad.AttributeProvider.GetAttributes(true).OfType<ExcluirDeLogAttribute>().Any())
            {
                propiedad.ShouldSerialize = instance => false; 
            }
            if (propiedad.PropertyType.IsGenericType && propiedad.PropertyType.GetGenericTypeDefinition() == typeof(ICollection<>))
            {
                propiedad.ShouldSerialize = instance => false;
            }
            else if (propiedad.PropertyType == typeof(byte[]))
            {
                propiedad.ShouldSerialize = instance => false;
            }

            return propiedad;
        }
    }

}
