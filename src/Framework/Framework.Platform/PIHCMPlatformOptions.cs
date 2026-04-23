namespace Framework.Platform
{
    /// <summary>
    /// Provides configuration options for the PIHCM platform, enabling or disabling core features such as SQL Sugar
    /// integration, logging, and Redis caching.
    /// </summary>
    /// <remarks>Use this class to configure platform-wide behaviors at application startup. Changing these
    /// options affects how the platform interacts with data storage, logging, and caching subsystems.</remarks>
    public sealed class PIHCMPlatformOptions
    {
        /// <summary>
        /// Gets or sets a value indicating whether the SqlSugar ORM integration is enabled.
        /// </summary>
        public bool EnableSqlSugar { get; set; } = true;

        /// <summary>
        /// Gets or sets a value indicating whether logging is enabled.
        /// </summary>
        public bool EnableLog { get; set; } = true;

        /// <summary>
        /// Gets or sets a value indicating whether Redis caching is enabled.
        /// </summary>
        public bool EnableRedisCache { get; set; } = true;
    }
}
