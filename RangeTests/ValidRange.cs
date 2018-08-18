using Xunit;
using Common.Helpers;

namespace RangeTests
{
    public class ValidRange
    {
        [Fact]
        public void SpecificCards()
        {
            Assert.True(RangeUtils.IsValid("Ah6s"));
            Assert.True(RangeUtils.IsValid("Kd6d"));
            Assert.True(RangeUtils.IsValid("7c7d"));
            Assert.True(RangeUtils.IsValid("8d8s"));
            Assert.True(RangeUtils.IsValid("KdKs"));

            Assert.False(RangeUtils.IsValid("AhAh"));
            Assert.False(RangeUtils.IsValid("Js2d"));
        }

        [Fact]
        public void SpecificCombination()
        {
            Assert.True(RangeUtils.IsValid("ATs"));
            Assert.True(RangeUtils.IsValid("J9o"));
            Assert.True(RangeUtils.IsValid("66"));
            Assert.True(RangeUtils.IsValid("K6"));

            Assert.False(RangeUtils.IsValid("ATd"));
            Assert.False(RangeUtils.IsValid("A4o"));
            Assert.False(RangeUtils.IsValid("J5"));
        }

        [Fact]
        public void RangeCombination()
        {
            Assert.True(RangeUtils.IsValid("AA-77"));
            Assert.True(RangeUtils.IsValid("KK-JJ"));
            Assert.True(RangeUtils.IsValid("JT-J8"));
            Assert.True(RangeUtils.IsValid("QTs-Q6s"));
            Assert.True(RangeUtils.IsValid("KAo-KJo"));

            Assert.False(RangeUtils.IsValid("66-66"));
            Assert.False(RangeUtils.IsValid("55-66"));
            Assert.False(RangeUtils.IsValid("JT-J6o"));
            Assert.False(RangeUtils.IsValid("JTs-J9o"));
            Assert.False(RangeUtils.IsValid("T7-T8"));
            Assert.False(RangeUtils.IsValid("77-TT"));
            Assert.False(RangeUtils.IsValid("T8-98"));
        }

        [Fact]
        public void SeveralCombinations()
        {
            Assert.True(RangeUtils.IsValid("AKs-ATs,QQ-TT,76o-79o"));
            Assert.False(RangeUtils.IsValid("AKs-ATs,QQ-TT,75o-79o"));
        }
    }
}
