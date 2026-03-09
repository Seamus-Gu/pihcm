using Framework.Core.Enums;
using System.Resources;

namespace Framework.Core.Resources
{
    public class CommonLocalization
    {
        private static readonly ResourceManager _resourceManager = new ResourceManager(typeof(CommonLocalization));

        /// <summary>
        /// 无法加载Consul配置
        /// </summary>
        public static string NotLoadConsul => GetString(ErrorEnum.NotLoadConsul.GetName())!;

        /// <summary>
        /// 无法获取登录用户
        /// </summary>
        public static string NoLoginUser => GetString(ErrorEnum.NoLoginUser.GetName())!;

        /// <summary>
        /// 获取对应的I18N消息
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static string? GetString(string name)
        {
            return _resourceManager.GetString(name);
        }
    }
}
