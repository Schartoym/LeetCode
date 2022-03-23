using System.Collections.Generic;
using FluentAssertions;
using Xunit;

namespace LeetCode;

public class RomanToInteger
{
    [Theory]
    [InlineData("XV", 15)]
    [InlineData("XIV", 14)]
    [InlineData("XXVII", 27)]
    [InlineData("MCMXCIV", 1994)]
    [InlineData("LVIII", 58)]
    public void Validate(string num, int result)
    {
        var solution = new Solution();

        solution.RomanToInt(num).Should().Be(result);
    }
    
    public class Solution
    {

        private readonly Dictionary<char, int> _dict = new()
        {
            { 'I', 1 },
            { 'V', 5 },
            { 'X', 10 },
            { 'L', 50 },
            { 'C', 100 },
            { 'D', 500 },
            { 'M', 1000 }
        };

        public int RomanToInt(string s)
        {
            var sum = 0;
            var lastNum = 0;

            for (var i = s.Length - 1; i >= 0; i--)
            {
                var currentNum = _dict[s[i]];

                if (currentNum < lastNum)
                {
                    sum -= currentNum;
                }
                else
                {
                    sum += currentNum;
                }

                lastNum = currentNum;
            }

            return sum;
        }
    }
}