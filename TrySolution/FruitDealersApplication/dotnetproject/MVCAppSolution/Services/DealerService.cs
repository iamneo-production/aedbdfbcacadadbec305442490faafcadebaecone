using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using BookStoreApp.Models;

namespace BookStoreApp.Services
{
    public interface IDealerService
    {
        bool AddDealer(Dealer dealer);
        List<Dealer> GetAllDealers();
        Dealer GetDealerById(int id);
        bool DeleteDealer(int id);
    }
    public class DealerService : IDealerService
    {
        private readonly HttpClient _httpClient;
            public DealerService(HttpClient httpClient,IConfiguration configuration)
        {
HttpClientHandler clientHandler = new HttpClientHandler();
clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };
_httpClient=new HttpClient(clientHandler);
         var apiSettings = configuration.GetSection("ApiSettings").Get<ApiSettings>();
        _httpClient.BaseAddress =new Uri(apiSettings.BaseUrl) ;
        }

        public bool AddDealer(Dealer dealer)
        {
            try
            {
                var json = JsonConvert.SerializeObject(dealer);
                var content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");

                HttpResponseMessage response = _httpClient.PostAsync(_httpClient.BaseAddress+$"/Dealer", content).Result;

                return response.IsSuccessStatusCode;
            }
            catch (HttpRequestException)
            {
                return false;
            }
        }

        public List<Dealer> GetAllDealers()
        {
            try
            {
                HttpResponseMessage response = _httpClient.GetAsync(_httpClient.BaseAddress+"/Dealer").Result;

                if (response.IsSuccessStatusCode)
                {
                    string data = response.Content.ReadAsStringAsync().Result;
                    return JsonConvert.DeserializeObject<List<Dealer>>(data);
                }

                return new List<Dealer>();
            }
            catch (HttpRequestException)
            {
                return new List<Dealer>();
            }
        }

        public Dealer GetDealerById(int id)
        {
            try
            {
                HttpResponseMessage response = _httpClient.GetAsync(_httpClient.BaseAddress+$"/Dealer/{id}").Result;

                if (response.IsSuccessStatusCode)
                {
                    string data = response.Content.ReadAsStringAsync().Result;
                    return JsonConvert.DeserializeObject<Dealer>(data);
                }

                return null;
            }
            catch (HttpRequestException)
            {
                return null;
            }
        }

        public bool DeleteDealer(int id)
        {
            try
            {
                HttpResponseMessage response = _httpClient.DeleteAsync(_httpClient.BaseAddress+$"/Dealer/{id}").Result;

                return response.IsSuccessStatusCode;
            }
            catch (HttpRequestException)
            {
                return false;
            }
        }
    }
}
