namespace PIHCM.Gen.Dto
{
    public class GenTemplateDto
    {
        /// <summary>
        /// 模板名
        /// </summary>
        public virtual string Name { get; set; } = string.Empty;

        /// <summary>
        /// 模板内容
        /// </summary>
        public virtual string Content { get; set; } = string.Empty;

        /// <summary>
        /// 生成文件夹
        /// </summary>
        public virtual string GenFolder { get; set; } = string.Empty;

        /// <summary>
        /// 文件名
        /// </summary>
        public virtual string FileName { get; set; } = string.Empty;

        /// <summary>
        /// 代码
        /// </summary>
        public virtual string Code { get; set; } = string.Empty;
    }
}
