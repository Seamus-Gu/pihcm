namespace Framework.Core
{

    /// <summary>
    /// 表示支持的设备类型的枚举。
    /// </summary>
    /// <remarks>用于指示应用程序或服务的访问来源，例如 Web 端或移动应用端。可用于根据设备类型执行不同的业务逻辑。</remarks>
    public enum DeviceEnum
    {
        /// <summary>
        /// 表示 Web 类型的枚举值。
        /// </summary>
        Web = 0,

        /// <summary>
        /// 表示应用程序类型的枚举值。
        /// </summary>
        APP = 1,
    }
}
