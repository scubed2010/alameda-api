using Alameda.Business.DTOs.Responses;
using Microsoft.AspNetCore.Mvc;

namespace Alameda.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class HealthCheck : Controller
    {
        [HttpGet]
        public ServiceResponse<bool> Index()
        {
            // Normally, we should make a simple call to the database or other key infrastructure to confirm system health
            
            return new ServiceResponse<bool>
            {
                Success = true,
                ResponseObject = true
            };
        }
    }
}
