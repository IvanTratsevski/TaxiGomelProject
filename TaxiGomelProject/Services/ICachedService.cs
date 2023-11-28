namespace TaxiGomelProject.Services
{
    public interface ICachedService<T> where T : class
    {
        public void AddData(string cacheKey);
        public IEnumerable<T> GetData(string cacheKey);
    }
}
