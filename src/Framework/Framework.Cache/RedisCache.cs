using Framework.Core;
using StackExchange.Redis;

namespace Framework.Cache
{
    public class RedisCache : ICache
    {
        private IDatabase _database;

        private IConnectionMultiplexer _connectionMultiplexer;

        private readonly object _lock = new object();

        public RedisCache()
        {
            var redisConfig = App.GetConfig<RedisConfig>("Redis");

            //var redisOptions = App.GetOptions<RedisConfig>();
            //ThreadPool.SetMinThreads(200, 200);
            //var options = new ConfigurationOptions
            //{
            //    AbortOnConnectFail = redisOptions.AbortOnConnectFail,
            //    AllowAdmin = redisOptions.AllowAdmin,
            //    ConnectRetry = redisOptions.ConnectRetry,
            //    ConnectTimeout = redisOptions.ConnectTimeout,
            //    KeepAlive = redisOptions.KeepAlive,
            //    SyncTimeout = redisOptions.SyncTimeout,
            //    EndPoints = { redisOptions.Host + ":" + redisOptions.Port },
            //    ServiceName = redisOptions.Name.IsNullOrEmpty() ? null : redisOptions.Name
            //};
            //if (!string.IsNullOrWhiteSpace(redisOptions.Password))
            //{
            //    options.Password = redisOptions.Password;
            //}

            //_connectionMultiplexer = ConnectionMultiplexer.Connect(options);
            //_database = _connectionMultiplexer.GetDatabase(redisOptions.Index);


            //var redisConnectionStr = $"{redisConfig.Host}:{redisConfig.Port}";
            //var options = ConfigurationOptions.Parse(redisConnectionStr);
            //options.Password = redisConfig.Password;

            //var connectionMultiplexer = GetConnectRedisMultiplexer(options);

            //DB = connectionMultiplexer.GetDatabase();
        }

        public string Get(string key)
        {
            var redisConfig = App.GetConfig<RedisConfig>("Redis");
            return "123";
        }

        public Task<T> GetAsync<T>(string key)
        {
            throw new NotImplementedException();
        }

        public bool Set(string key, object value, TimeSpan? timeout)
        {
            throw new NotImplementedException();
        }

        public Task<bool> SetAsync(string key, object value, TimeSpan? timeout)
        {
            throw new NotImplementedException();
        }

        public bool Remove(string key)
        {
            throw new NotImplementedException();
        }

        public Task<bool> RemoveAsync(string key)
        {
            throw new NotImplementedException();
        }

        //private static IConnectionMultiplexer GetConnectRedisMultiplexer(ConfigurationOptions options)
        //{
        //    if (_connMultiplexer == null)
        //    {
        //        lock (_lock)
        //        {
        //            if (_connMultiplexer != null)
        //            {
        //                return _connMultiplexer;
        //            }

        //            _connMultiplexer = _connectionMultiplexer.Connect(options);

        //            //Todo 注册事件

        //            //_connMultiplexer.ConnectionFailed
        //            //_connMultiplexer.ConnectionFailed += MuxerConnectionFailed;
        //            //_connMultiplexer.ConnectionRestored += MuxerConnectionRestored;
        //            //_connMultiplexer.ErrorMessage += MuxerErrorMessage;
        //            //_connMultiplexer.ConfigurationChanged += MuxerConfigurationChanged;
        //            //_connMultiplexer.HashSlotMoved += MuxerHashSlotMoved;
        //            //_connMultiplexer.InternalError += MuxerInternalError;
        //        }
        //    }

        //    return _connMultiplexer;
        //}

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

        #region String

        ///// <summary>
        ///// get the value for string key
        ///// </summary>
        ///// <param name="key"></param>
        ///// <returns></returns>
        //public static string Get(string key)
        //{
        //object value = null;
        //var redisValue = _database.StringGet(key);
        //if (!redisValue.HasValue)
        //    return default;
        //var valueEntry = redisValue.ToString().ToObject<ValueInfoEntry>();
        //value = valueEntry.TypeName == typeof(string).AssemblyQualifiedName
        //    ? valueEntry.Value
        //    : valueEntry.Value.ToObject(Type.GetType(valueEntry.TypeName));

        //if (valueEntry.ExpireType == CacheExpireType.Relative)
        //    _database.KeyExpire(key, valueEntry.ExpireTime);
        //return (T)value;

        //return _.StringGet(key);
        //}

        ///// <summary>
        ///// get the value for string key
        ///// </summary>
        ///// <param name="key"></param>
        ///// <returns></returns>
        //public static async Task<string> GetAsync(string key)
        //{
        //    try
        //    {
        //        var result = await DB.StringGetAsync(key);

