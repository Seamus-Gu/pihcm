using Framework.Core;
using StackExchange.Redis;

namespace Framework.Cache
{
    /// <summary>
    /// Provides an implementation of a distributed cache using Redis as the backend store.
    /// </summary>
    /// <remarks>This class enables storing, retrieving, and removing cache entries in a Redis database. It
    /// supports both synchronous and asynchronous operations for common cache actions. Thread safety is ensured for
    /// connection management. Use this class when you need a scalable, high-performance cache shared across multiple
    /// application instances.</remarks>
    public class RedisCache : ICache
    {
        private IDatabase _database;

        private IConnectionMultiplexer _connMultiplexer;

        private readonly object _lock = new object();

        public RedisCache()
        {
            var redisConfig = App.GetConfig<RedisConfig>(FrameworkConstant.REDIS);

            ThreadPool.SetMinThreads(200, 200);

            var redisConnectionStr = $"{redisConfig.Host}:{redisConfig.Port}";
            var options = ConfigurationOptions.Parse(redisConnectionStr);
            options.Password = redisConfig.Password;

            _connMultiplexer = GetConnectRedisMultiplexer(options);
            _database = _connMultiplexer.GetDatabase();
        }

        /// <summary>
        /// Retrieves the value associated with the specified key from the data store and deserializes it to the
        /// specified type.
        /// </summary>
        /// <remarks>If the key does not exist or the stored value cannot be deserialized to the specified
        /// type, the method returns the default value for type T. The method expects the stored value to be in a format
        /// compatible with the deserialization process.</remarks>
        /// <typeparam name="T">The type to which the stored value will be deserialized.</typeparam>
        /// <param name="key">The key whose associated value is to be retrieved. Cannot be null.</param>
        /// <returns>The deserialized value of type T if the key exists and the value can be deserialized; otherwise, the default
        /// value for type T.</returns>
        public T? Get<T>(string key)
        {
            var redisValue = _database.StringGet(key);

            if (!redisValue.HasValue)
            {
                return default;
            }

            return JsonUtil.Deseriallize<T>(redisValue!);
        }

        public async Task<T?> GetAsync<T>(string key)
        {
            var redisValue = _database.StringGet(key);

            if (!redisValue.HasValue)
            {
                return default;
            }

            return JsonUtil.Deseriallize<T>(redisValue!);
        }

        public bool Set(string key, object value, TimeSpan? timeout)
        {
            var valueStr = JsonUtil.Serialize(value);

            if (timeout is null)
            {
                return _database.StringSet(key, valueStr);
            }
            else
            {
                return _database.StringSet(key, valueStr, timeout.Value);
            }
        }

        public async Task<bool> SetAsync(string key, object value, TimeSpan? timeout)
        {
            var valueStr = JsonUtil.Serialize(value);

            if (timeout is null)
            {
                return await _database.StringSetAsync(key, valueStr);
            }
            else
            {
                return await _database.StringSetAsync(key, valueStr, timeout.Value);
            }
        }

        public bool Remove(string key)
        {
            return _database.KeyDelete(key);
        }

        public Task<bool> RemoveAsync(string key)
        {
            return _database.KeyDeleteAsync(key);
        }
        private IConnectionMultiplexer GetConnectRedisMultiplexer(ConfigurationOptions options)
        {
            if (_connMultiplexer == null)
            {
                lock (_lock)
                {
                    if (_connMultiplexer != null)
                    {
                        return _connMultiplexer;
                    }

                    return ConnectionMultiplexer.Connect(options);

                    //Todo 注册事件

                    //_connMultiplexer.ConnectionFailed
                    //_connMultiplexer.ConnectionFailed += MuxerConnectionFailed;
                    //_connMultiplexer.ConnectionRestored += MuxerConnectionRestored;
                    //_connMultiplexer.ErrorMessage += MuxerErrorMessage;
                    //_connMultiplexer.ConfigurationChanged += MuxerConfigurationChanged;
                    //_connMultiplexer.HashSlotMoved += MuxerHashSlotMoved;
                    //_connMultiplexer.InternalError += MuxerInternalError;
                }
            }

            return _connMultiplexer;
        }

        #region Event

        /// <summary>
        /// 添加注册事件
        /// </summary>
        private static void AddRegisterEvent()
        {
            //_connMultiplexer.ConnectionRestored += ConnMultiplexer_ConnectionRestored;
            //_connMultiplexer.ConnectionFailed += ConnMultiplexer_ConnectionFailed;
            //_connMultiplexer.ErrorMessage += ConnMultiplexer_ErrorMessage;
            //_connMultiplexer.ConfigurationChanged += ConnMultiplexer_ConfigurationChanged;
            //_connMultiplexer.HashSlotMoved += ConnMultiplexer_HashSlotMoved;
            //_connMultiplexer.InternalError += ConnMultiplexer_InternalError;
            //_connMultiplexer.ConfigurationChangedBroadcast += ConnMultiplexer_ConfigurationChangedBroadcast;
        }

        ///// <summary>
        ///// 重新配置广播时（通常意味着主从同步更改）
        ///// </summary>
        ///// <param name="sender"></param>
        ///// <param name="e"></param>
        //private static void ConnMultiplexer_ConfigurationChangedBroadcast(object sender, EndPointEventArgs e)
        //{
        //}

        ///// <summary>
        ///// 发生内部错误时（主要用于调试）
        ///// </summary>
        ///// <param name="sender"></param>
        ///// <param name="e"></param>
        //private static void ConnMultiplexer_InternalError(object sender, InternalErrorEventArgs e)
        //{
        //}

        ///// <summary>
        ///// 更改集群时
        ///// </summary>
        ///// <param name="sender"></param>
        ///// <param name="e"></param>
        //private static void ConnMultiplexer_HashSlotMoved(object sender, HashSlotMovedEventArgs e)
        //{
        //}

        ///// <summary>
        ///// 配置更改时
        ///// </summary>
        ///// <param name="sender"></param>
        ///// <param name="e"></param>
        //private static void ConnMultiplexer_ConfigurationChanged(object sender, EndPointEventArgs e)
        //{
        //}

        ///// <summary>
        ///// 发生错误时
        ///// </summary>
        ///// <param name="sender"></param>
        ///// <param name="e"></param>
        //private static void ConnMultiplexer_ErrorMessage(object sender, RedisErrorEventArgs e)
        //{
        //}

        ///// <summary>
        ///// 物理连接失败时
        ///// </summary>
        ///// <param name="sender"></param>
        ///// <param name="e"></param>
        //private static void ConnMultiplexer_ConnectionFailed(object sender, ConnectionFailedEventArgs e)
        //{
        //}

        ///// <summary>
        ///// 建立物理连接时
        ///// </summary>
        ///// <param name="sender"></param>
        ///// <param name="e"></param>
        //private static void ConnMultiplexer_ConnectionRestored(object sender, ConnectionFailedEventArgs e)
        //{
        //}

        #endregion Event
    }
}