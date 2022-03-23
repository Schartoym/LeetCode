using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentAssertions;
using Xunit;

namespace LeetCode;

public class NumberToWords
{
    [Theory]
    [InlineData(0, "Zero")]
    [InlineData(123, "One Hundred Twenty Three")]
    [InlineData(12345, "Twelve Thousand Three Hundred Forty Five")]
    [InlineData(1234567, "One Million Two Hundred Thirty Four Thousand Five Hundred Sixty Seven")]
    public void ValidateFull(int number, string words)
    {
        new Solution().NumberToWords(number).Should().Be(words);
    }
    
    [Theory]
    [InlineData(0, "Zero")]
    [InlineData(23, "Twenty Three")]
    [InlineData(45, "Forty Five")]
    [InlineData(67, "Sixty Seven")]
    [InlineData(6, "Six")]
    [InlineData(16, "Sixteen")]
    [InlineData(99, "Ninety Nine")]
    public void ValidateLessThan100(int number, string words)
    {
        new Solution().NumberLessThan100ToWords(number).Should().Be(words);
    }
    
    public class Solution
    {
        private readonly Dictionary<int, string> _numbers = new()
        {
            { 0, "Zero" },
            { 1, "One" },
            { 2, "Two" },
            { 3, "Three" },
            { 4, "Four" },
            { 5, "Five" },
            { 6, "Six" },
            { 7, "Seven" },
            { 8, "Eight" },
            { 9, "Nine" },
            { 10, "Ten" },
            { 11, "Eleven" },
            { 12, "Twelve" },
            { 13, "Thirteen" },
            { 14, "Fourteen" },
            { 15, "Fifteen" },
            { 16, "Sixteen" },
            { 17, "Seventeen" },
            { 18, "Eighteen" },
            { 19, "Nineteen" },
            { 20, "Twenty" },
            { 30, "Thirty" },
            { 40, "Forty" },
            { 50, "Fifty" },
            { 60, "Sixty" },
            { 70, "Seventy" },
            { 80, "Eighty" },
            { 90, "Ninety" },
        };

        private readonly Dictionary<int, string> _ranks = new()
        {
            { (int)Math.Pow(10, 9), "Billion" },
            { (int)Math.Pow(10, 6), "Million" },
            { (int)Math.Pow(10, 3), "Thousand" },
            { (int)Math.Pow(10, 2), "Hundred" },
        };

        public string NumberToWords(int num)
        {
            if (_numbers.ContainsKey(num))
            {
                return _numbers[num];
            }
            
            var sb = new StringBuilder();
            
            var rankKeys = _ranks.Keys.OrderByDescending(k => k).ToArray();
            
            while (num > 0)
            {
                var rank = rankKeys.FirstOrDefault(r => num / r > 0);

                if (rank != default)
                {
                    var rankCount = num / rank;
                    sb.Append(NumberToWords(rankCount));
                    sb.Append(' ');
                    sb.Append(_ranks[rank]);
                    sb.Append(' ');

                    num -= rankCount * rank;
                }
                else
                {
                    sb.Append(NumberLessThan100ToWords(num));
                    sb.Append(' ');
                    
                    break;
                }
            }

            return sb.ToString().Trim();
        }

        public string NumberLessThan100ToWords(int num)
        {
            if (num >= 100)
            {
                throw new ArgumentOutOfRangeException(nameof(num), "Number must be less than 100");
            }
            
            if (_numbers.ContainsKey(num))
            {
                return _numbers[num];
            }
            
            var keys = _numbers.Keys.OrderBy(k => k).ToArray();
            
            var sb = new StringBuilder();

            while (num > 0)
            {
                var key = keys.Last(k => k <= num);

                sb.Append(_numbers[key]);
                sb.Append(' ');

                num -= key;
            }

            return sb.ToString().Trim();
        }
    }
}