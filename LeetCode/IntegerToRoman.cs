using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentAssertions;
using Xunit;

namespace LeetCode;

public class IntegerToRoman
{
    [Theory]
    [InlineData(15, "XV")]
    [InlineData(20, "XX")]
    [InlineData( 14, "XIV")]
    [InlineData( 27, "XXVII")]
    [InlineData( 1994, "MCMXCIV")]
    [InlineData( 58, "LVIII")]
    public void Validate(int num, string roman)
    {
        var solution = new Solution();

        solution.IntToRoman(num).Should().Be(roman);
    }
    
    public class Solution
    {

        private readonly Dictionary<int, string> _dict = new()
        {
            { 1000, "M" },
            { 900, "CM" },
            { 500, "D" },
            { 400, "CD" },
            { 100, "C" },
            { 90, "XC" },
            { 50, "L" },
            { 40, "XL" },
            { 10, "X" },
            { 9, "IX" },
            { 5, "V" },
            { 4, "IV" },
            { 1, "I" },
        };

        public string IntToRoman(int num)
        {
            var sb = new StringBuilder();

            var sortedKeys = _dict.Keys.OrderBy(k => k).ToArray();
            
            while (num > 0)
            {
                var key = sortedKeys.Last(k => k <= num);

                sb.Append(_dict[key]);
                num -= key;
            }

            return sb.ToString();
        }
    }
}