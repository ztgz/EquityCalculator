using System.Collections.Generic;
using System.Linq;
using Common.Helpers;
using Models.ViewModels;
using Services.Interfaces;

namespace Services
{
    public class RangeService : IRangeService
    {
        public RangeService ()
        {
            
        }
        
        public IList<_Hand> GetHands(string range)
        {
            string[] combinations = range.Split(',');
            Dictionary<string, _Hand> strHands = new Dictionary<string, _Hand>(); 
            foreach (var comb in combinations)
            {
                foreach (var strHand in RangeUtils.GetHands(comb))
                {
                    strHands.TryAdd(strHand, new _Hand(strHand));
                }
            }

            return strHands.Values.ToList();
        }
    }
}
