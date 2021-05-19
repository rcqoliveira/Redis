using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using RC.RedisApi.Domains;
using RC.RedisApi.Extensions;
using System.Threading.Tasks;

namespace RC.RedisApi.Controllers
{
    [ApiController]
    [Route("v1/redis")]
    public class RedisController : Controller
    {
        private readonly IDistributedCache distributedCache;

        public RedisController(IDistributedCache distributedCache)
        {
            this.distributedCache = distributedCache;
        }

        [HttpGet]
        [Route("id:string")]
        public Task<Country> Get(string id)
        {
            return this.distributedCache.GetRecordAsync<Country>(id);
        }

        [HttpPost]
        [Route("")]
        public async void Post(string name)
        {
            var country = new Country(name);
            await this.distributedCache.SetRecordAsync(country.Id.ToString(), country);
        }
    }
}