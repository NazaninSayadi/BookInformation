namespace Cache
{
    public class CacheKey<T>
    {
        public CacheKey(string prefix, string suffix)
        {
            Prefix = prefix;
            Suffix = suffix;
        }
        public string Create(T key)
        {
            return $"{Prefix}_{key}_{Suffix}";
        }
        private string _prefix;

        private string Prefix
        {
            get => _prefix;
            init
            {
                _prefix = string.IsNullOrEmpty(value) ? "Item" : value;
            }
        }

        private string _suffix;
        private string Suffix
        {
            get => _suffix;
            init
            {
                _suffix = string.IsNullOrEmpty(value) ? "Cache" : value;
            }
        }
    }
}