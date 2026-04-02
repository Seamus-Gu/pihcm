using Microsoft.Extensions.DependencyInjection;
using Yitter.IdGenerator;

namespace Framework.IdGenerater
{
    /// <summary>
    /// 为指定的服务集合添加分布式唯一ID生成器，使用配置的选项进行初始化。
    /// </summary>
    /// <remarks>该方法配置并注册基于 Snowflake 算法的分布式ID生成器，适用于分布式系统。生成器的选项（如 WorkerId
    /// 和位数等）从应用程序配置中加载。为避免不同服务间ID冲突，请确保每个服务实例使用唯一的 WorkerId。</remarks>
    public static class IdGeneraterExtension
    {
        //请使用中文注释
        /// <summary>
        /// 为依赖注入容器添加全局唯一ID生成器配置。
        /// </summary>
        ///
        /// <remarks>此方法将ID生成器配置为全局可用，适用于分布式系统中需要生成唯一ID的场景。请确保在微服务环境下为每个服务分配不同的WorkerId，以避免ID冲突。有关更多参数配置，请参考IdGeneratorOptions的定义。</remarks>
        /// <param name="services">要注册ID生成器服务的依赖注入服务集合。</param>
        public static void AddIdGenerater(this IServiceCollection services)
        {
            //var idConfig = App.GetConfig<IdConfig>(FrameworkConstant.SNOW_ID);
            // 创建 IdGeneratorOptions 对象，可在构造函数中输入 WorkerId： 微服务不同服务配置不同Id
            var options = new IdGeneratorOptions(58);
            // options.WorkerIdBitLength = 10; // 默认值6，限定 WorkerId 最大值为2^6-1，即默认最多支持64个节点。
            // options.SeqBitLength = 6; // 默认值6，限制每毫秒生成的ID个数。若生成速度超过5万个/秒，建议加大 SeqBitLength 到 10。
            // options.BaseTime = Your_Base_Time; // 如果要兼容老系统的雪花算法，此处应设置为老系统的BaseTime。
            // ...... 其它参数参考 IdGeneratorOptions 定义。

            // 保存参数（务必调用，否则参数设置不生效）：
            YitIdHelper.SetIdGenerator(options);
        }
    }
}