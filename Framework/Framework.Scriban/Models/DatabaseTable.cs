using Framework.Core.Utils;

namespace Framework.Scriban.Models;


/// <summary>
/// 表示数据库中的一张表，包括表名、描述和列的定义信息。
/// </summary>
/// <remarks>该类型为密封类，通常用于描述数据库结构或元数据。适用于数据库架构建模、代码生成或元数据分析等场景。所有属性均为只读初始化，确保表结构在创建后不可更改。</remarks>
public sealed class DatabaseTable
{

    /// <summary>
    /// 数据库表名
    /// </summary>
    public string Name { get; init; } = string.Empty;

    /// <summary>
    /// 实体名称
    /// </summary>
    public string EntityName { get; init; } = string.Empty;

    /// <summary>
    /// 描述
    /// </summary>
    public string? Description { get; init; }

    /// <summary>
    /// 获取数据库表中所有列的只读集合。用于描述表结构的各个字段。
    /// </summary>
    /// <remarks>集合中的每个元素都表示一个数据库列，包含列名、类型等元数据信息。该属性在对象初始化后不可更改，适用于只读场景。</remarks>
    public IReadOnlyList<DatabaseColumn> Columns { get; init; } = [];

    /// <summary>
    /// 表名驼峰命名表示
    /// </summary>
    public string CamelName
    {
        get
        {
            return NamingUtil.SnakeCaseToCamelCase(Name);
        }
    }

    public string KebabName
    {
        get
        {
            return NamingUtil.SnakeCaseToKebabCase(Name);
        }
    }

}
