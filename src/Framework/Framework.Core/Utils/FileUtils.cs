using System.IO.Compression;

namespace Framework.Core.Utils
{
    //Todo 文件操作（读取、写入、删除、复制、移动、压缩、路径处理等）
    public class FileUtil
    {
        /// <summary>
        /// 将文件夹压缩到一个文件流中，可保存为zip文件，方便于web方式下载
        /// </summary>
        /// <param name="dir">文件夹</param>
        /// <param name="rootdir"></param>
        /// <returns>文件流</returns>
        public static MemoryStream ZipStream(string dir, string rootdir = "")
        {
            if (string.IsNullOrWhiteSpace(dir))
            {
                //return BadRequest(new { error = "文件夹路径不能为空" });
            }

            if (!Directory.Exists(dir))
            {
                //return NotFound(new { error = $"文件夹不存在: {folderPath}" });
            }

            var ms = new MemoryStream();

            var folderName = new DirectoryInfo(dir).Name;

            ZipFile.CreateFromDirectory(
                   sourceDirectoryName: dir,
                   destination: ms,
                   compressionLevel: CompressionLevel.Optimal,
                   includeBaseDirectory: true
               );

            ms.Position = 0;

            return ms;
        }
    }
}
