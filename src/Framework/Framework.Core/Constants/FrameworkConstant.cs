namespace Framework.Core
{
    public class FrameworkConstant
    {
        #region 项目基础
        // 项目前缀
        public const string FRAMEWORK_PREFIX = "PIHCM";
        // 数据库雪花Id
        public const string SNOW_ID = "SnowId";
        // 基础配置项
        public const string COMMON = "PIHCM.Common";
        #endregion

        #region App
        public const string APP = "App";
        public const string SERVICE = "Service";
        #endregion

        #region 协议
        public const string HTTP = "http://";
        public const string HTTPS = "https://";
        #endregion

        #region 国际化
        public const string LOCALIZATION_PATH = "Resources";
        public const string LANGUAGE = "Lang";
        public const string LANGUAGE_CHINESE = "zh-CN";
        public const string LANGUAGE_ENGLISH = "en-US";
        #endregion

        #region 路由&UI (路由/布局/管理员)
        public const string HEALTH_ROUTE = "/health";
        public const string ADMIN = "admin";
        public const string ADMIN_PERMISSION = "*:*:*";
        public const string LAYOUT = "Layout";
        public const string PARENT_VIEW = "ParentView";
        #endregion

        #region 操作结果
        public const string ADD = "Add";
        public const string EDIT = "Edit";
        public const string REMOVE = "Remove";
        public const string SUCCESS = "Success";
        public const string FAILED = "Failed";
        #endregion Result


        #region Swagger (API 文档)
        public const string OPENAPI_PATH = "openapi/v1.json";
        public const string SWAGGER_HEADER = "Is-Swagger";
        #endregion

        #region Configurations (数据库/缓存/安全/存储/邮件/任务)
        public const string REDIS = "Redis";
        //public const string EMAIL = "Email";
        //public const string JOB = "Job";
        public const string SECURITY = "Security";
        #endregion



    }
}
