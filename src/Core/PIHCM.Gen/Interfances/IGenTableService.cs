namespace PIHCM.Gen.Interfances
{
    public interface IGenTableService
    {
        public Task<List<GenTable>> GetPageList(GenTableQueryDto query);

        public Task<List<GenTemplateDto>> GenerateCode(long tableId);

        public Task<MemoryStream> ExportCode(long tableId);
    }
}