        //        return result!;
        //    }
        //    catch (Exception)
        //    {
        //        throw new Exception("123");
        //    }
        //}

        ///// <summary>
        ///// get the value for string key
        ///// </summary>
        ///// <param name="key"></param>
        ///// <returns></returns>
        //public static RedisValueWithExpiry GetWithExpire(string key)
        //{
        //    return DB.StringGetWithExpiry(key);
        //}

        ///// <summary>
        ///// get the value for string key
        ///// </summary>
        ///// <param name="key"></param>
        ///// <returns></returns>
        //public static Task<RedisValueWithExpiry> GetWithExpireAsync(string key)
        //{
        //    return DB.StringGetWithExpiryAsync(key);
        //}

        ///// <summary>
        ///// set or update the value for string key
        ///// </summary>
        ///// <param name="key"></param>
        ///// <param name="value"></param>
        ///// <returns></returns>
        //public static bool Set(string key, string value)
        //{
        //    return DB.StringSet(key, value);
        //}

        ///// <summary>
        ///// set or update the value for string key
        ///// </summary>
        ///// <param name="key"></param>
        ///// <param name="value"></param>
        ///// <returns></returns>
        //public static Task<bool> SetStringAsync(string key, string value)
        //{
        //    return DB.StringSetAsync(key, value);
        //}

        ///// <summary>
        ///// set or update the value for string key
        ///// </summary>
        ///// <param name="key"></param>
        ///// <param name="value"></param>
        ///// <returns></returns>
        //public static bool SetStringWithExpire(string key, string value, TimeSpan? expireMinutes)
        //{
        //    return DB.StringSet(key, value, expireMinutes);
        //}

        ///// <summary>
        ///// set or update the value for string key
        ///// </summary>
        ///// <param name="key"></param>
        ///// <param name="value"></param>
        ///// <returns></returns>
        //public static Task<bool> SetStringWithExpireAsync(string key, string value, TimeSpan? expireMinutes)
        //{
        //    return DB.StringSetAsync(key, value, expireMinutes);
        //}

        ///// <summary>
        ///// Delete the value for string key
        ///// </summary>
        ///// <param name="key"></param>
        ///// <returns></returns>
        //public static bool Remove(string key)
        //{
        //    return DB.KeyDelete(key);
        //}

        ///// <summary>
        ///// Delete the value for string key
        ///// </summary>
        ///// <param name="key"></param>
        ///// <returns></returns>
        //public static Task<bool> RemoveAsync(string key)
        //{
        //    return DB.KeyDeleteAsync(key);
        //}



        #endregion String

        ///// <summary>
        ///// 新增Redis值(有时限)
        ///// </summary>
        ///// <param name="key">id</param>
        ///// <param name="value">值</param>
        ///// <param name="expireMinutes">时限(分钟,默认30天)</param>
        ///// <returns></returns>
        //public static bool AddExpire(string key, string value, int expireMinutes = 24 * 60 * 30)
        //    => new RedisHelper().AddExpireValue(key, value, expireMinutes);

        ///// <summary>
        ///// 保存一个对象
        ///// </summary>
        ///// <typeparam name="T"></typeparam>
        ///// <param name="key"></param>
        ///// <param name="obj"></param>
        ///// <returns></returns>
        //public bool SetStringKey<T>(string key, T obj, int expireMinutes = 0)
        //{
        //    string json = JsonConvert.SerializeObject(obj);
        //    if (expireMinutes > 0)
        //    {
        //        return DB.StringSet(key, json, TimeSpan.FromMinutes(expireMinutes));
        //    }
        //    else
        //        return DB.StringSet(key, json);
        //}

        ///// <summary>
        ///// 获取一个key的对象
        ///// </summary>
        ///// <typeparam name="T"></typeparam>
        ///// <param name="key"></param>
        ///// <returns></returns>
        //public T GetStringKey<T>(string key) where T : class
        //{
        //    var result = DB.StringGet(key);
        //    if (string.IsNullOrEmpty(result))
        //    {
        //        return null;
        //    }
        //    try
        //    {
        //        return JsonConvert.DeserializeObject<T>(result);
        //    }
        //    catch
        //    {
        //        return null;
        //    }
        //}

        ///// <summary>
        ///// set or update the HashValue for string key
        ///// </summary>
        ///// <param name="key"></param>
        ///// <param name="hashkey"></param>
        ///// <param name="value"></param>
        ///// <returns></returns>
        //public bool SetHashValue(string key, string hashkey, string value)
        //{
        //    return DB.HashSet(key, hashkey, value);
        //}

