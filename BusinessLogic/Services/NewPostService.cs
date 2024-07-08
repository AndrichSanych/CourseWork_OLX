using BusinessLogic.Interfaces;
using BusinessLogic.Exceptions;
using BusinessLogic.Models.NewPostModels;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using SixLabors.ImageSharp;
using System.Text;
using System.Net;
using static BusinessLogic.Models.NewPostModels.NPSettlementResponseViewModel;

namespace BusinessLogic.Services
{
    internal class NewPostService:INewPostService
    {
       
            
        private readonly HttpClient _httpClient;
        private readonly string _key;

        public NewPostService(IConfiguration config)
        {
           
            _httpClient = new HttpClient();
            this._key = config.GetValue<string>("NovaposhtaKey")!;
        }
        public async Task<IEnumerable<NPAreaItemViewModel?>>  GetAreas()
        {
            NPAreaRequestViewModel model = new NPAreaRequestViewModel
            {
                ApiKey = _key,
                ModelName = "Address",
                CalledMethod = "getSettlementAreas",
                MethodProperties = new NPAreaProperties
                {
                    Page = 1,
                    Ref = ""
                }
            };

            string json = JsonConvert.SerializeObject(model);
            HttpContent content = new StringContent(json, Encoding.UTF8, "application/json");
            HttpResponseMessage response = await _httpClient.PostAsync("https://api.novaposhta.ua/v2.0/json/", content);
            if (response.IsSuccessStatusCode)
            {
                string responseData = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<NPAreaResponseViewModel>(responseData);
                if (result != null && result.Data.Count != 0)
                {
                    return result.Data.GroupBy(x => x.Ref).Select(z => z.FirstOrDefault()) ;
                }
                else
                {
                    return Array.Empty<NPAreaItemViewModel>();
                }
            }
            else
            {
                throw new HttpException( "Error NewPost service", HttpStatusCode.InternalServerError);
            }
        }

        public async Task<IEnumerable<NPSettlementItemViewModel?>> GetCities()
        {
            int page = 1;
            List<NPSettlementItemViewModel?> cities = [];
            while (true)
            {
                NPSettlementRequestViewModel model = new NPSettlementRequestViewModel
                {
                    ApiKey = _key,
                    ModelName = "AddressGeneral",
                    CalledMethod = "getSettlements",
                    MethodProperties = new NPSettlementProperties
                    {
                        Page = page
                    }
                };

                string json = JsonConvert.SerializeObject(model);
                HttpContent content = new StringContent(json, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await _httpClient.PostAsync("https://api.novaposhta.ua/v2.0/json/", content);
                if (response.IsSuccessStatusCode)
                {
                    string responseData = await response.Content.ReadAsStringAsync();
                    var result = JsonConvert.DeserializeObject<NPSettlementResponseViewModel>(responseData);
                    if (result!=null && result.Data.Count != 0)
                    {
                        cities.AddRange(result.Data.GroupBy(x => new { x.Description,x.Area }).Select(z => z.FirstOrDefault()));
                        page++;
                    }
                    else
                    {
                        return cities;
                    }
                }
                else
                {
                    throw new HttpException("Error NewPost service", HttpStatusCode.InternalServerError);
                }
            }

        }
    }
}
