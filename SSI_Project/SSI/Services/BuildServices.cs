using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace SSI.Services
{
    public static class BuildServices
    {
        public static void AddService(this IServiceCollection services)
        {
            #region Đăng ký các service
            var assembly = Assembly.GetAssembly(typeof(BuildServices));
            var classes = assembly.ExportedTypes
               .Where(a => !a.Name.StartsWith("I") && a.Name.EndsWith("Service"));
            foreach (Type implement in classes)
            {
                foreach (var @interface in implement.GetInterfaces())
                {
                    services.AddScoped(@interface, implement);
                }
            }
            #endregion
        }
    }
}
