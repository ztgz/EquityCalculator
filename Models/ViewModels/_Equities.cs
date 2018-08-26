using System.Collections.Generic;

namespace Models.ViewModels
{
    public class _Equities
    {
        private const int MAX_PLAYERS = 10;

        public IList<string>  Ranges   { get; set; }
        public IList<decimal> Equities { get; set; }

        public bool FlushBeatFullHouse { get; set; }
        public bool TripsBeatStaright  { get; set; }

        public _Equities()
        {
            Ranges   = new List<string>(MAX_PLAYERS);
            Equities = new List<decimal>(MAX_PLAYERS);
        }
    }
}
