using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Redis101.Request;
using Redis101.Services;

namespace Redis101.Controllers
{
    [ApiController]
    [Route("v1/cache")]
    public class CacheController : ControllerBase
    {
        private readonly ICacheService _cacheServices;
        public CacheController(ICacheService cacheServices)
        {
            _cacheServices = cacheServices ?? throw new ArgumentNullException(nameof(cacheServices));
        }

        [HttpGet("{key}")]
        public async Task<IActionResult> GetValue([FromRoute] string key)
        {
            var result = await _cacheServices.GetCacheValueAsync(key);

            return string.IsNullOrWhiteSpace(result) ? 
            NotFound($"Not found register with key {key}") : Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> PostValue([FromBody] EntryRedisRequest request)
        {
            await _cacheServices.SetCacheValueAsync(request.Key, request.Value);

            return Ok();
        }
    }
}