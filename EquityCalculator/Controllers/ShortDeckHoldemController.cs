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
        public IActionResult GetEquitys(string rangePos0, string rangePos1, string rangePos2, string rangePos3,
            string rangePos4, string rangePos5, string rangePos6, string rangePos7)
        {
            _Equities inputData = new _Equities();
            inputData.Ranges.Add("ATs,AhTh");
            inputData.FlushBeatFullHouse = true;
            inputData.TripsBeatStaright  = false;

            (int statusCode, _Equities data) = _equityService.GetEquities(inputData);
            return StatusCode(statusCode, data);
        }
    }
}