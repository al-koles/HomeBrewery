using System.Reflection;
using AutoMapper;

namespace HomeBrewery.Application.Common.Mappings
{
    public class AssemblyMappingProfile : Profile
    {
        public AssemblyMappingProfile(Assembly assembly) =>
            ApplyMappingsFromAssembly(assembly);

        private void ApplyMappingsFromAssembly(Assembly assembly)
        {
            var types = assembly.GetExportedTypes()
                .Where(type => type.GetInterfaces()
                    .Any(i => i.IsGenericType &&
                              i.GetGenericTypeDefinition() == typeof(IMapWith<>)))
                .ToList();

            types.ForEach(type =>
            {
                var instance = Activator.CreateInstance(type);

                var methodInfo = type.GetMethod("Mapping")
                                 ?? type
                                     .GetInterfaces()
                                     .FirstOrDefault(i =>
                                         i.IsGenericType &&
                                         i.GetGenericTypeDefinition() == typeof(IMapWith<>))?
                                     .GetMethod("Mapping");

                methodInfo?.Invoke(instance, new object[] { this });
            });
        }
    }
}
