using AutoMapper;
using ShoppingCart.Mappings;

namespace ShoppingCart.Core.Mappings;

public static class Mapper
{
    public static IMapper GetMapper()
    {
        var configuration = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile<MappingProfile>();
        });

        return configuration.CreateMapper();
    }
}