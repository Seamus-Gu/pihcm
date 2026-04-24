using Autofac;
using Framework.Core;

namespace Framework.DI
{
    /// <summary>
    /// 提供用于扩展依赖注入容器的静态方法。
    /// </summary>
    /// <remarks>该类包含扩展方法，用于在应用程序启动时批量注册实现了特定接口的仓储（Repository）和服务（Service）类型。通常与 Autofac
    /// 依赖注入框架配合使用，以简化类型注册流程。</remarks>
    public static class DependencyInjectionExtension
    {
        /// <summary>
        /// 为指定的 <see cref="ContainerBuilder"/> 实例注册应用程序中符合约定的仓储和服务类型，以便通过依赖注入进行解析。
        /// </summary>
        /// <remarks>此方法会自动扫描当前应用程序域中所有包含指定前缀的程序集，并将实现 IRepository
        /// 接口或以特定后缀命名的服务类注册为自身及其实现的接口。每次解析时都会创建新的实例。适用于基于约定的批量注册场景。</remarks>
        /// <param name="builder">要在其上注册类型的 Autofac 容器生成器实例。不能为空。</param>
        public static void InitAutofac(this ContainerBuilder builder)
        {
            var assemblyList = AppDomain.CurrentDomain.GetAssemblies()
                .Where(t => t.FullName != null && t.FullName.Contains(FrameworkConstant.PREFIX));

            builder.RegisterAssemblyTypes(assemblyList.ToArray())
                .Where(t => t.IsClass && !t.IsAbstract)
                .Where(t => t.IsAssignableTo<IRepository>())
                .AsSelf().AsImplementedInterfaces()
                .InstancePerDependency();

            builder.RegisterAssemblyTypes(assemblyList.ToArray())
                .Where(t => t.IsClass && !t.IsAbstract)
                .Where(t => t.IsAssignableTo<ISingletonService>())
                .AsSelf().AsImplementedInterfaces()
                .SingleInstance();

            builder.RegisterAssemblyTypes(assemblyList.ToArray())
                .Where(t => t.IsClass && !t.IsAbstract)
                .Where(t => t.IsAssignableTo<ITransientService>())
                .AsSelf().AsImplementedInterfaces()
                .InstancePerDependency();

            builder.RegisterAssemblyTypes(assemblyList.ToArray())
                .Where(t => t.IsClass && !t.IsAbstract)
                .Where(t => t.IsAssignableTo<IScopeService>())
                .AsSelf().AsImplementedInterfaces()
                .InstancePerLifetimeScope();


            //// 注册Job所有接口
            //builder.RegisterAssemblyTypes(assemblyList.ToArray())
            //    .Where(t => t.IsClass && !t.IsAbstract)
            //    .Where(t => t.IsAssignableTo<IJobService>())
            //    //.Where(t => t.GetInterfaces().Contains(typeof(IJobService)))
            //    .AsImplementedInterfaces()
            //    .InstancePerLifetimeScope();
        }
    }
}