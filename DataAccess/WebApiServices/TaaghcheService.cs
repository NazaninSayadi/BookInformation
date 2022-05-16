
namespace DataAccess.WebApiServices
{
    public class TaaghcheService : ITaaghcheService
    {
        public async Task<string> GetById(int id)
        {
            using var httpClient = new HttpClient();
            using var response = await httpClient.GetAsync($"https://get.taaghche.com/v2/book/{id}");
            return await response.Content.ReadAsStringAsync();
        }
    }
}
