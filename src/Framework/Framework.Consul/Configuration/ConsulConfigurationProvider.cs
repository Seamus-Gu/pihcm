using Consul;
using Framework.Core;
using Microsoft.Extensions.Configuration;
using System.Net;

namespace Framework.Consul
{
    internal class ConsulConfigurationProvider : ConfigurationProvider
    {
        private readonly IConsulClient _consulClient;
        private readonly string _serviceName;
        private readonly string _envName;
        private readonly TimeSpan _timerCycle;
        private ulong _lastIndex;
        private Task? _pollTask;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="timerCycle">轮询时长</param>
        /// <param name="assemblyName">程序集名</param>
        /// <param name="env">环境名</param>
        public ConsulConfigurationProvider(
            ConsulClient consulClient,
            TimeSpan timerCycle,
            string assemblyName,
            string env)
        {
            _consulClient = consulClient;
            _timerCycle = timerCycle;
            _serviceName = assemblyName;
            _envName = env;
        }

        /// <summary>
        /// 重写Load
        /// </summary>
        public override void Load()
        {
            if (_pollTask != null)
            {
                return;
            }

            LoadData(GetKvPairList(false).GetAwaiter().GetResult());

            _pollTask = Task.Run(() => PollReaload());
        }

        /// <summary>
        /// 将配置数据写入全局
        /// </summary>
        /// <param name="configData"></param>
        private void LoadData(QueryResult<KVPair[]> queryRes)
        {
            var currentName = _envName + DelimitersConstant.SLASH + _serviceName;
            var commonName = _envName + DelimitersConstant.SLASH + FrameworkConstant.COMMON;

            var list = queryRes.Response.Where(t => t.Key == currentName || t.Key == commonName).ToList();
            Data = list.SelectMany(t => t.ToConfigDic()).ToDictionary(t => t.Key, t => t.Value);

            SetLastIndex(queryRes.LastIndex);
        }

        private async Task<QueryResult<KVPair[]>> GetKvPairList(bool waitForChange)
        {
            var queryOptions = new QueryOptions
            {
                WaitTime = _timerCycle,
                WaitIndex = waitForChange ? _lastIndex : 0
            };

            var result = await _consulClient.KV.List(_envName, queryOptions).ConfigureAwait(false);

            if (result.StatusCode == HttpStatusCode.OK)
            {
                return result;
            }

            var message = string.Format(CommonLocalization.NotLoadConsul, result.StatusCode);

            throw new CodeException(ErrorEnum.NotLoadConsul.ToInt(), message);
        }

        /// <summary>
        /// 轮询
        /// </summary>
        private async Task PollReaload()
        {
            while (true)
            {
                var result = await GetKvPairList(true).ConfigureAwait(false);

                if (result.LastIndex > _lastIndex)
                {
                    var currentName = _envName + DelimitersConstant.SLASH + _serviceName;
                    var commonName = _envName + DelimitersConstant.SLASH + FrameworkConstant.FRAMEWORK_PREFIX;

                    var list = result.Response.Where(t => t.Key == commonName || t.Key == currentName).ToList();
                    Data = list.SelectMany(t => t.ToConfigDic()).ToDictionary(t => t.Key, t => t.Value);

                    OnReload();
                }

                SetLastIndex(result.LastIndex);
            }
        }

        private void SetLastIndex(ulong index)
        {
            if (index == 0)
            {
                _lastIndex = 1;
            }
            else
            {
                _lastIndex = index < _lastIndex ? 0 : index;
            }
        }
    }
}