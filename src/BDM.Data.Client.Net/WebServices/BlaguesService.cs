using BDM.Common.Model;
using BDM.Data.Client.Contracts;
using BDM.Data.Client.Mock;
using BDM.Data.Client.Net.Utils;
using BDM.Data.Client.Net.WebServicesData;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BDM.Data.Client.Net.WebServices
{
    public class BlaguesService : IBlaguesService
    {
        private readonly RestClient _client;
        private const string _baseUrl = "http://ws.blaguesdemerde.fr/v1";

        public BlaguesService(IAccessIsolatedStorage isolatedStorage)
        {
            _client = new RestClient(isolatedStorage);
        }

        public async Task<Dictionary<Order, List<Blague>>> GetBlagues()
        {
            var request = new GetBlaguesRequest();
            var resp = await _client.SendDataAsync<GetBlaguesRequest, GetBlaguesResponse>(_baseUrl, request.Command, request, RestClient.DefaultCacheLifetime, CachePolicy.CanUseOldValues);

            var dicoBlagues = new Dictionary<Order, List<Blague>>();

            if (resp?.Response?.Meta?.Code == 200 && resp?.Response?.Data != null)
            {
                dicoBlagues[Order.Last] = resp.Response.Data.Last ?? new List<Blague>();
                dicoBlagues[Order.TopWeek] = resp.Response.Data.TopWeek ?? new List<Blague>();
                dicoBlagues[Order.TopMonth] = resp.Response.Data.TopMonth ?? new List<Blague>();
                dicoBlagues[Order.TopYear] = resp.Response.Data.TopYear ?? new List<Blague>();
                dicoBlagues[Order.Random] = resp.Response.Data.Random ?? new List<Blague>();
            }
            return dicoBlagues;
        }

        public async Task<List<Category>> GetCategories()
        {
            var request = new GetCategoriesRequest();
            var resp = await _client.SendDataAsync<GetCategoriesRequest, GetCategoriesResponse>(_baseUrl, request.Command, request, RestClient.DefaultCacheLifetime, CachePolicy.CanUseOldValues);

            if (resp?.Response?.Meta?.Code == 200 && resp?.Response?.Data != null)
            {
                return resp?.Response?.Data.Categories;
            }
            return new List<Category>();
        }

        public async Task<Dictionary<Order, List<Blague>>> GetBlaguesForCategory(int categoryId)
        {
            var request = new GetBlaguesForCategoryRequest(categoryId);
            var resp = await _client.SendDataAsync<GetBlaguesForCategoryRequest, GetBlaguesForCategoryResponse>(_baseUrl, request.Command, request, RestClient.DefaultCacheLifetime, CachePolicy.CanUseOldValues);

            var dicoBlagues = new Dictionary<Order, List<Blague>>();

            if (resp?.Response?.Meta?.Code == 200 && resp?.Response?.Data != null)
            {
                dicoBlagues[Order.Last] = resp.Response.Data.Last ?? new List<Blague>();
                dicoBlagues[Order.TopWeek] = resp.Response.Data.TopWeek ?? new List<Blague>();
                dicoBlagues[Order.TopMonth] = resp.Response.Data.TopMonth ?? new List<Blague>();
                dicoBlagues[Order.TopYear] = resp.Response.Data.TopYear ?? new List<Blague>();
                dicoBlagues[Order.Random] = resp.Response.Data.Random ?? new List<Blague>();
            }
            return dicoBlagues;
        }

        public async Task<int> Vote(int blagueId, bool like)
        {
            var request = new VoteRequest(blagueId, like);
            var resp = await _client.SendDataAsync<VoteRequest, VoteResponse>(_baseUrl, request.Command, request, RestClient.DefaultCacheLifetime, CachePolicy.CanUseOldValues);

            return 0;
        }
    }
}
