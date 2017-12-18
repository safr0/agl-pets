using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using Microsoft.Extensions.Logging;
using Assignment.Models;
using System.Net.Http.Headers;
using Assignment.Interface;

namespace Assignment.Controllers
{
    [ApiVersion("1.0")]
    [Produces("application/json")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ControllerName("pets")]
    public class PetsController : Controller
    {
        private readonly ILogger _logger;
        private readonly IHttpClient httpClient;

        public PetsController(ILogger<PetsController> logger, IHttpClient _httpClient)
        {
            _logger = logger;
            httpClient = _httpClient;
        }


        [HttpGet]
        public async Task<IActionResult> GetAsync(string gender,string pet_type)
        {            
            try
            {
                _logger.LogInformation(LoggingEvent.LIST_ITEMS, "Get List of Pets ");
                
                var stringResult = await httpClient.GetAsync(new Uri("http://agl-developer-test.azurewebsites.net/people.json"));
                IEnumerable<Owner> rawData = Newtonsoft.Json.JsonConvert.DeserializeObject<IEnumerable<Owner>>(stringResult);

                //Return the cats based on gender
                List<string> cats = rawData.Where(o => o.Gender.ToLower().Equals(gender.Trim().ToLower()) && o.OwnerPets != null)
                    .SelectMany(t=> t.OwnerPets)
                    .Where(p=>p.Type.Trim().ToLower().Equals(pet_type.Trim().ToLower()))
                    .Select(p=> p.Name)
                    .OrderBy(o=>o)
                    .ToList();

                return Ok(cats);                
            }
            catch (HttpRequestException ex)
            {
                _logger.LogError(LoggingEvent.LIST_ITEMS, "Exception while getting pets data {0}", ex.ToString());
                return BadRequest("Error in getting pets data" + ex.ToString());
            }
        }
    }
}