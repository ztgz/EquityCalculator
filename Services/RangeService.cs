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
            Dictionary<_Hand, bool> hands = new Dictionary<_Hand, bool>();
            string[] combinations = range.Split(',');
            foreach (var comb in combinations)
            {
                foreach (_Hand hand in TransformCombinationToHands(comb))
                {
                    hands.TryAdd(hand, true);
                }
            }

            return hands.Keys.ToList();
        }

        private IList<_Hand> TransformCombinationToHands(string combination)
        {
            IList<_Hand> hands = new List<_Hand>();
            RangeUtils.GetHands(combination);

            return hands;
        }
    }
}
