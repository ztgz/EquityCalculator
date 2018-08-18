using System.Collections.Generic;

namespace Models.ViewModels
{
    public class _Equities
    {
        private const int MAX_PLAYERS = 10;

        public IList<string>  ranges   { get; set; }
        public IList<decimal> equities { get; set; }

        public _Equities()
        {
            ranges   = new List<string>(MAX_PLAYERS);
            equities = new List<decimal>(MAX_PLAYERS);
        }
    }
}
