using Framework.Core;

namespace Framework.Security
{
    internal class TokenSign
    {
        /// <summary>
        /// TokenId
        /// </summary>
        public string TokenId { get; set; } = string.Empty;

        /// <summary>
        /// 设备
        /// </summary>
        public DeviceEnum Device { get; set; }
    }
}