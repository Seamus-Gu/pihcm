using Framework.Orm;

namespace PIHCM.Core.Migrations
{
    public class _20260501_init : Migration
    {
        protected override void Up()
        {
            CreateTable("sys_user")
                .CreateId()
                .CreateProperty("sys_name", typeof(string), new SugarColumn() { Length = 20 });

            InsertData("sys_user")
                .Column("id", "sys_name")
                .Values(1, "admin");
        }

        protected override void Down()
        {
            DropTable("sys_user");
        }
    }
}
