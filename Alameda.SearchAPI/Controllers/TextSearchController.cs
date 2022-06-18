using Alameda.Business.DTOs.Requests;
using Alameda.Business.DTOs.Responses;
using Alameda.Business.Services;
using Microsoft.AspNetCore.Mvc;

namespace Alameda.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TextSearchController : ControllerBase
    {
        private readonly TextSearchService _textSearchService;

        public TextSearchController(
            TextSearchService textSearchService
            )
        {
            _textSearchService = textSearchService;
        }

        [HttpPost]
        public async Task<ServiceResponse<TextSearchResponse>> Post([FromBody]TextSearchRequest request)
        {
            try
            {
                return await _textSearchService.ExecuteSearch(request);
            }
            catch (Exception ex)
            {
                return new ServiceResponse<TextSearchResponse>
                {
                    Success = false,
                    Message = ex.Message
                };
            }
        }
    }
}
