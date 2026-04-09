using Dm.util;
using Framework.Core.Utils;
using Scriban;

namespace PIHCM.Gen.Services
{
    public class GenTableService : IGenTableService, IScopeService
    {
        private readonly GenTableRepository _genTableRepository;
        private readonly GenColumnRepository _genColumnRepository;

        public GenTableService(GenTableRepository repository, GenColumnRepository genColumnRepository)
        {
            _genTableRepository = repository;
            _genColumnRepository = genColumnRepository;
        }

        public async Task<List<GenTable>> GetPageList(GenTableQueryDto query)
        {
            return await _genTableRepository.SelectPageList(query);
        }

        public async Task<List<GenTemplateDto>> GenerateCode(long tableId)
        {
            var genTable = await _genTableRepository.GetByIdAsync(tableId);

            genTable.Columns = await _genColumnRepository.SelectListByTableId(tableId);

            var templateList = GetTemplateList(tableId);

            foreach (var item in templateList)
            {
                var template = Template.ParseLiquid(item.Content);

                var result = template.Render(genTable);

                var fileName = string.Format(item.FileName, genTable.TableName);

                var path = Path.Combine(item.GenFolder, fileName);

                // 确保目标文件夹存在
                if (!Directory.Exists(item.GenFolder))
                {
                    Directory.CreateDirectory(item.GenFolder);
                }

                File.WriteAllText(path, result);

                item.Code = result;
            }

            return templateList;
        }

        public async Task<MemoryStream> ExportCode(long tableId)
        {
            string genFolder = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Generate", tableId.toString());

            var ms = FileUtil.ZipStream(genFolder);

            return ms;
        }

        private List<GenTemplateDto> GetTemplateList(long tableId)
        {
            var result = new List<GenTemplateDto>();

            string folder = Path.Combine(AppContext.BaseDirectory, "Templates", "Common");
            string genFolder = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Generate", tableId.toString());

            return new List<GenTemplateDto>
            {
                new GenTemplateDto
                {
                    Name = "Entity",
                    Content = File.ReadAllText($"{folder}/Entity.txt"),
                    GenFolder =  Path.Combine(genFolder,"Entities"),
                    FileName = "{0}.cs"
                },
                new GenTemplateDto
                {
                    Name = "Repository",
                    Content = File.ReadAllText($"{folder}/Repository.txt"),
                    GenFolder =  Path.Combine(genFolder,"Repositories"),
                    FileName = "{0}Repository.cs"
                },
                new GenTemplateDto
                {
                    Name = "Service",
                    Content = File.ReadAllText($"{folder}/Service.txt"),
                    GenFolder =  Path.Combine(genFolder,"Services"),
                    FileName = "{0}Services.cs"
                },
                new GenTemplateDto
                {
                    Name = "Interface",
                    Content = File.ReadAllText($"{folder}/Interface.txt"),
                    GenFolder =  Path.Combine(genFolder,"Entities"),
                    FileName = "I{0}Service.cs"
                },
                new GenTemplateDto
                {
                    Name = "Controller",
                    Content = File.ReadAllText($"{folder}/Controller.txt"),
                    GenFolder =  Path.Combine(genFolder,"Controllers"),
                    FileName = "{0}Controller.cs"
                }
            };
        }
    }
}