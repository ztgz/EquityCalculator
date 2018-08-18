using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Services.Interfaces;
using Models.ViewModels;

namespace EquityCalculator.Controllers
{
    [Route("api_1/[controller]")]
    [ApiController]
    public class ShortDeckHoldemController : ControllerBase
    {
        private readonly IEquityService _equityService;
        
        public ShortDeckHoldemController(IEquityService equityService)
        {
            _equityService = equityService;
        }
        
        [HttpGet]
        [Route("test")]
        public async Task<IActionResult> GetEquitys(string rangePos0, string rangePos1, string rangePos2, string rangePos3,
            string rangePos4, string rangePos5, string rangePos6, string rangePos7)
        {
            _Equities inputData = new _Equities();
            inputData.ranges.Add("AT");

            (int statusCode, _Equities data) = await _equityService.GetEquities(inputData);
            return StatusCode(statusCode, data);
        }
    }
}