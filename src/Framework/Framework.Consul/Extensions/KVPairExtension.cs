using Consul;

namespace Framework.Consul
{
    internal static class KVPairExtension
    {
        internal static IDictionary<string, string?> ToConfigDic(this KVPair result)
        {
            var stream = new MemoryStream(result.Value);

            return JsonToDictionatyUtil.Parse(stream);
        }
    }
}