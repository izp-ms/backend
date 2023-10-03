using AutoMapper;
using System.Reflection;

namespace Application.Mappings;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        ApplyMappingFromAssembly(Assembly.GetExecutingAssembly());
    }

    private void ApplyMappingFromAssembly(Assembly assembly)
    {
        List<Type> types = assembly.GetExportedTypes()
            .Where(x => typeof(IMap).IsAssignableFrom(x) && !x.IsInterface)
            .ToList();

        foreach (Type type in types)
        {
            object instance = Activator.CreateInstance(type);
            MethodInfo methodInfo = type.GetMethod("Mapping");
            methodInfo?.Invoke(instance, new object[] { this });
        }
    }
}