namespace Framework.Core
{
    /// <summary>
    /// 定义用于实现数据存储库模式的通用接口。该接口为数据访问层提供抽象，便于实现与具体数据源无关的操作。
    /// </summary>
    /// <remarks>实现此接口可统一管理实体的持久化操作，支持依赖注入和单元测试。具体的数据访问方法和类型应由继承接口或实现类定义。</remarks>
    public interface IRepository
    {
    }
}
