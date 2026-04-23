namespace Framework.Core
{
    public interface ICache
    {
        #region 获取缓存

        /// <summary>
        /// 获取缓存数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        string Get(string key);

        /// <summary>
        /// 获取缓存数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        Task<T> GetAsync<T>(string key);

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
        Task<bool> SetAsync(string key, object value, TimeSpan? timeout);

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
