namespace PIHCM.Api.Core.Entities
{
    /// <summary>
    /// 表示系统中的部门实体，支持树形结构以反映部门层级关系。用于管理和组织部门相关信息。
    /// </summary>
    /// <remarks>继承自 TreeEntity<SysDept>，可用于构建多级部门结构。适用于需要部门分级、状态管理的数据场景。</remarks>
    public class SysDept : TreeEntity<SysDept>
    {
        /// <summary>
        /// 获取或设置部门的名称。
        /// </summary>
        public virtual string DeptName { get; set; } = string.Empty;

        /// <summary>
        /// 获取或设置部门的当前状态。用于指示部门的有效性或业务流程中的状态。
        /// </summary>
        /// <remarks>通常用于区分部门是否处于启用、禁用或其他业务相关状态。设置该属性时应确保赋值为有效的枚举成员。</remarks>
        public virtual StatusEnum DeptStatus { get; set; }

        /// <summary>
        /// 数据状态
        /// </summary>
        public virtual StatusEnum DataStatus { get; set; }
    }
}