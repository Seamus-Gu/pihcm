namespace Framework.Core
{
    /// <summary>
    /// 表示实体的启用或停用状态。用于指示对象当前是否处于可用状态。
    /// </summary>
    /// <remarks>可用于标识用户、功能或资源的当前有效性。通常与业务逻辑中的状态控制相关联。建议在需要明确区分启用与停用状态时使用此枚举类型。默认值为 <see
    /// cref="StatusEnum.Enable"/>。</remarks>
    public enum StatusEnum
    {

        /// <summary>
        /// 表示启用状态。
        /// </summary>
        Enable = 0,

        /// <summary>
        /// 表示禁用状态。
        /// </summary>
        Disable = 1,
    }
}
