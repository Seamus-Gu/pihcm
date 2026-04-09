namespace PIHCM.Gen.Constants
{
    public class GenConstant
    {
        public static readonly Dictionary<string, string> TYPE_MAP = new Dictionary<string, string>
        {
            { SQLConstant.INT, "int" },
            { SQLConstant.INTEGER, "int" },
            { SQLConstant.TINYINT, "bool" },
            { SQLConstant.BIGINT , "long" },
            { SQLConstant.DATE , "DateTime" },
            { SQLConstant.DATETIME , "DateTime" },
            { SQLConstant.CHAR , "char" },
            { SQLConstant.VARCHAR , "string" },
        };
    }
}
