using System.Resources;

namespace Framework.Core
{
    public class FrameworkResource
    {
        private static readonly ResourceManager _resourceManager = new ResourceManager(typeof(FrameworkResource));

        public static string NotLoadConfig => GetString(ErrorEnum.NotLoadConfig.GetName());

        /// <summary>
        /// 无法加载Consul配置
        /// </summary>
        public static string NotLoadConsul => GetString(ErrorEnum.NotLoadConsul.GetName());

        public static string AddFailed => GetString($"{FrameworkConstant.ADD}{FrameworkConstant.FAILED}");

        public static string EditFailed => GetString($"{FrameworkConstant.EDIT}{FrameworkConstant.FAILED}");

        public static string RemoveFailed => GetString($"{FrameworkConstant.REMOVE}{FrameworkConstant.FAILED}");

        /// <summary>
        /// 获取对应的I18N消息
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static string GetString(string name)
        {
            return _resourceManager.GetString(name) ?? name;
        }
    }
}
