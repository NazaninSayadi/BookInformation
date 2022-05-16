namespace DataAccess.WebApiServices
{
    public interface ITaaghcheService
    {
        Task<string> GetById(int id);
    }
}
