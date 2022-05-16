namespace ExternalServices.WebApiServices
{
    public interface ITaaghcheService
    {
        Task<string> GetById(int id);
    }
}
