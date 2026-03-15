namespace Framework.Core
{
    /// <summary>
    /// 定义用于发现和获取所有可用服务及其地址的接口。
    /// </summary>
    /// <remarks>实现此接口的类型通常用于服务注册中心或服务发现机制，以便客户端能够动态获取服务列表及其对应的访问地址。线程安全性和缓存策略取决于具体实现。</remarks>
    public interface IServiceDiscovery
    {
        /// <summary>
        /// 异步获取所有已注册服务的名称及其对应描述。
        /// </summary>
        /// <remarks>此方法通常用于发现和枚举当前可用的服务。返回的字典不会为 null，但可能为空。调用方可根据返回结果动态展示或处理服务列表。</remarks>
        /// <returns>一个包含服务名称及其描述的字典。字典的键为服务名称，值为对应的服务描述。如果没有已注册的服务，则返回一个空字典。</returns>
        public Task<Dictionary<string, string>> GetServices();
    }
}