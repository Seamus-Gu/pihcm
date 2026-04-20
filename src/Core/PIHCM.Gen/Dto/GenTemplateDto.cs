namespace PIHCM.Gen.Dto
{
    public class GenTemplateDto
    {
        /// <summary>
        /// 模板名
        /// </summary>
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// 模板内容
        /// </summary>
        public string Content { get; set; } = string.Empty;

        /// <summary>
        /// 生成文件夹
        /// </summary>
        public string GenFolder { get; set; } = string.Empty;

        /// <summary>
        /// 文件名
        /// </summary>
        public string FileName { get; set; } = string.Empty;

        /// <summary>
        /// 代码
        /// </summary>
        public string Code { get; set; } = string.Empty;
    }
}
