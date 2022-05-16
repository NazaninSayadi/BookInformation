
namespace ExternalServices.WebApiServices
{
    public class TaaghcheService : ITaaghcheService
    {
        public async Task<string> GetById(int id)
        {
            using var httpClient = new HttpClient();
            //in api tu akharin test down bood.
            using var response = await httpClient.GetAsync($"https://get.taaghche.com/v2/book/{id}");
            return await response.Content.ReadAsStringAsync();
        }
    }
}
