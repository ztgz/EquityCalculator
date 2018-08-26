using Models.ViewModels;
using System.Threading.Tasks;

namespace Services.Interfaces
{
    public interface IEquityService
    {
        (int statusCode, _Equities data) GetEquities(_Equities inputData);
    }
}
