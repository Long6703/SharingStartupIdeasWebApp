using System.Reflection;

namespace SSI.Ultils.Mapper
{
    public static class AutoMapperExtensions
    {
        public static void AddAutoMapperConfig(this IServiceCollection services)
        {
            #region Add AutoMapper
            var assembly = Assembly.GetAssembly(typeof(AutoMapperExtensions));
            services.AddAutoMapper(assembly);
            #endregion
        }
    }
}
