using System.Collections.Generic;
using Models.ViewModels;

namespace Services.Interfaces
{
    public interface IRangeService
    {
        IList<_Hand> GetHands(string range);
    }
}
