using Models.ViewModels;
using Services.Interfaces;
using System.Threading.Tasks;
using Common.Helpers;

namespace Services
{
    public class EquityService : IEquityService
    {
        public async Task<(int statusCode, _Equities data)> GetEquities(_Equities inputData)
        {
            if (RangeUtils.IsValid(inputData.range0))
            {
                inputData.equity0 = 1.0m;
            }
            else
            {
                inputData.equity0 = 0.5m;
            }
            return (200, inputData);
        }
    }
}
