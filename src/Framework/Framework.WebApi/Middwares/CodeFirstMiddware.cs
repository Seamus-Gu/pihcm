using Framework.Core;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using SqlSugar;

namespace Framework.WebApi
{
    public static class CodeFirstMiddware
    {
        public static void UseCodeFirst(this IApplicationBuilder app)
        {
            var dataContext = app.ApplicationServices.GetService<ISqlSugarClient>();

            if (dataContext is null)
            {
                return;
            }

            var entityTypes = new List<Type>();
            var assemblyList = AppDomain.CurrentDomain.GetAssemblies()
               .Where(t => t.FullName != null && t.FullName.Contains(FrameworkConstant.PREFIX));

            foreach (var assembly in assemblyList)
            {
                var types = assembly.GetTypes()
                    .Where(t =>
                        t.IsClass &&
                        !t.IsAbstract &&
                        !t.IsInterface
                    );
                var matchTypes = types.Where(t =>
                  typeof(BaseEntity).IsAssignableFrom(t)
                );
                entityTypes.AddRange(matchTypes);
            }


            dataContext.DbMaintenance.CreateDatabase();

            entityTypes.ForEach(entity =>
            {
                dataContext.CodeFirst.InitTables(entity);
            });
        }
    }
}
