namespace Framework.Core
{
    public interface ICache
    {
        #region 获取缓存

        /// <summary>
        /// Retrieves the value associated with the specified key from the cache and deserializes it to the specified type.
        /// </summary>
        /// <typeparam name="T">The type to which the cached value will be deserialized.</typeparam>
        /// <param name="key">The key whose associated value is to be retrieved. Cannot be null.</param>
        /// <returns>The deserialized value of type T if the key exists and the value can be deserialized; otherwise, the default
        /// value for type T.</returns>
        T? Get<T>(string key);

        /// <summary>
        /// 获取缓存数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        Task<T?> GetAsync<T>(string key);

        #endregion

        #region 添加缓存

        /// <summary>
        /// 添加缓存
        /// </summary>
        /// <param name="key">键</param>
        /// <param name="value">值</param>
        /// <param name="timeout">过期时间</param>
        /// <returns>添加结果</returns>
        bool Set(string key, object value, TimeSpan? timeout);

        /// <summary>
        /// 添加缓存
        /// </summary>
        /// <param name="key">键</param>
        /// <param name="value">值</param>
        /// <param name="timeout">过期时间</param>
        /// <returns>添加结果</returns>
        Task<bool> SetAsync(string key, object value, TimeSpan? timeout = null);

        #endregion

        #region 移除缓存

        /// <summary>
        /// 移除缓存
        /// </summary>
        /// <param name="key">键</param>
        /// <returns>移除结果</returns>
        bool Remove(string key);

        /// <summary>
        /// 移除缓存
        /// </summary>
        /// <param name="key">键</param>
        /// <returns>移除结果</returns>
        Task<bool> RemoveAsync(string key);

        #endregion
    }
}
