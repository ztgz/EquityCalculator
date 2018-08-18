using System.Collections.Generic;
using Models.ViewModels;
using Services.Interfaces;
using System.Threading.Tasks;
using Common.Helpers;

namespace Services
{
    public class EquityService : IEquityService
    {
        private readonly IRangeService _rangeService;

        public EquityService()
        {
            _rangeService = new RangeService();
        }

        public async Task<(int statusCode, _Equities data)> GetEquities(_Equities inputData)
        {
            foreach (var range in inputData.ranges)
            {
                if (!RangeUtils.IsValid(range))
                {
                    return (400, null);
                }
            }

            foreach (var range in inputData.ranges)
            {
                IList<_Hand> hands = _rangeService.GetHands(range);
            }

            return (200, inputData);
        }
    }
}
