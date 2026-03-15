namespace PIHCM.Api.Core.Entities
{
    /// <summary>
    /// 表示系统用户的登录信息，包括用户名、登录设备、登录状态和登录时间等。用于记录和追踪用户的登录活动。
    /// </summary>
    /// <remarks>该类型通常用于安全审计、登录日志或用户行为分析等场景。继承自 BaseEntity，包含基础实体属性。线程安全性取决于具体的使用方式。</remarks>
    public class SysLoginInfo : BaseEntity
    {
        /// <summary>
        /// 获取或设置用户的名称。
        /// </summary>
        public string UserName { get; set; } = string.Empty;

        /// <summary>
        /// 获取或设置与该实体关联的IP地址。
        /// </summary>
        [SugarColumn(ColumnName = "ip_address")]
        public string IPAddress { get; set; } = string.Empty;

        /// <summary>
        /// 获取或设置设备的类型。
        /// </summary>
        public DeviceEnum DeviceType { get; set; }

        //请使用中文注释
        /// <summary>
        /// 获取或设置当前操作或实体的状态。用于指示处理流程中的不同阶段或结果。
        /// 0 成功 1 失败
        /// </summary>
        public StatusEnum Status { get; set; }

        /// <summary>
        /// 登录时间
        /// </summary>
        public DateTime LoginTime { get; set; }
    }
}