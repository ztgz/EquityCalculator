using System.Collections.Generic;
using Common.Helpers;
using Xunit;

namespace RangeTests
{
    public class RangeGetCards
    {
        [Fact]
        public void SpecificCards()
        {
            IList<string> result;
            result = new List<string>{ "AhAd"};
            Assert.Equal(result, RangeUtils.GetHands("AdAh"));
            result = new List<string> { "Kc6s" };
            Assert.Equal(result, RangeUtils.GetHands("6sKc"));

            result = new List<string>{};
            Assert.Equal(result, RangeUtils.GetHands("6s6s"));
            Assert.Equal(result, RangeUtils.GetHands("5d7s"));
        }

        [Fact]
        public void CombinationOfCards()
        {
            IList<string> result;
            result = new List<string> { "AhAs", "AhAd", "AhAc", "AsAd", "AsAc", "AdAc" };
            Assert.Equal(result, RangeUtils.GetHands("AA"));
            result = new List<string> { "AhTh", "AsTs", "AdTd", "AcTc" };
            Assert.Equal(result, RangeUtils.GetHands("ATs"));
            result = new List<string> { "AhTs", "AhTd", "AhTc", "AsTh", "AsTd", "AsTc", "AdTh", "AdTs", "AdTc", "AcTh", "AcTs", "AcTd" };
            Assert.Equal(result, RangeUtils.GetHands("ATo"));
            result = new List<string> { "AhTh", "AsTs", "AdTd", "AcTc", "AhTs", "AhTd", "AhTc", "AsTh", "AsTd", "AsTc", "AdTh", "AdTs", "AdTc", "AcTh", "AcTs", "AcTd" };
            Assert.Equal(result, RangeUtils.GetHands("AT"));
        }

        [Fact]
        public void IsRangeWithSuitSpecified()
        {
            IList<string> result;
            result = new List<string> { "KhTh", "KsTs", "KdTd", "KcTc", "Kh9h", "Ks9s", "Kd9d", "Kc9c" };
            Assert.Equal(result, RangeUtils.GetHands("KTs-K9s"));
            result = new List<string> {
                "KhTs", "KhTd", "KhTc", "KsTh", "KsTd", "KsTc", "KdTh", "KdTs","KdTc", "KcTh", "KcTs", "KcTd",
                "Kh9s", "Kh9d", "Kh9c", "Ks9h", "Ks9d", "Ks9c", "Kd9h", "Kd9s", "Kd9c", "Kc9h",  "Kc9s", "Kc9d"
            };
            Assert.Equal(result, RangeUtils.GetHands("KTo-K9o"));

            result = new List<string> { };
            Assert.Equal(result, RangeUtils.GetHands("KTs-KJs"));
        }

        [Fact]
        public void IsRangeWithoutSuitSpecified()
        {
            IList<string> result;
            result = new List<string> {
                "KhTh", "KsTs", "KdTd", "KcTc", "Kh9h", "Ks9s", "Kd9d", "Kc9c",
                "KhTs", "KhTd", "KhTc", "KsTh", "KsTd", "KsTc", "KdTh", "KdTs","KdTc", "KcTh", "KcTs", "KcTd",
                "Kh9s", "Kh9d", "Kh9c", "Ks9h", "Ks9d", "Ks9c", "Kd9h", "Kd9s", "Kd9c", "Kc9h",  "Kc9s", "Kc9d"
            };
            Assert.Equal(result, RangeUtils.GetHands("KT-K9"));

            result = new List<string> { };
            Assert.Equal(result, RangeUtils.GetHands("KT-KJ"));
        }
    }
}
