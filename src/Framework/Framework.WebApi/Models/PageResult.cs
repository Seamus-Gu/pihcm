namespace Framework.WebApi
{
    public class PageResult<T>
    {
        /// <summary>
        /// Gets or sets the collection of items contained in the current instance.
        /// </summary>
        public IReadOnlyList<T> Items { get; set; } = Array.Empty<T>();

        /// <summary>
        /// Gets or sets the total number of items represented by the current instance.
        /// </summary>
        public int Total { get; set; }
    }
}
