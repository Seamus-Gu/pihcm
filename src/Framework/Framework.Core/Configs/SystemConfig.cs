namespace Framework.Core.Configs
{
    public class SystemConfig
    {
        /// <summary>
        /// Gets or sets the version identifier for the current instance.
        /// </summary>
        public string Version { get; set; } = "v0.0.0";

        /// <summary>
        /// Gets or sets a value indicating whether the current operation is a rollback.
        /// </summary>
        public bool IsRollback { get; set; }
    }
}