        ///// <summary>
        ///// set or update the HashValue for string key
        ///// </summary>
        ///// <typeparam name="T"></typeparam>
        ///// <param name="key"></param>
        ///// <param name="hashkey"></param>
        ///// <param name="t">defined class</param>
        ///// <returns></returns>
        //public bool SetHashValue<T>(String key, string hashkey, T t) where T : class
        //{
        //    var json = JsonConvert.SerializeObject(t);
        //    return DB.HashSet(key, hashkey, json);
        //}

        //public static void HashSet<T>(string key, List<T> list, Func<T, string> getModelId)
        //    => new RedisHelper().HashSetValue(key, list, getModelId);

        //public static void HashSetExpire<T>(string key, List<T> list, Func<T, string> getModelId, int expireTime = 24 * 60 * 30)
        //=> new RedisHelper().HashSetExpireValue(key, list, getModelId, expireTime);

        ///// <summary>
        ///// 保存一个集合
        ///// </summary>
        ///// <typeparam name="T"></typeparam>
        ///// <param name="key">Redis Key</param>
        ///// <param name="list">数据集合</param>
        ///// <param name="getModelId"></param>
        //public void HashSetValue<T>(string key, List<T> list, Func<T, string> getModelId)
        //{
        //    List<HashEntry> listHashEntry = new List<HashEntry>();
        //    foreach (var item in list)
        //    {
        //        string json = JsonConvert.SerializeObject(item);
        //        listHashEntry.Add(new HashEntry(getModelId(item), json));
        //    }
        //    DB.HashSet(key, listHashEntry.ToArray());
        //}

        ///// <summary>
        ///// 保存一个集合
        ///// </summary>
        ///// <typeparam name="T"></typeparam>
        ///// <param name="key">Redis Key</param>
        ///// <param name="list">数据集合</param>
        ///// <param name="getModelId"></param>
        //private void HashSetExpireValue<T>(string key, List<T> list, Func<T, string> getModelId, int expireTime)
        //{
        //    List<HashEntry> listHashEntry = new List<HashEntry>();
        //    foreach (var item in list)
        //    {
        //        string json = JsonConvert.SerializeObject(item);
        //        listHashEntry.Add(new HashEntry(getModelId(item), json));
        //    }
        //    DB.HashSet(key, listHashEntry.ToArray());
        //    DB.KeyExpire(key, TimeSpan.FromMinutes(expireTime));
        //}

        ///// <summary>
        ///// 获取hashkey所有的值
        ///// </summary>
        ///// <typeparam name="T"></typeparam>
        ///// <param name="key"></param>
        ///// <returns></returns>
        //public List<T> HashGetAll<T>(string key) where T : class
        //{
        //    List<T> result = new List<T>();
        //    HashEntry[] arr = DB.HashGetAll(key);
        //    foreach (var item in arr)
        //    {
        //        if (!item.Value.IsNullOrEmpty)
        //        {
        //            result.Add(Newtonsoft.Json.JsonConvert.DeserializeObject<T>(item.Value));
        //        }
        //    }
        //    return result;
        //}

        ///// <summary>
        ///// get the HashValue for string key  and hashkey
        ///// </summary>
        ///// <param name="key">Represents a key that can be stored in redis</param>
        ///// <param name="hashkey"></param>
        ///// <returns></returns>
        //public RedisValue GetHashValue(string key, string hashkey)
        //{
        //    RedisValue result = DB.HashGet(key, hashkey);
        //    return result;
        //}

        ///// <summary>
        ///// get the HashValue for string key  and hashkey
        ///// </summary>
        ///// <param name="key">Represents a key that can be stored in redis</param>
        ///// <param name="hashkey"></param>
        ///// <returns></returns>
        //public T GetHashValue<T>(string key, string hashkey) where T : class
        //{
        //    RedisValue result = DB.HashGet(key, hashkey);
        //    if (string.IsNullOrEmpty(result))
        //    {
        //        return null;
        //    }
        //    T t = JsonConvert.DeserializeObject<T>(result);
        //    return null;
        //}

        ///// <summary>
        ///// delete the HashValue for string key  and hashkey
        ///// </summary>
        ///// <param name="key"></param>
        ///// <param name="hashkey"></param>
        ///// <returns></returns>
        //public bool DeleteHashValue(string key, string hashkey)
        //{
        //    return DB.HashDelete(key, hashkey);
        //}
    }
}